using System;
using NGame.Entity;
using NGame.Event;
using NGame.Systems;
using NGame.Component;
using NGame.Managed;
using NGame.Config;
using System.Collections.Generic;

public delegate void LogEvent(object o);

/// <summary>
/// 框架核心
/// </summary>
public sealed partial class NCore
{
    static List<IManaged> _manageds;
    static bool isDispose = false;
    public static float Timer;

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initlizition()
    {
        System.Threading.ThreadPool.SetMaxThreads(2, 2);
        Ex.Log("初始化核心框架");
        try
        {
            _manageds = new List<IManaged>();
            LoadManaged<SystemManaged>();
            LoadManaged<EntityManaged>();
            LoadManaged<EventSystem>();
            LoadManaged<ConfigerManaged>();
            Ex.Log("初始化核心框架成功！ 框架版本：" + AppSettings.version);
        }
        catch (Exception ex)
        {
            Ex.Log("初始化核心框架失败！");
            Ex.Log(ex.Message);
        }
    }

    /// <summary>
    /// 加载管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadManaged<T>() where T : class, IManaged, new()
    {
        T managed = GetManaged<T>();
        if (managed != null) return managed;
        managed = new T();
        managed.Initlizition();
        _manageds.Add(managed);
        return managed;
    }
    /// <summary>
    /// 获取加载器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetManaged<T>() where T : class, IManaged, new()
    {
        for (int i = 0; i < _manageds.Count; i++)
        {
            if (_manageds[i].GetType() == typeof(T))
                return (T)_manageds[i];
        }
        return null;
    }
    /// <summary>
    /// 卸载管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void UnLoadManaged<T>() where T : class, IManaged, new()
    {
        T managed = GetManaged<T>();
        if (managed == null) return;
        _manageds.Remove(managed);
        managed.Dispose();
    }

    /// <summary>
    /// 轮询系统
    /// </summary>
    public static void FixedUpdate(float time)
    {
        if (isDispose) return;
        Timer = time;
        for (int i = _manageds.Count - 1; i >= 0; i--)
        {
            _manageds[i].Update(time);
        }
    }
    /// <summary>
    /// 释放
    /// </summary>
    public static void Dispose()
    {
        if (isDispose) return;
        isDispose = true;

        _manageds.ForEach(a => a.Dispose());
        _manageds.Clear();
    }
}
