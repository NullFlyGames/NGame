using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HPSocket;
using HPSocket.Tcp;

namespace NGame.RPC
{
    public class Bootstrap : IBootstrap
    {
        TcpServer TcpServer;
        TcpClient TcpClient;
        uint outTime;
        Type _channelType;

        IChannel Channel;
        /// <summary>
        /// 远程地址
        /// </summary>
        public IPEndPoint RemoteAdders { get; protected set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        public IPEndPoint LocalAdders { get; protected set; }
        /// <summary>
        /// 消息处理管道
        /// </summary>
        public IChannelHandle ChannelHandle { get; protected set; }
        /// <summary>
        /// 数据解码组件
        /// </summary>
        public IDecoderComparserChannel DecoderComparser { get; protected set; }
        /// <summary>
        /// 数据编码组件
        /// </summary>
        public IEncoderComparerChannel EncoderComparer { get; protected set; }



        public IBootstrap BindAsync()
        {
            TcpServer = new TcpServer();
            TcpServer.Port = (ushort)LocalAdders.Port;
            TcpServer.Address = LocalAdders.Address.ToString();
            TcpServer.OnAccept += new ServerAcceptEventHandler(AcceptCompleted);
            TcpServer.WorkerThreadCount = (uint)Environment.ProcessorCount * 2;
            TcpServer.KeepAliveTime = outTime;
            TcpServer.Start();
            TcpServer.Wait();
            return this;
        }
        HandleResult AcceptCompleted(IServer sender, IntPtr connId, IntPtr client)
        {
            IChannel channel = sender.GetExtra<IChannel>(connId);
            if (channel == null)
            {
                channel = (IChannel)Activator.CreateInstance(_channelType);
                sender.SetExtra(connId, channel);
                channel.GUID = Guid.NewGuid().GetHashCode();
                channel.SSID = connId;
                channel.LocalAddress = LocalAdders;
                sender.GetRemoteAddress(connId, out string ip, out ushort port);
                channel.RemoteAddress = new IPEndPoint(IPAddress.Parse(ip), port);
            }
            ChannelHandle.ChannelConnectdCompleted(channel);
            return HandleResult.Ok;
        }
        public IBootstrap CloseAsync()
        {
            if (TcpClient != null) TcpClient.Dispose();
            if (TcpServer != null) TcpServer.Dispose();
            return this;
        }

        public IBootstrap ConnectdAsync()
        {
            TcpClient = new TcpClient();
            TcpClient.Address = RemoteAdders.Address.ToString();
            TcpClient.Port = (ushort)RemoteAdders.Port;
            TcpClient.KeepAliveTime = outTime;
            TcpClient.OnClose += new ClientCloseEventHandler(OnCloseCompleted);
            TcpClient.OnConnect += new ClientConnectEventHandler(OnConnectdCompleted);
            TcpClient.OnReceive += new ClientReceiveEventHandler(OnRecvieCompleted);
            TcpClient.OnSend += new ClientSendEventHandler(OnSendCompleted);
            TcpClient.Connect();
            return this;
        }
        HandleResult OnCloseCompleted (IClient sender, SocketOperation socketOperation, int errorCode)
        {
            if (Channel == null)
            {
                Channel = (IChannel)Activator.CreateInstance(_channelType);
                Channel.GUID = Guid.NewGuid().GetHashCode();
                Channel.SSID = sender.ConnectionId;
                Channel.LocalAddress = LocalAdders;
                Channel.RemoteAddress = RemoteAdders;
            }
            ChannelHandle?.ChannelActive(Channel);
            return HandleResult.Ok;
        }

        HandleResult OnConnectdCompleted(IClient sender)
        {
            if (Channel == null)
            {
                Channel = (IChannel)Activator.CreateInstance(_channelType);
                Channel.GUID = Guid.NewGuid().GetHashCode();
                Channel.SSID = sender.ConnectionId;
                Channel.LocalAddress = LocalAdders;
                Channel.RemoteAddress = RemoteAdders;
            }
            ChannelHandle?.ChannelActive(Channel);
            return HandleResult.Ok;
        }
        HandleResult OnRecvieCompleted(IClient sender,byte[]bytes)
        {
            if (Channel == null)
            {
                Channel = (IChannel)Activator.CreateInstance(_channelType);
                Channel.GUID = Guid.NewGuid().GetHashCode();
                Channel.SSID = sender.ConnectionId;
                Channel.LocalAddress = LocalAdders;
                Channel.RemoteAddress = RemoteAdders;
            }
            Memory memory = Memory.GetMemory();
            memory.Write(bytes, 0, bytes.Length);
            ChannelHandle?.ChannelReadComplete(Channel, memory);
            return HandleResult.Ok;
        }
        HandleResult OnSendCompleted(IClient sender, byte[] bytes)
        {
            if (Channel == null)
            {
                Channel = (IChannel)Activator.CreateInstance(_channelType);
                Channel.GUID = Guid.NewGuid().GetHashCode();
                Channel.SSID = sender.ConnectionId;
                Channel.LocalAddress = LocalAdders;
                Channel.RemoteAddress = RemoteAdders;
            }
            ChannelHandle?.ChannelWriterCompleted(Channel);
            return HandleResult.Ok;
        }
        public IBootstrap SetLocalAdders(IPEndPoint endPoint)
        {
            LocalAdders = endPoint;
            return this;
        }

        public IBootstrap SetRemoteAdders(IPEndPoint endPoint)
        {
            RemoteAdders = endPoint;
            return this;
        }

        public IBootstrap SetTimeOut(uint time)
        {
            outTime = time;
            return this;
        }

        public IBootstrap SetChannel<T>() where T : class, IChannel, new()
        {
            _channelType = typeof(T);
            return this;
        }

        public IBootstrap SetChannelHandle<T>() where T : class, IChannelHandle, new()
        {
            ChannelHandle = new T();
            return this;
        }

        public IBootstrap SetDecoderChannel<T>() where T : class, IDecoderComparserChannel, new()
        {
            DecoderComparser = new T();
            return this;
        }

        public IBootstrap SetEncoderChannel<T>() where T : class, IEncoderComparerChannel, new()
        {
            EncoderComparer = new T();
            return this;
        }
    }
}
