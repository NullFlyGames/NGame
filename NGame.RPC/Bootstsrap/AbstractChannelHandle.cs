using NGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public abstract class AbstractChannelHandle : IChannelHandle
    {
        public virtual void ChannelActive(ISocketChannelContext context) { }
        public virtual void ChannelBindingCompleted(ISocketChannelContext context) { }
        public virtual void ChannelConnectdCompleted(ISocketChannelContext context) { }
        public virtual void ChannelInactive(ISocketChannelContext context) { }
        public virtual void ChannelReadComplete(ISocketChannelContext context, IMemory message) { }
        public virtual void ChannelWriterCompleted(ISocketChannelContext channelHandleContext) { }
        public virtual void ExceptionCaught(ISocketChannelContext context, Exception exception) { }
        public virtual void ChannelDisconnectdCompleted(ISocketChannelContext channelHandleContext) { }

    }
}
