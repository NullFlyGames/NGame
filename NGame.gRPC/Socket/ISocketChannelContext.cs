using NGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NGame.RPC
{
    /// <summary>
    /// 网络操作上下文
    /// </summary>
    public interface ISocketChannelContext : IDisposable
    {
        int id { get; }
        IPEndPoint Adders { get; }
        IPEndPoint Local { get; }
        void Write(IMemory memory);
        void WriteAndFlush(IMemory memory);
        void Flush();
    }
}
