using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.ECS
{
    public class Systems
    {
        private static List<ExecuteSystem> _system = new List<ExecuteSystem>();
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
        public static void FixedUpdate(float time)
        {
            for (int i = _system.Count - 1; i >= 0; i--)
            {
                _system[i].Execute();
            }
        }
        public static void Dispose()
        {
            _system.ForEach(a => a.Dispose());
            _system.Clear();
        }
    }
}
