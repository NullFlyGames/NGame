using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.UIForm
{
    public class UIFormManaged : AbstractManaged
    {
        private readonly Dictionary<Type, IUIPanle> _uipanles = new Dictionary<Type, IUIPanle>();
        private readonly Dictionary<int, ILayer> _layers = new Dictionary<int, ILayer>();

        public override void Update(float time)
        {
            base.Update(time);
        }

        /// <summary>
        /// 加载UI界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadUIForm<T>() where T : class, IUIPanle, new()
        {
            var type = typeof(T);
            if (_uipanles.ContainsKey(type))
            {
                var cache = _uipanles[type];
                cache.OnEnable();
                return (T)cache;
            }

            T panle = new T();
            panle.OnAwake();
            _uipanles.Add(type, panle);
            return panle;
        }
        /// <summary>
        /// 卸载UI界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UnLoadForm<T>(bool isDelete) where T : IUIPanle
        {
            var type = typeof(T);
            if (_uipanles.ContainsKey(type) == false) return;
            if (isDelete)
            {
                _uipanles[type].OnDestory();
                _uipanles.Remove(type);
                return;
            }
            _uipanles[type].OnDisable();
        }
        /// <summary>
        /// 设置UI界面层级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="layer"></param>
        /// <param name="target"></param>
        public void SetPanleLayer<T>(int layer, T target)
        {
            if (_layers.ContainsKey(layer) == false) throw new KeyNotFoundException();

            _layers[layer].Set<T>(target);
        }

        /// <summary>
        /// 获取UI界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUIForm<T>() where T : IUIPanle
        { 
            if(_uipanles.ContainsKey(typeof(T))==false) throw new KeyNotFoundException();

            return (T)_uipanles[typeof(T)];
        }

        /// <summary>
        /// 创建UI层级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="layer"></param>
        /// <returns></returns>
        public T CreateLayer<T>(int layer) where T : class, ILayer, new()
        {
            if (_layers.ContainsKey(layer) == true)
            {
                _layers[layer].OnEnable();
                return (T)_layers[layer];
            }

            T temp = new T();
            temp.OnAwake();
            _layers.Add(layer, temp);
            return temp;
        }
        /// <summary>
        /// 获取UI层级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="layer"></param>
        /// <returns></returns>
        public T GetLayer<T>(int layer) where T : ILayer
        {
            if (_layers.ContainsKey(layer) == false) throw new NullReferenceException();

            return (T)_layers[layer];
        }
        /// <summary>
        /// 清楚所有缓存
        /// </summary>
        public void Clear()
        {
            foreach (var item in _uipanles)
            {
                item.Value.OnDestory();
            }
            foreach (var item in _layers)
            {
                item.Value.OnDestory();
            }
            _layers.Clear();
            _uipanles.Clear();
        }
        public override void Dispose()
        {
            Clear();
        }
    }
}
