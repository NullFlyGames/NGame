using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Web;

namespace NGame.Resource
{
    public class DownloadAssetbundleChannel : IWebChannel
    {
        public string Url { get; set; }
        public uint Progres { get; set; }
        public string LocalPath { get; set; }
        public object Current { get; }

        public void OnStart()
        {
            
        }

        public bool MoveNext()
        {
            return false;
        }

       

        public void Reset()
        {
            
        }
        public void Dispose()
        {
            
        }
    }
}
