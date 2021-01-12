using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame
{
    public interface IMemory
    {
        byte[] Buffer { get; }
        int Offset { get; set; }
        int Length { get; }
        void Recycle();
        void Read(byte[] bytes, int offset, int length);
        void Write(byte[] bytes, int offset, int length);
        T Read<T>();
        void Write(object o);
        IMemory Clone();
        void Refresh();
    }
}
