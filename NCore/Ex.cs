using NGame.NCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Ex
{
    public static T AddComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {
        NCore.FindEntity<>
        T component = entity.GetComponent<T>();
        if (component != null) return component;
        component = new T();

    }

    public static T GetComponent<T>(this IEntity entity) where T : class, IComponent, new()
    {

    }

    public static IComponent[] GetComponents(this IEntity entity)
    {

    }

}
