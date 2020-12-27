using NGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Ex
{
    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static T AddComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        return Context.AddComponent<T>(entity);
    }

    /// <summary>
    /// 获取指定类型的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static T GetComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        return Context.GetComponent<T>(entity);
    }

    /// <summary>
    /// 获取实体上所有组件
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static IComponent[] GetComponents(this IEntity entity)
    {
        return Context.GetComponents(entity);
    }

    public static void RemoveComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        Context.RemoveComponent<T>(entity);
    }

    public static void Log(object o)
    {
        if (NCore.logEvent == null)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}] {o}");
            return;
        }
        NCore.logEvent(o);
    }
   
}
