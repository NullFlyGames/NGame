using System;
using System.Collections.Generic;
using NGame.ECS;
using NGame.Event;

public delegate void LogEvent(object o);

/// <summary>
/// 框架核心
/// </summary>
public sealed partial class NCore : IDisposable
{
    internal static LogEvent logEvent;

    public static event LogEvent LogEvents
    {
        add { logEvent += value; }
        remove { logEvent -= value; }
    }

    public static void Initlizition()
    {
        Context.Initlizition();
    }

    /// <summary>
    /// 加载逻辑系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadSystem<T>() where T : class, ExecuteSystem, new()
    {
        return Systems.LoadSystem<T>();
    }

    /// <summary>
    /// 卸载逻辑系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void UnLoadSystem<T>() where T : class, ISystem, new()
    {
        Systems.UnLoadSystem<T>();
    }
    /// <summary>
    /// 查找实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T Find<T>(int id) where T : class, IEntity, new()
    {
        return Context.Find<T>(id);
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
    public static void FixedUpdate(float time)
    {
        EventSystem.FixedUpdate(time);
        Systems.FixedUpdate(time);
      
    }

    public void Dispose()
    {
        Systems.Dispose();
        EventSystem.Dispose();
    }
}
