using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.NCore;

public delegate void EntityComponentChangeEvent(IEntity entity, IComponent component);
public delegate void EntityDestoryEvent(IEntity entity);
public delegate void EntityCreateEvent(IEntity entity);


/// <summary>
/// 框架核心
/// </summary>
public sealed partial class NCore:IDisposable
{
    private static Dictionary<int, IEntity> _entitys;
    private static Dictionary<int, List<IComponent>> _components;

    private static List<ExecuteSystem<IMatcher>> _system;
    private static IContext _contexts;

    public NCore()
    {
        Initlizition();
    }

    public static void Initlizition()
    {
        _system = new List<ExecuteSystem<IMatcher>>();
        _entitys = new Dictionary<int, IEntity>();
        _components = new Dictionary<int, List<IComponent>>();
    }

    /// <summary>
    /// 加载逻辑系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadSystem<T>() where T : class, ExecuteSystem<IMatcher>, new()
    {
        T sys = new T();
        sys.Initialize();
        _system.Add(sys);
        return sys;
    }

    /// <summary>
    /// 卸载逻辑系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void UnLoadSystem<T>() where T : class, ISystem, new()
    {
        for (int i = _system.Count - 1; i >= 0; i--)
        {
            ISystem system = _system[i];
            if (system == null || system.GetType() != typeof(T)) continue;
            system.Dispose();
        }
    }

    /// <summary>
    /// 查找实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T Find<T>(int id) where T : class, IEntity, new()
    {
        if (_entitys.ContainsKey(id) == false) return null;
        return (T)_entitys[id];
    }

    /// <summary>
    /// 移除实体对象
    /// </summary>
    /// <param name="id"></param>
    public static void Remove(int id)
    {
        if (_entitys.ContainsKey(id) == false) return;
        _entitys.Remove(id);
    }

    /// <summary>
    /// 创建实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Create<T>() where T : class, IEntity, new()
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

        _components[entity.id].Remove(component);
    }

    public static List<IEntity> GetGroup(IMatcher matcher)
    {
        return _contexts.GetGroup(matcher);
    }

    /// <summary>
    /// 轮询系统
    /// </summary>
    public static void FixedUpdate()
    {
        for (int i = _system.Count - 1; i >= 0; i--)
        {
            _system[i].Execute();
        }
    }

    public void Dispose()
    {
    }
}
