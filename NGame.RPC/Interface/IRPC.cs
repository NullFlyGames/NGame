using NGame.Memorys;

namespace NGame.RPC
{
    public interface IRPC
    {
        void Packaged(Memory memory);
        void UnPackaged(Memory memory);
    }
}
