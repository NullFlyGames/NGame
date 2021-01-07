﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NRPC
{
    public abstract class AbstractHandleChannel : IHandleChannel
    {
        public virtual void OnConnectHandle(ISocketChannelContext context) { }
        public virtual void OnDisconnectdHandle(ISocketChannelContext context) { }
        public virtual void OnDispose() { }
        public virtual void OnErrorHandle(ISocketChannelContext context, Exception ex) { }
        public virtual void OnRecvieCompletedHandle(ISocketChannelContext context, IMemory memory) { }
        public virtual void OnSendCompletedHandle(ISocketChannelContext context, IMemory memory) { }
        public virtual void OnTiggerEventHandle(ISocketChannelContext context) { }
    }
}
