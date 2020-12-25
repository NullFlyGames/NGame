using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.NCore;

public sealed partial class NCore
{
    private static Dictionary<int, IContext> _entitys;
    private static List<ISystem> _sys;

    public static void Initlizition()
    {
        _sys = new List<ISystem>();
        _entitys = new Dictionary<int, IContext>();
    }

    public static T LoadSystem<T>() where T : class, ISystem, new()
    {
        T sys = new T();
        sys.Initialize();
        _sys.Add(sys);
        return sys;
    }

    public static void UnLoadSystem<T>() where T : class, ISystem, new()
    {
        for (int i = _sys.Count - 1; i >= 0; i--)
        {
            ISystem system = _sys[i];
            if (system == null || system.GetType() != typeof(T)) continue;
            system.Dispose();
        }
    }

    public static T Find<T>(int id) where T : class, IEntity, new()
    {
        if (_entitys.ContainsKey(id) == false) return null;
        return (T)_entitys[id].Entity;
    }

    public static void Remove(int id)
    {
    }

    public static List<IEntity> GetGroup<T>(Matcher matcher) where T : class, IEntity, new()
    {
        return matcher.AllOf()
    }

    public static void FixedUpdate()
    {
        for (int i = _sys.Count - 1; i >= 0; i--)
        {
            _sys[i].Execute();
        }
    }
}
