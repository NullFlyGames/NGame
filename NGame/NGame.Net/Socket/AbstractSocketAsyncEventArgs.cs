using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace NGame.Net
{
    public class AbstractSocketAsyncEventArgs : SocketAsyncEventArgs
    {

        public ITcpSocket TcpSocket;
        public IMemory _Memory;
        static ConcurrentQueue<AbstractSocketAsyncEventArgs> abstracts_send = new ConcurrentQueue<AbstractSocketAsyncEventArgs>();
        static ConcurrentQueue<AbstractSocketAsyncEventArgs> abstracts_recv = new ConcurrentQueue<AbstractSocketAsyncEventArgs>();
        public static AbstractSocketAsyncEventArgs GetAbstractSocketAsync(ITcpSocket tcp, IMemory recv = null)
        {
            if (!abstracts_send.TryDequeue(out AbstractSocketAsyncEventArgs args))
            {
                args = new AbstractSocketAsyncEventArgs(tcp);
            }
            if (recv == null)
            {
                recv = Memory.GetMemory();
                args.TcpSocket = tcp;
                args.SetMemory(recv, recv.Length);
            }
            else
            {
                args.TcpSocket = tcp;
                args.SetMemory(recv, recv.Offset);
            }
            return args;
        }

        public static void Push(AbstractSocketAsyncEventArgs args)
        {
            args._Memory?.Recycle();
            args._Memory = null;
            args.TcpSocket = null;
            abstracts_send.Enqueue(args);
        }
        public AbstractSocketAsyncEventArgs(ITcpSocket tcp) : this(tcp, null) { }

        public AbstractSocketAsyncEventArgs(ITcpSocket tcp, IMemory memory)
        {
            this.TcpSocket = tcp;
            this._Memory = memory;
            this.Completed += DoCompleted;
            SetMemory(memory);
        }
        static void DoCompleted(object o, SocketAsyncEventArgs args)
        {
            AbstractSocketAsyncEventArgs abstractSocketAsync = (AbstractSocketAsyncEventArgs)args;
            switch (args.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ThreadPool.QueueUserWorkItem((object o1) => { abstractSocketAsync.TcpSocket.OnRecvieCompleted(abstractSocketAsync); });
                    break;
                case SocketAsyncOperation.Send:
                    ThreadPool.QueueUserWorkItem((object o1) => { abstractSocketAsync.TcpSocket.OnSendCompleted(abstractSocketAsync); });
                    break;
                case SocketAsyncOperation.Accept:
                    ThreadPool.QueueUserWorkItem((object o1) => { abstractSocketAsync.TcpSocket.OnAcceptCompleted(abstractSocketAsync); });
                    break;
                case SocketAsyncOperation.Connect:
                    ThreadPool.QueueUserWorkItem((object o1) => { abstractSocketAsync.TcpSocket.OnConnectdCompleted(abstractSocketAsync); });
                    break;
            }
        }

        public void SetMemory(IMemory memory, int length = 0)
        {
            if (memory == null) return;
            if (_Memory != null) _Memory.Recycle();
            _Memory = memory;
            SetBuffer(_Memory.Buffer, 0, length);
        }
    }
}
