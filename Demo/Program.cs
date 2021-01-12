using System;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using NGame.Net;
using NGame;

namespace Demo
{
    class Program
    {
        public class DispatcherHandle : AbstractHandleChannel
        {
            public override void OnConnectHandle(ISocketChannelContext context)
            {
                Ex.Log($"连接完成：{context.Local.ToString()}  {context.Adders.ToString()}");
                IMemory memory = Memory.GetMemory();
                memory.Write("哈哈哈哈哈");
                context.WriteAndFlush(memory);
            }
            public override async void OnReadCompletedHandle(ISocketChannelContext context, IMemory memory)
            {
                string info = memory.Read<string>();
                Ex.Log($"收到数据：{context.Adders.ToString()} offset:{memory.Offset} {info}");
                if (info.Contains("心跳检测"))
                {
                    return;
                }
                IMemory m = memory.Clone();
                await Task.Delay(1000);
                context.WriteAndFlush(m);
            }
            public override void OnWriteCompletedHandle(ISocketChannelContext context, IMemory memory)
            {
                memory.Refresh();
                Ex.Log($"数据发送完成：{context.Local.ToString()} {context.id} {memory.Read<string>()}");
            }
            public override void OnTiggerEventHandle(ISocketChannelContext context)
            {
                IMemory memory = Memory.GetMemory();
                memory.Write("心跳检测");
                context.WriteAndFlush(memory);
            }
        }

        static float times = 0;
        static List<Bootstrap> bootstraps = new List<Bootstrap>();
        static void Main(string[] args)
        {
            NCore.Initlizition();
            NetworkMonitor monitor = new NetworkMonitor();
            monitor.StartMonitoring();
            OpenSocket(1000);
            while (true)
            {
                NCore.FixedUpdate(times);
                bootstraps.ForEach(a => { a.Update(times); });
                Console.Title = $"客户端 接收：{monitor.GetRecvieSpeed()} 发送：{monitor.GetSendSpeed()}";
                System.Threading.Thread.Sleep(10);
                times += 0.01f;
            }
        }

        static void OpenSocket(int count)
        {
            async void Connectd()
            {
                Bootstrap bootstrap = new Bootstrap();
                bootstraps.Add(bootstrap);
                bootstrap.SetIdelStage(new IdelStage(IdelState.Read, new DispatcherHandle(), 2));
                ISocketChannelContext context = await bootstrap.DoConnectdAsync<ISocketChannelContext>(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            }
            for (int i = 0; i < count; i++)
            {
                Connectd();
            }
        }
    }
}