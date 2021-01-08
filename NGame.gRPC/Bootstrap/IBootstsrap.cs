using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NGame.Event;

namespace NGame.RPC
{
    public interface IBootstsrap
    {
        IAsyncEvent<T> DoConnectdAsync<T>(IPEndPoint adders) where T : ISocketChannelContext;
        IAsyncEvent<T> DoBindAsync<T>(IPEndPoint adders) where T : ISocketChannelContext;
        void SetIdelStage(IdelStage stage);
    }
}
