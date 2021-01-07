using NGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NRPC
{
    public abstract class AbstractBootstapChannel : IBootstsrap
    {
        public virtual void SetChannel(IHandleChannel channel) { }
        public virtual IAsyncEvent<T> DoConnectdAsync<T>(IPEndPoint adders) where T : ISocketChannelContext => null;
        public virtual IAsyncEvent<T> DoBindAsync<T>(IPEndPoint adders) where T : ISocketChannelContext => null;
        public virtual void SetPingpong(int time) { }
    }
}
