using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Core;

public delegate void LogEvent(object o);

/// <summary>
/// 框架核心
/// </summary>
public sealed partial class NCore : IDisposable
{
    private static List<ExecuteSystem> _system;
    internal static LogEvent logEvent;

    public static event LogEvent LogEvents
    {
        add { logEvent += value; }
        remove { logEvent -= value; }
    }

    public static void Initlizition()
    {
        _system = new List<ExecuteSystem>();
        Context.Initlizition();
    }

    /// <summary>
    /// 加载逻辑系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadSystem<T>() where T : class, ExecuteSystem, new()
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
        return (T)Context.Find<T>(id);
    }

    /// <summary>
    /// 移除实体对象
    /// </summary>
    /// <param name="id"></param>
    public static void Remove(int id)
    {
        Context.Remove(id);
    }

    /// <summary>
    /// 创建实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Create<T>() where T : class, IEntity, new()
    {
        return Context.Create<T>();
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
        _system.ForEach(a => a.Dispose());
        _system.Clear();
    }
}
