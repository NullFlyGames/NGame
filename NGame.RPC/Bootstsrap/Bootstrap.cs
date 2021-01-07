using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HPSocket;
using HPSocket.Tcp;
using NGame.Core;

namespace NGame.RPC
{
    public class Bootstrap : AbstractBootstap
    {
        TcpClient TcpClient;
        uint outTime;
        protected SocketChannelContext Context;


        public override IBootstrap CloseAsync()
        {
            if (TcpClient != null) TcpClient.Dispose();
            return this;
        }
        public override IBootstrap ConnectdAsync()
        {
            TcpClient = new TcpClient();
            TcpClient.Address = RemoteAdders.Address.ToString();
            TcpClient.Port = (ushort)RemoteAdders.Port;
            TcpClient.KeepAliveTime = outTime;
            TcpClient.SocketBufferSize = (uint)NCore.BufferSize;

            Context = new SocketChannelContext();
            Context.Socket = TcpClient;
            Context.SSID = TcpClient.ConnectionId;
            Context.GUID = id;
            Context.RemoteAddress = RemoteAdders;
            Context.LocalAddress = LocalAdders;
            Context.ChannelHandle = ChannelHandle;

            TcpClient.OnClose += new ClientCloseEventHandler(OnCloseCompleted);
            TcpClient.OnConnect += new ClientConnectEventHandler(OnConnectdCompleted);
            TcpClient.OnReceive += new ClientReceiveEventHandler(OnRecvieCompleted);
            TcpClient.OnSend += new ClientSendEventHandler(OnSendCompleted);
            TcpClient.Connect();
            return this;
        }
        HandleResult OnCloseCompleted(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            ChannelHandle?.ChannelInactive(Context);
            return HandleResult.Ok;
        }
        HandleResult OnConnectdCompleted(IClient sender)
        {
            ChannelHandle?.ChannelActive(Context);
            return HandleResult.Ok;
        }
        HandleResult OnRecvieCompleted(IClient sender, byte[] bytes)
        {
            Memory memory = Memory.GetMemory();
            memory.Write(bytes, 0, bytes.Length);
            ChannelHandle?.ChannelReadComplete(Context, memory);
            return HandleResult.Ok;
        }
        HandleResult OnSendCompleted(IClient sender, byte[] bytes)
        {
            ChannelHandle?.ChannelWriterCompleted(Context);
            return HandleResult.Ok;
        }

        public override IBootstrap SetLocalAdders(IPEndPoint endPoint)
        {
            LocalAdders = endPoint;
            return this;
        }

        public override IBootstrap SetRemoteAdders(IPEndPoint endPoint)
        {
            RemoteAdders = endPoint;
            return this;
        }

        public override IBootstrap SetTimeOut(uint time)
        {
            outTime = time;
            return this;
        }

        public override IBootstrap SetChannelHandle<T>()
        {
            ChannelHandle = new T();
            return this;
        }

        public override IBootstrap SetDecoderChannel<T>()
        {
            DecoderComparser = new T();
            return this;
        }

        public override IBootstrap SetEncoderChannel<T>()
        {
            EncoderComparer = new T();
            return this;
        }
        public override IBootstrap Flush()
        {
            Context.Flush();
            return this;
        }
        public override IBootstrap Write(IMemory memory)
        {
            Context.Write(memory);
            return this;
        }
        public override IBootstrap WriteAndFlush(IMemory memory)
        {
            Context.WriteAndFlush(memory);
            return this;
        }
    }
}
