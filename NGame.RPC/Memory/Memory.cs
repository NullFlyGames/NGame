using NGame.ObjectPool;
using System;
using System.Text;


namespace NGame.RPC
{
    public sealed class Memory : IMemory, IDisposable
    {
        static ObjectPool<Memory> MemoryPool;
        private bool disposedValue;

        public byte[] Buffer { get; private set; }
        public int Offset { get; private set; }
        public int Length { get; private set; }

        static Memory OnCreate()
        {
            Memory memory = new Memory();
            memory.Buffer = new byte[4096];
            memory.Offset = 0;
            memory.Length = memory.Buffer.Length;
            return memory;
        }
        static void OnDestory(Memory memory)
        {
            memory.Buffer = null;
            memory.Offset = 0;
            memory.Length = 0;
        }

        static void OnRelease(Memory memory)
        {
            memory.Offset = 0;
        }

        public static Memory GetMemory()
        {
            if (MemoryPool == null)
            {
                MemoryPool = new ObjectPool<Memory>(OnCreate, OnDestory, OnRelease);
            }
            return MemoryPool.Pop();
        }
        public void Read(byte[] bytes, int offset, int length, int disPosition = 0)
        {
            System.Buffer.BlockCopy(Buffer, Offset, bytes, offset, length);
            Offset += length;
        }
        public void Write(byte[] bytes, int offset, int length, int disPosition = 0)
        {
            System.Buffer.BlockCopy(bytes, offset, Buffer, Offset, length);
            Offset += length;
        }
        public T Read<T>()
        {
            if (typeof(T) == typeof(short))
            {
                short a = BitConverter.ToInt16(Buffer, Offset);
                Offset += sizeof(short);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(int))
            {
                int a = BitConverter.ToInt32(Buffer, Offset);
                Offset += sizeof(int);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(long))
            {
                long a = BitConverter.ToInt64(Buffer, Offset);
                Offset += sizeof(long);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(float))
            {
                float a = BitConverter.ToSingle(Buffer, Offset);
                Offset += sizeof(float);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(ushort))
            {
                ushort a = BitConverter.ToUInt16(Buffer, Offset);
                Offset += sizeof(ushort);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(uint))
            {
                uint a = BitConverter.ToUInt32(Buffer, Offset);
                Offset += sizeof(uint);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong a = BitConverter.ToUInt64(Buffer, Offset);
                Offset += sizeof(ulong);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(double))
            {
                double a = BitConverter.ToDouble(Buffer, Offset);
                Offset += sizeof(double);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            if (typeof(T) == typeof(string))
            {
                int length = Read<int>();
                string info = Encoding.UTF8.GetString(Buffer, Offset, length);
                Offset += length;
                return (T)Convert.ChangeType(info, typeof(T));
            }
            if (typeof(T) == typeof(bool))
            {
                bool a = BitConverter.ToBoolean(Buffer, Offset);
                Offset += sizeof(bool);
                return (T)Convert.ChangeType(a, typeof(T));
            }
            return default;
        }
        public void Write(object o)
        {
            if (o == null)
                throw new NullReferenceException();
            switch (o)
            {
                case short @short:
                    {
                        Write(BitConverter.GetBytes(@short), 0, sizeof(short));
                        return;
                    }
                case int @int:
                    {
                        Write(BitConverter.GetBytes(@int), 0, sizeof(int));
                        return;
                    }
                case long @long:
                    {
                        Write(BitConverter.GetBytes(@long), 0, sizeof(long));
                        return;
                    }
                case float single:
                    {
                        Write(BitConverter.GetBytes(single), 0, sizeof(float));
                        return;
                    }
                case ushort @ushort:
                    {
                        Write(BitConverter.GetBytes(@ushort), 0, sizeof(ushort));
                        return;
                    }
                case uint @uint:
                    {
                        Write(BitConverter.GetBytes(@uint), 0, sizeof(uint));
                        return;
                    }
                case ulong @ulong:
                    {
                        Write(BitConverter.GetBytes(@ulong), 0, sizeof(ulong));
                        return;
                    }
                case double @double:
                    {
                        Write(BitConverter.GetBytes(@double), 0, sizeof(double));
                        return;
                    }
                case string @string:
                    {
                        byte[] b = Encoding.UTF8.GetBytes(o.ToString());
                        Write(b.Length);
                        Write(b, 0, b.Length);
                        return;
                    }
                case bool boolean:
                    {
                        Write(BitConverter.GetBytes(boolean), 0, sizeof(bool));
                        return;
                    }
            }
        }
        public void Recycle()
        {
            Offset = 0;
            MemoryPool.Push(this);
        }

        public override string ToString()
        {
            return UTF8Encoding.UTF8.GetString(Buffer, 0, Offset);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }
                Recycle();
                disposedValue = true;
            }
        }

        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~Memory()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
