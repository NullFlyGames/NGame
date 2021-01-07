using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.Component
{
    public class ComponentManaged : IManaged
    {
        Dictionary<int, List<IComponent>> _components;
        public void Initlizition()
        {
            _components = new Dictionary<int, List<IComponent>>();
            NCore.GetManaged<Event.EventSystem>().ListenerEvent(CreateEntityCallback);
        }



        public void Update(float time) { }


        void CreateEntityCallback(Event.IEventArgs args)
        {

        }

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddComponent<T>(int id) where T : class, IComponent, new()
        {
            T component = GetComponent<T>(id);
            if (component != null) return component;

            component = new T();
            _components[id].Add(component);
            return component;
        }

        /// <summary>
        /// 获取指定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T GetComponent<T>(int id) where T : class, IComponent, new()
        {
            if (_components.ContainsKey(id))
                throw new NullReferenceException();
            for (int i = 0; i < _components[id].Count; i++)
            {
                if (_components[id][i].GetType() == typeof(T))
                    return (T)_components[id][i];
            }
            return null;
        }

        /// <summary>
        /// 获取实体上所有组件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IComponent[] GetComponents(int id)
        {
            if (_components.ContainsKey(id))
                throw new NullReferenceException();
            return _components[id].ToArray();
        }

        public void RemoveComponent<T>(int id) where T : class, IComponent, new()
        {
            T component = GetComponent<T>(id);
            if (component == null) return;

            _components[id].Remove(component);
        }
        public void Dispose()
        {
            _components.Clear();
        }

    }
}
