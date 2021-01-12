using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Net
{
    public interface IHandleChannel
    {
        void OnConnectHandle(ISocketChannelContext context);
        void OnReadCompletedHandle(ISocketChannelContext context,IMemory memory);
        void OnWriteCompletedHandle(ISocketChannelContext context,IMemory memory);
        void OnTiggerEventHandle(ISocketChannelContext context);
        void OnDisconnectdHandle(ISocketChannelContext context);
        void OnErrorHandle(ISocketChannelContext context,Exception ex);
        void OnDispose();
    }
}
