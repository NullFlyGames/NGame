using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    public class Context 
    {
        private static Dictionary<IMatcher, IGroup> _groups;
        private static Action<IEntity> _entitycomponentchange;
        private static Action<int> _EntityRemove;
        private static Dictionary<int, IEntity> _entitys;
        private static Dictionary<int, List<IComponent>> _components;
        internal static void Initlizition()
        {
            _groups = new Dictionary<IMatcher, IGroup>();
            _entitys = new Dictionary<int, IEntity>();
            _components = new Dictionary<int, List<IComponent>>();
        }

        public static List<IEntity> GetEntities()
        {
            return _entitys.Values.ToList();
        }
        public static List<IEntity> GetGroup(IMatcher matcher)
        {
            if (!_groups.TryGetValue(matcher, out IGroup group))
            {
                group = new Group(matcher);
                _entitycomponentchange += group.HandleEntitySilently;
                _EntityRemove += group.Remove;
                List<IEntity> entities = GetEntities();
                for (int i = 0; i < entities.Count; i++)
                {
                    group.HandleEntitySilently(entities[i]);
                }
                _groups.Add(matcher, group);
            }
            return group.GetEntities();
        }

        /// <summary>
        /// 查找实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static T Find<T>(int id) where T : class, IEntity, new()
        {
            if (_entitys.ContainsKey(id) == false) return null;
            return (T)_entitys[id];
        }

        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <param name="id"></param>
        internal static void Remove(int id)
        {
            if (_entitys.ContainsKey(id) == false) return;
            if (_EntityRemove != null) _EntityRemove(id);
            _entitys.Remove(id);
        }

        /// <summary>
        /// 创建实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static T Create<T>() where T : class, IEntity, new()
        {
            T entity = new T();
            entity.id = entity.GetHashCode();
            _entitys.Add(entity.id, entity);
            return entity;
        }


        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static T AddComponent<T>(IEntity entity) where T : class, IComponent, new()
        {
            T component = entity.GetComponent<T>();
            if (component != null) return component;
            component = new T();
            _components.Add(entity.id, new List<IComponent>() { component });
            if (_entitycomponentchange != null) _entitycomponentchange(entity);
            return component;
        }

        /// <summary>
        /// 获取指定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static T GetComponent<T>(IEntity entity) where T : class, IComponent, new()
        {
            IComponent[] components = entity.GetComponents();
            if (components == null || components.Length <= 0) return null;
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].GetType() != typeof(T)) continue;
                return (T)components[i];
            }
            return null;
        }

        /// <summary>
        /// 获取实体上所有组件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static IComponent[] GetComponents(IEntity entity)
        {
            if (_components.ContainsKey(entity.id) == false) return null;

            return _components[entity.id].ToArray();
        }

        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        internal static void RemoveComponent<T>(IEntity entity) where T : class, IComponent, new()
        {
            T component = entity.GetComponent<T>();
            if (component == null) return;
            if (_entitycomponentchange != null) _entitycomponentchange(entity);
            _components[entity.id].Remove(component);
        }
    }
}
