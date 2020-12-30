using HPSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public interface ISocketChannelContext
    {
        IntPtr SSID { get; set; }
        int GUID { get; set; }
        IPEndPoint LocalAddress { get; set; }
        IPEndPoint RemoteAddress { get; set; }
        void WriteAndFlush(IMemory memory);
        void Write(IMemory memory);
        void Flush();
    }
}
