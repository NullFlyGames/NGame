using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Web
{
    public enum DownloadType
    {
        Onece,
        All,
    }
    public interface IWebChannel : IDisposable
    {
        void Download(DownloadType type);
        void Lister(string url);
        string Request(string url);
        string Request(string url, Dictionary<string, string> header);
        string Post(string url, string data);
        void UpLoadFile(string url, string filePath);
    }
}
