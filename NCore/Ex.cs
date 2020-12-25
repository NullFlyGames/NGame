using NGame.NCore;
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
        return NCore.AddComponent<T>(entity);
    }

    /// <summary>
    /// 获取指定类型的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static T GetComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        return NCore.GetComponent<T>(entity);
    }

    /// <summary>
    /// 获取实体上所有组件
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static IComponent[] GetComponents(this IEntity entity)
    {
        return NCore.GetComponents(entity);
    }

    public static void RemoveComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        NCore.RemoveComponent<T>(entity);
    }
   
}
