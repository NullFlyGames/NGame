using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 网络会话连接
    /// </summary>
    public interface IChannel
    {
        IntPtr SSID { get; set; }
        int GUID { get; set; }
        IPEndPoint LocalAddress { get; set; }
        IPEndPoint RemoteAddress { get; set; }
    }
}
