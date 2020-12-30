using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public class SocketChannelContext : IChannel
    {

        public IntPtr SSID { get; set; }
        public int GUID { get; set; }
        public IPEndPoint LocalAddress { get; set; }
        public IPEndPoint RemoteAddress { get; set; }
    }
}
