using HPSocket;
using HPSocket.Tcp;
using NGame.ECS;
using System;
using System.Net;

namespace NGame.RPC
{
    public class ServerBootstap : AbstractBootstap
    {
        TcpServer TcpServer;
        uint outTime;

        public int id { get; set; }

        public override IBootstrap BindAsync()
        {
            TcpServer = new TcpServer();
            TcpServer.Port = (ushort)LocalAdders.Port;
            TcpServer.Address = LocalAdders.Address.ToString();
            TcpServer.OnAccept += new ServerAcceptEventHandler(AcceptCompleted);
            TcpServer.OnClose += new ServerCloseEventHandler(OnCloseCompleted);
            TcpServer.OnReceive += new ServerReceiveEventHandler(OnRecvieCompleted);
            TcpServer.OnSend += new ServerSendEventHandler(OnSendCompleted);
            TcpServer.OnShutdown += new ServerShutdownEventHandler(OnShutdown);
            TcpServer.WorkerThreadCount = (uint)Environment.ProcessorCount * 2;
            TcpServer.KeepAliveTime = outTime;
            TcpServer.Start();
            TcpServer.Wait();
            return this;
        }
        public override IBootstrap CloseAsync()
        {
            TcpServer.Dispose();
            return this;
        }
        public override IBootstrap SetLocalAdders(IPEndPoint endPoint)
        {
            LocalAdders = endPoint;
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

        public HandleResult AcceptCompleted(IServer sender, IntPtr connId, IntPtr client)
        {
            SocketChannelContext channel = sender.GetExtra<SocketChannelContext>(connId);
            if (channel == null)
            {
                channel = Activator.CreateInstance<SocketChannelContext>();
                sender.SetExtra(connId, channel);
                sender.GetRemoteAddress(connId, out string ip, out ushort port);
                channel.GUID = Guid.NewGuid().GetHashCode();
                channel.SSID = connId;
                channel.Socket = sender;
                channel.RemoteAddress = new IPEndPoint(IPAddress.Parse(ip), port);
                channel.LocalAddress = LocalAdders;
                channel.ChannelHandle = ChannelHandle;
            }
            ChannelHandle.ChannelConnectdCompleted(channel);
            return HandleResult.Ok;
        }
        HandleResult OnRecvieCompleted(IServer sender, IntPtr connId, byte[] data)
        {
            SocketChannelContext channel = sender.GetExtra<SocketChannelContext>(connId);
            if (channel == null)
            {
                ChannelHandle.ExceptionCaught(channel, new Exception($"not find this connectd id:{connId}"));
                return HandleResult.Ok;
            }
            Memory memory = Memory.GetMemory();
            memory.Write(data, 0, data.Length);
            ChannelHandle?.ChannelReadComplete(channel, memory);
            return HandleResult.Ok;
        }
        HandleResult OnSendCompleted(IServer sender, IntPtr connId, byte[] data)
        {
            SocketChannelContext channel = sender.GetExtra<SocketChannelContext>(connId);
            if (channel == null)
            {
                ChannelHandle.ExceptionCaught(channel, new Exception($"not find this connectd id:{connId}"));
                return HandleResult.Ok;
            }
            ChannelHandle.ChannelWriterCompleted(channel);
            return HandleResult.Ok;
        }
        HandleResult OnCloseCompleted(IServer sender, IntPtr connId, SocketOperation socketOperation, int errorCode)
        {
            SocketChannelContext channel = sender.GetExtra<SocketChannelContext>(connId);
            if (channel == null)
            {
                ChannelHandle.ExceptionCaught(channel, new Exception($"not find this connectd id:{connId}"));
                return HandleResult.Ok;
            }
            ChannelHandle.ChannelDisconnectdCompleted(channel);
            sender.RemoveExtra(connId);
            return HandleResult.Ok;
        }
        HandleResult OnShutdown(IServer sender)
        {
            ChannelHandle.ExceptionCaught(null, new Exception("shutdown"));
            return HandleResult.Ok;
        }
    }
}
