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
        public virtual void ChannelActive(IChannel context) { }
        public virtual void ChannelBindingCompleted(IChannel context) { }
        public virtual void ChannelConnectdCompleted(IChannel context) { }
        public virtual void ChannelInactive(IChannel context) { }
        public virtual void ChannelReadComplete(IChannel context, IMemory message) { }
        public virtual void ChannelWriterCompleted(IChannel channelHandleContext) { }
        public virtual void ExceptionCaught(IChannel context, Exception exception) { }
    }
}
