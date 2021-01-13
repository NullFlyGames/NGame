using System;
using System.Net;
using System.Threading.Tasks;
using NGame;
using NGame.Net;

namespace NGame.Gateway
{
    public class DispatcherHandle : AbstractHandleChannel
    {
        public override void OnConnectHandle(ISocketChannelContext context)
        {
            Ex.Log($"连接进入：{context.id}  {context.Adders.ToString()}");
        }
        public override void OnReadCompletedHandle(ISocketChannelContext context, IMemory memory)
        {
            string info = memory.Read<string>();
            Ex.Log($"收到数据：{context.Adders.ToString()} offset:{memory.Offset} {info}");
            if (info.Contains("心跳检测"))
            {
                return;
            }
            context.WriteAndFlush(memory.Clone());
        }
        public override void OnDisconnectdHandle(ISocketChannelContext context)
        {
            //Ex.Log($"连接断开：{context.id}  {context.Adders.ToString()}");
        }
        public override void OnWriteCompletedHandle(ISocketChannelContext context, IMemory memory)
        {
            //Ex.Log($"发送完成：{context.id}  {context.Adders.ToString()} length:{memory.Offset}");
        }
        public override void OnTiggerEventHandle(ISocketChannelContext context)
        {
            Ex.Log($"{context.id}  {context.Adders.ToString()} 心跳检测超时");
        }
    }
    class Program
    {

        static float times = 0;
        static ServerBootstrap bootstrap;
        static void Main(string[] args)
        {
            

            NetworkMonitor monitor = new NetworkMonitor();
            monitor.StartMonitoring();
            NCore.Initlizition();
            Star();
            while (true)
            {
                NCore.FixedUpdate(times);
                Console.Title = $"分发器 接收：{monitor.GetRecvieSpeed()} 发送：{monitor.GetSendSpeed()}";
                System.Threading.Thread.Sleep(4);
                times += 0.01f;
            }
        }

        static async void Star()
        {
            bootstrap = NCore.LoadManaged<ServerBootstrap>();
            bootstrap.SetIdelStage(new IdelStage(IdelState.Read, new DispatcherHandle(), 10));
            ISocketChannelContext channel = await bootstrap.DoBindAsync<ISocketChannelContext>(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));

        }
    }
}
