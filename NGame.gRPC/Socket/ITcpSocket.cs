using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NGame.RPC
{
    public interface ITcpSocket : ISocketChannelContext, IDisposable
    {
        void DoBind(IPEndPoint adder);
        void DoConnectd(IPEndPoint adder);
        void OnConnectdCompleted(AbstractSocketAsyncEventArgs args);
        void DoAccept(object o);
        void OnAcceptCompleted(AbstractSocketAsyncEventArgs args);
        void DoRecvie(object o);
        void OnRecvieCompleted(AbstractSocketAsyncEventArgs args);
        void DoSend(object o);
        void OnSendCompleted(AbstractSocketAsyncEventArgs args);
    }
}
