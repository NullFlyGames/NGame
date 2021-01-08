using NGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.RPC
{
    public class Bootstrap : AbstractBootstapChannel, IManaged
    {
        TcpClientChannel TcpClient;
        IdelStage Stage;

        public void Initlizition()
        {
            
        }

        public override IAsyncEvent<T> DoConnectdAsync<T>(IPEndPoint adders)
        {
            AsyncEventHandle<T> handle = NCore.GetManaged<EventSystem>().OnCreate<T>(GetHashCode(), 5f);
            TcpClient = new TcpClientChannel();
            TcpClient.OnClose += OnClose;
            TcpClient.OnConnectd += OnConnectd;
            TcpClient.DoConnectd(adders);
            return handle;
        }
        void OnConnectd(TcpClientChannel channel)
        {
            NCore.GetManaged<EventSystem>().SetAsyncEventResult(GetHashCode(), TcpClient);
            Stage.OnConnect(channel);
        }
        void OnClose(TcpClientChannel channel)
        {
            Dispose();
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
            Stage = null;
            TcpClient = null;
        }
    }
}
