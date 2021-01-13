using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NGame.Web
{
    public class Fragment
    {
        public int form;
        public int to;
        public bool isCompleted;
    }

    public class DownloadData
    {
        public string Url;
        public string LocalPath;
        public string TempFile;
        public string Name;
        public int Length;
        public List<Fragment> _fragments;
    }
    public class DownloadHandle
    {
        internal DownloadData Data;
        internal DownloadFileWebChannel Channel;

        internal void InitData(string url, string local)
        {
            if (File.Exists(local + ".temp"))
            {
                string data = File.ReadAllText(local + ".temp");
                if (string.IsNullOrEmpty(data) == false)
                {
                    Data = LitJson.JsonMapper.ToObject<DownloadData>(data);
                    return;
                }
            }
            splitDownloadData(url, local);
        }

        async void CreateFile()
        {
            if (File.Exists(Data.LocalPath))
            {
                if (FileHelper.GetLength(Data.LocalPath) == Data.Length)
                {
                    Ex.Log($"{Data.Name} 文件长度一致，开始下载");
                    return;
                }
                FileHelper.Delete(Data.LocalPath);
            }

            Ex.Log($"{Data.Name} 文件长度不一致或缓存文件不存在，重新下载");
            await FileHelper.Writr(Data.LocalPath, 0, new byte[Data.Length], 0, Data.Length);
        }
        bool splitDownloadData(string url, string local)
        {
            int offset = 0;
            Data = new DownloadData();
            Data.Length = (int)GetRemoteFileLength(url);
            if (Data.Length == -1) return false;

            Data.LocalPath = local;
            Data.Url = url;
            Data.TempFile = local + ".temp";
            Data.Name = Path.GetFileName(local);
            Data._fragments = new List<Fragment>();
            int maxSize = 1024 * 1024;
            int fragmentSize = Data.Length > maxSize ? maxSize : (int)Data.Length;
            while (offset < Data.Length)
            {
                Fragment fragment = new Fragment();
                fragment.form = offset;
                int length = offset + fragmentSize > Data.Length ? Data.Length - offset : fragmentSize;
                fragment.to = fragment.form + length;
                Data._fragments.Add(fragment);
                offset += length;
            }
            FileHelper.WriteAllTextLocked<DownloadData>(Data.TempFile, ref Data);
            return true;
        }
        internal void StarDownload(object o)
        {
            CreateFile();
            Ex.Log($"{Data.Name} 开始下载:{Data.Url}");
            Channel = (DownloadFileWebChannel)o;
            Data._fragments.ForEach(a => ThreadPool.QueueUserWorkItem(StarDownloadFragment, a));
        }

        internal async void StarDownloadFragment(object o)
        {
            Fragment fragment = (Fragment)o;
            try
            {
                if (fragment.isCompleted == true) return;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Data.Url);
                request.Proxy = null;
                request.AddRange(fragment.form, fragment.to);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        byte[] bytes = new byte[fragment.to - fragment.form];
                        do
                        {
                            int recv = stream.Read(bytes, 0, bytes.Length);
                            await FileHelper.Writr(Data.LocalPath, fragment.form, bytes, 0, recv);
                            fragment.form += recv;
                            fragment.isCompleted = fragment.form >= fragment.to;
                            FileHelper.WriteAllTextLocked(Data.TempFile, ref Data);
                        }
                        while (fragment.form < fragment.to);

                        if (GetIsCompleted())
                        {
                            lock (Channel)
                            {
                                Channel.DownloadFragmentCompleted(this);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Channel.DownloadFragmentFailur(this, ex);
            }
        }
        internal bool GetIsCompleted()
        {
            return Data._fragments.Where(a => a.isCompleted == false).Count() <= 0;
        }
        long GetRemoteFileLength(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        return response.ContentLength;
                    }
                }
            }
            catch (Exception ex)
            {
                Ex.Log(ex);
                return -1;
            }
        }
        internal void clearLastDownloadData()
        {
            FileHelper.Delete(Data.TempFile);
            Data._fragments.Clear();
            Data._fragments = null;
            Channel = null;
        }
    }
}
