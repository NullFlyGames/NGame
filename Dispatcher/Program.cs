using System;
using System.Net;
using System.Threading.Tasks;
using NGame;
using NGame.NRPC;

namespace Dispatcher
{

    public class DispatcherHandle : AbstractHandleChannel
    {
        public override void OnConnectHandle(ISocketChannelContext context)
        {
            Ex.Log($"连接进入：{context.id}  {context.Adders.ToString()}");
        }
        public override void OnRecvieCompletedHandle(ISocketChannelContext context, IMemory memory)
        {
            Ex.Log($"收到数据：{context.Adders.ToString()} {memory.ToString()} offset:{memory.Offset}");
            context.WriteAndFlush(memory.Clone());
        }
        public override void OnDisconnectdHandle(ISocketChannelContext context)
        {
            Ex.Log($"连接断开：{context.id}  {context.Adders.ToString()}");
        }
        public override void OnSendCompletedHandle(ISocketChannelContext context, IMemory memory)
        {
            Ex.Log($"发送完成：{context.id}  {context.Adders.ToString()} length:{memory.Offset}");
        }
    }
    class Program
    {

        static float times = 0;
        static ServerBootstrap bootstrap;
        static void Main(string[] args)
        {
            Console.Title = "分发器";
            NCore.Initlizition();
            Star();
            while (true)
            {
                NCore.FixedUpdate(times);
                System.Threading.Thread.Sleep(10);
                times += 0.001f;
            }
        }

        static async void Star()
        {
            bootstrap = new ServerBootstrap();
            bootstrap.SetChannel(new DispatcherHandle());
            ISocketChannelContext channel = await bootstrap.DoBindAsync<ISocketChannelContext>(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));

        }
    }
}
