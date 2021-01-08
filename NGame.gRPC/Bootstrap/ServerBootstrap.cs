using System.Collections.Generic;
using System.Net;
using NGame.Event;
using NGame.Managed;
using System;

namespace NGame.RPC
{
    public class ServerBootstrap : AbstractBootstapChannel, IManaged
    {
        TcpServerChannel TcpServer;
        IdelStage Stage;

        public void Initlizition()
        {

        }
        public override IAsyncEvent<T> DoBindAsync<T>(IPEndPoint adders)
        {
            TcpServer = new TcpServerChannel();
            TcpServer.OnAccept += Stage.OnConnect;
            TcpServer.OnBind += OnBind;
            TcpServer.OnClose += Dispose;
            TcpServer.OnError += Stage.OnError;
            AsyncEventHandle<T> handle = NCore.GetManaged<EventSystem>().OnCreate<T>(GetHashCode(), 5f);
            TcpServer.DoBind(adders);
            return handle;
        }
        void OnBind()
        {
            NCore.GetManaged<EventSystem>().SetAsyncEventResult(GetHashCode(), TcpServer);
        }
        public override void SetIdelStage(IdelStage stage)
        {
            Stage = stage;
        }

        public void Update(float time)
        {
            Stage.Update(time);
        }

        public void Dispose()
        {
            Stage.OnClose();
        }
    }
}
