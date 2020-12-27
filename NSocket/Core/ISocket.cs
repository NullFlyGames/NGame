namespace NGame.NSocket.Core
{
    using NGame.NCore;
    public interface ISocket
    {
        int id { get; }
        string Adders { get; }
        ushort Port { get; }
    }
}
