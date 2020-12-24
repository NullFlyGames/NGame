namespace NGame.NSocket.Core
{
    using NGame.NCore;
    public interface ISocket : IEntity
    {
        string Adders { get; set; }
        ushort Port { get; set; }

    }
}
