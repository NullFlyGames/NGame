using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Web
{
    public abstract class AbstractWebRequesChannel : IWebChannel
    {
        public abstract void Dispose();
        public virtual void Download(DownloadType type) { }


        public virtual void Lister(string url) { }
        public virtual string Post(string url, string data) => string.Empty;
        public virtual string Request(string url) => string.Empty;
        public virtual string Request(string url, Dictionary<string, string> header) => string.Empty;
        public virtual void UpLoadFile(string url, string filePath) { }

      
    }
}
