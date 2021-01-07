using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NGame.Event;

namespace NGame.NRPC
{
    public class ServerBootstrap : AbstractBootstapChannel
    {
        IHandleChannel Handle;
        TcpServerChannel TcpServer;

        public override IAsyncEvent<T> DoBindAsync<T>(IPEndPoint adders)
        {

            TcpServer = new TcpServerChannel();
            TcpServer.OnAccept += Handle.OnConnectHandle;
            TcpServer.OnBind += OnBind;
            TcpServer.OnClose += Handle.OnDispose ;
            TcpServer.OnDisconnect += Handle.OnDisconnectdHandle;
            TcpServer.OnError += Handle.OnErrorHandle;
            TcpServer.OnRecvie += Handle.OnRecvieCompletedHandle;
            TcpServer.OnSend += Handle.OnSendCompletedHandle;
            AsyncEventHandle<T> handle = NCore.GetManaged<EventSystem>().OnCreate<T>(GetHashCode(), 5f);
            TcpServer.DoBind(adders);
            return handle;
        }
        void OnBind()
        {
            NCore.GetManaged<EventSystem>().SetAsyncEventResult(GetHashCode(), TcpServer);
        }
        public override void SetChannel(IHandleChannel channel)
        {
            Handle = channel;
        }

    }
}
