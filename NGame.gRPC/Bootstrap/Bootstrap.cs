using NGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NRPC
{
    public class Bootstrap : AbstractBootstapChannel
    {
        TcpClientChannel TcpClient;
        IHandleChannel HandleChannel;
        public override IAsyncEvent<T> DoConnectdAsync<T>(IPEndPoint adders)
        {
            AsyncEventHandle<T> handle = NCore.GetManaged<EventSystem>().OnCreate<T>(GetHashCode(), 5f);
            TcpClient = new TcpClientChannel();
            TcpClient.OnClose += OnClose;
            TcpClient.OnConnectd += OnConnectd;
            TcpClient.OnError += HandleChannel.OnErrorHandle;
            TcpClient.OnRecvie += HandleChannel.OnRecvieCompletedHandle;
            TcpClient.OnSend += HandleChannel.OnSendCompletedHandle;
            TcpClient.DoConnectd(adders);
            return handle;
        }
        void OnConnectd(TcpClientChannel channel)
        {
            NCore.GetManaged<EventSystem>().SetAsyncEventResult(GetHashCode(), TcpClient);
            HandleChannel.OnConnectHandle(TcpClient);
        }
        void OnClose(TcpClientChannel channel)
        {
            HandleChannel.OnDispose();
        }
        public override void SetPingpong(int time)
        {
            base.SetPingpong(time);
        }
        public override void SetChannel(IHandleChannel channel)
        {
            HandleChannel = channel;
        }
    }
}
