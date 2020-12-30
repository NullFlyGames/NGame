using System;
using System.Collections.Concurrent;

namespace NGame.ObjectPool
{
    public sealed class ObjectPool<T> : Core.IObjectPool
    {
        private readonly Func<T> _onCreate;
        private readonly Action<T> _OnDestroy;
        private readonly Action<T> _OnRelease;
        private readonly ConcurrentStack<T> _stack;
        public Type Owner { get; } = typeof(T);

        public ObjectPool(Func<T> onCreate, Action<T> OnDestroy, Action<T> OnRelease)
        {
            _onCreate = onCreate;
            _OnDestroy = OnDestroy;
            _OnRelease = OnRelease;
            _stack = new ConcurrentStack<T>();
        }

        public void Push(T o)
        {
            _OnRelease?.Invoke(o);
            _stack.Push(o);
        }

        public T Pop()
        {
            if (_stack.TryPop(out T result))
            {
                return result;
            }
            return _onCreate();
        }

        public void Clear()
        {
            while (_stack.TryPop(out T result))
            {
                _OnDestroy(result);
            }
        }
    }
}
