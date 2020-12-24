using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NSocket.Session
{
    public sealed class TcpSession : Core.ISocket
    {
        private bool disposedValue;
        public int id { get; set; }
        public string Adders { get; set; }
        public ushort Port { get; set; }


        public void Awake()
        {
            
        }

        public bool Connect(string adder, ushort port)
        {
            return false;
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            GC.SuppressFinalize(this);
        }
    }
}
