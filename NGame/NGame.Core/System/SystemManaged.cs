using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.Systems
{
    public class SystemManaged : IManaged
    {
        private List<IExecuteSystem> executes;
        private bool isDispose = false;

        public void Initlizition()
        {
            executes = new List<IExecuteSystem>();
        }

        public void Update(float time)
        {
            if (isDispose) return;

            for (int i = executes.Count - 1; i >= 0; i--)
            {
                executes[i].Execute(time);
            }
        }
        public void Dispose()
        {
            if (isDispose) return;
            isDispose = true;
            executes.ForEach(a => a.Dispose());
        }
        /// <summary>
        /// 加载逻辑系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadSystem<T>() where T : class, IExecuteSystem, new()
        {
            T temp = FindSystem<T>();
            if (temp != null) return temp;

            temp = new T();
            temp.Initlizition();
            executes.Add(temp);
            return temp;
        }
        /// <summary>
        /// 加载逻辑系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindSystem<T>() where T : class, IExecuteSystem, new()
        {
            for (int i = executes.Count - 1; i >= 0; i--)
            {
                if (executes[i].GetType() == typeof(T))
                    return (T)executes[i];
            }
            return null;
        }
        /// <summary>
        /// 卸载逻辑系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UnLoadSystem<T>() where T : class, IExecuteSystem, new()
        {
            T temp = FindSystem<T>();
            if (temp == null) return;
            executes.Remove(temp);
        }
    }
}
