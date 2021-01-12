using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Web
{
    public interface IWebChannel : IDisposable
    {
        string Url { get; set; }
        uint Progres { get; set; }
        string LocalPath { get; set; }
        byte[] bytes { get; set; }
        Action<int> Progress { get; set; }
        Action<IWebChannel> completed { get; set; }

        void OnStart();
    }
}
