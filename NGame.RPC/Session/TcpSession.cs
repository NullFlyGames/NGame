namespace NGame.NSocket.Session
{
    using System.RPC
    public sealed class TcpSession : Core.ISocket
    {
        public int id { get; private set; }
        public string Adders { get; private set; }
        public ushort Port { get; private set; }
        private Socket Socket { get; set; }

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

        public bool TrySend(Core.Expansion.Memory memory)
        {
            return true;
        }
    }
}
