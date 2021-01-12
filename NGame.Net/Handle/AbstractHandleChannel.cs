using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Net
{
    public abstract class AbstractHandleChannel : IHandleChannel
    {
        public virtual void OnConnectHandle(ISocketChannelContext context) { }
        public virtual void OnDisconnectdHandle(ISocketChannelContext context) { }
        public virtual void OnDispose() { }
        public virtual void OnErrorHandle(ISocketChannelContext context, Exception ex) { }
        public virtual void OnReadCompletedHandle(ISocketChannelContext context, IMemory memory) { }
        public virtual void OnWriteCompletedHandle(ISocketChannelContext context, IMemory memory) { }
        public virtual void OnTiggerEventHandle(ISocketChannelContext context) { }
    }
}
