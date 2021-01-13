using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections.Concurrent;

namespace NGame.Web
{
    public class DownloadFileWebChannel : AbstractWebRequesChannel
    {


        private DownloadType DownloadType;
        private Action<float> _progress;
        private Action<string> _completed;
        private Action<string, Exception> _error;
        private List<DownloadHandle> downloads = new List<DownloadHandle>();

        /// <summary>
        /// 下载进度事件
        /// </summary>
        public event Action<float> Progress
        {
            add { _progress += value; }
            remove { _progress -= value; }
        }
        /// <summary>
        /// 下载完成事件
        /// </summary>
        public event Action<string> Completed
        {
            add { _completed += value; }
            remove { _completed -= value; }
        }
        /// <summary>
        /// 下载错误事件
        /// </summary>
        public event Action<string, Exception> Error
        {
            add { _error += value; }
            remove { _error -= value; }
        }

        public void AddDownloadUrl(string url, string localPath)
        {
            DownloadHandle download = new DownloadHandle();
            download.InitData(url, localPath);
            downloads.Add(download);
        }
        public override void Download(DownloadType type)
        {
            DownloadType = type;
            if (downloads.Count <= 0) throw new IndexOutOfRangeException();
            if (type == DownloadType.Onece)
            {
                DownloadHandle download = downloads.First();
                ThreadPool.QueueUserWorkItem(download.StarDownload, this);
                return;
            }
            downloads.ForEach(a => ThreadPool.QueueUserWorkItem(a.StarDownload, this));
        }

        internal void DownloadFragmentFailur(DownloadHandle download, Exception ex)
        {
            Ex.Log(ex);
            _error?.Invoke(Path.GetFileName(download.Data.LocalPath), ex);
            download?.clearLastDownloadData();
            downloads.Remove(download);
            StarNextDownload(download);

        }

        internal void DownloadFragmentCompleted(DownloadHandle download)
        {
            Ex.Log(download.Data.Url + " 下载完成");
            download?.clearLastDownloadData();
            downloads.Remove(download);
            StarNextDownload(download);
        }
        void StarNextDownload(DownloadHandle download)
        {
            if (DownloadType == DownloadType.Onece)
            {
                if (downloads.Count <= 0) 
                {
                    _completed?.Invoke(Path.GetFileName(download.Data.LocalPath));
                    NCore.GetManaged<WebManaged>().RemoveWenChannel(this);
                    return; 
                }
                Download(DownloadType);
                return;
            }
            if (downloads.Where(a => a.GetIsCompleted() == false).FirstOrDefault() == null)
            {
                NCore.GetManaged<WebManaged>().RemoveWenChannel(this);
            }
        }

        public override void Dispose()
        {
            _completed = null;
            _progress = null;
            _error = null;
            downloads.Clear();
            Ex.Log("释放:" + GetType().Name);
        }
    }
}
