namespace NGame.NSocket.Session
{
    using NGame.Memorys;
    using NGame.RPC;
    using System.Net.Sockets;
    public sealed class TcpSession<Handle> : ISocket<Handle> where Handle : IHandle
    {
        public int id { get; private set; }
        public string Adders { get; private set; }
        public ushort Port { get; private set; }
        public Handle Handles { get; private set; }
        public Socket Socket { get; set; }



        public bool Connect(string adder, ushort port)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TrySend(Memory memory)
        {
            return true;
        }
    }
}
