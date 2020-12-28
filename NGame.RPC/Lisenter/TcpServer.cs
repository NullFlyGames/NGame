using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public sealed class TcpServer<Handle> : ISocket<Handle> where Handle : IHandle
    {
        public int id => throw new NotImplementedException();

        public string Adders => throw new NotImplementedException();

        public ushort Port => throw new NotImplementedException();

        public Handle Handles => throw new NotImplementedException();

        public Socket Socket => throw new NotImplementedException();
    }
}
