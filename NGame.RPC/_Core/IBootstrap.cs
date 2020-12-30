using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public interface IBootstrap
    {
        IBootstrap SetRemoteAdders(IPEndPoint endPoint);
        IBootstrap SetLocalAdders(IPEndPoint endPoint);
        IBootstrap SetTimeOut(uint time);
        IBootstrap SetChannelHandle<T>() where T : class, IChannelHandle, new();
        IBootstrap SetDecoderChannel<T>() where T : class, IDecoderComparserChannel, new();
        IBootstrap SetEncoderChannel<T>() where T : class, IEncoderComparerChannel, new();
        IBootstrap BindAsync();
        IBootstrap ConnectdAsync();
        IBootstrap CloseAsync();


    }
}
