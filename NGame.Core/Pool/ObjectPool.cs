using System;
using System.Collections.Generic;

namespace NGame.ObjectPool
{
    public sealed class ObjectPool<T> : Core.IObjectPool
    {
        private readonly Func<T> _onCreate;
        private readonly Action<T> _OnDestroy;
        private readonly Action<T> _OnRelease;
        private readonly Stack<T> _stack;
        public Type Owner { get; } = typeof(T);

        public ObjectPool(Func<T> onCreate, Action<T> OnDestroy, Action<T> OnRelease)
        {
            _onCreate = onCreate;
            _OnDestroy = OnDestroy;
            _OnRelease = OnRelease;
            _stack = new Stack<T>();
        }

        public void Push(T o)
        {
            _OnRelease?.Invoke(o);
            _stack.Push(o);
        }

        public T Pop()
        {
            if (_stack.Count <= 0) return _onCreate();
            return _stack.Pop();
        }

        public void Clear()
        {
            T o;
            while ((o = _stack.Pop()) != null)
            {
                _OnDestroy?.Invoke(o);
            }
        }
    }
}
