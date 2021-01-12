using NGame.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Entity
{
    public abstract class AbsttactEntity : IEntity
    {
        public abstract int id { get; }
        private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddComponent<T>() where T : class, IComponent, new()
        {
            T component = GetComponent<T>();
            if (component != null) return component;

            component = new T();
            _components.Add(typeof(T),component);
            return component;
        }

        /// <summary>
        /// 获取指定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T GetComponent<T>() where T : class, IComponent, new()
        {
            if (_components.ContainsKey(typeof(T)))
                throw new NullReferenceException();

            return (T)_components[typeof(T)];
        }

        /// <summary>
        /// 获取实体上所有组件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IComponent[] GetComponents()
        {
            return _components.Values.ToArray();
        }

        public void RemoveComponent<T>() where T : class, IComponent, new()
        {
            T component = GetComponent<T>();
            if (component == null) return;

            _components.Remove(typeof(T));
        }
    }
}
