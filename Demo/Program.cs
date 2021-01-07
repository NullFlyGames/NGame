using System;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using NGame.NRPC;
using NGame;

namespace Demo
{
    class Program
    {
        public class DispatcherHandle : AbstractHandleChannel
        {
            public override async void OnConnectHandle(ISocketChannelContext context)
            {
                Ex.Log($"连接完成：{context.Local.ToString()}  {context.Adders.ToString()}");
                IMemory memory = Memory.GetMemory();
                memory.Write("哈哈哈哈哈");
                context.WriteAndFlush(memory);
            }
            public override void OnRecvieCompletedHandle(ISocketChannelContext context, IMemory memory)
            {
                Ex.Log($"收到数据：{context.Adders.ToString()} offset:{memory.Offset} {memory.ToString()} ");
            }
            public override void OnSendCompletedHandle(ISocketChannelContext context, IMemory memory)
            {
                //Ex.Log($"数据发送完成：{context.Local.ToString()} {context.id} {memory.ToString()}");
            }
        }

        static float times = 0;
        static List<Bootstrap> bootstraps = new List<Bootstrap>();
        static void Main(string[] args)
        {
            NCore.Initlizition();
            OpenSocket(10);
            while (true)
            {
                NCore.FixedUpdate(times);
                System.Threading.Thread.Sleep(10);
                times += 0.001f;
            }
        }

        static void OpenSocket(int count)
        {
            async void Connectd()
            {
                Bootstrap bootstrap = new Bootstrap();
                bootstraps.Add(bootstrap);
                bootstrap.SetChannel(new DispatcherHandle());
                ISocketChannelContext context = await bootstrap.DoConnectdAsync<ISocketChannelContext>(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                await Task.Delay(1000);
            }
            for (int i = 0; i < count; i++)
            {
                Connectd();
            }
        }
    }
}