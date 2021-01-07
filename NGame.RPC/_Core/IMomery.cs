using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 内存流
    /// </summary>
    public interface IMemory
    {
        byte[] Buffer { get; }
        int Offset { get; }
        int Length { get; }
        T Read<T>();
        void Read(byte[] bytes, int offset, int length, int disPosition = 0);
        void Write(object o);
        void Write(byte[] bytes, int offset, int length, int disPosition = 0);
        void Recycle();
    }
}
