using HPSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public class SocketChannelContext : ISocketChannelContext
    {
        public ISocket Socket { get; set; }
        public IntPtr SSID { get; set; }
        public int GUID { get; set; }
        public IPEndPoint LocalAddress { get; set; }
        public IPEndPoint RemoteAddress { get; set; }
        public IChannelHandle ChannelHandle { get; set; }
        Queue<IMemory> Memories = new Queue<IMemory>();

        

        public void WriteAndFlush(IMemory memory)
        {
            Write(memory);
            Flush();
        }

        public void Write(IMemory memory)
        {
            Memories.Enqueue(memory);
        }

        public void Flush()
        {
            if (Socket is ITcpServer)
            {
                ITcpServer server = (ITcpServer)Socket;
                while (Memories.Count > 0)
                {
                    IMemory memory = Memories.Dequeue();
                    server.Send(SSID, memory.Buffer, memory.Offset);
                }
            }
            else if (Socket is ITcpClient)
            {
                ITcpClient client = (ITcpClient)Socket;
                while (Memories.Count > 0)
                {
                    IMemory memory = Memories.Dequeue();
                    client.Send(memory.Buffer, memory.Offset);
                }
            }
            
        }
    }
}
