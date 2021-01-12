using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.Entity
{
    public class EntityManaged : IManaged
    {
        List<IEntity> _entitys;
        public void Initlizition()
        {
            _entitys = new List<IEntity>();
        }




        public void Update(float time) { }
        /// <summary>
        /// 查找实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : IEntity
        {
            for (int i = 0; i < _entitys.Count; i++)
            {
                if (_entitys[i].id == id)
                    return (T)_entitys[i];
            }
            return default;
        }

        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            IEntity entity = Find<IEntity>(id);
            if (entity == null) return;

            _entitys.Remove(entity);
        }

        /// <summary>
        /// 创建实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>() where T : class, IEntity, new()
        {
            T entity = new T();
            _entitys.Add(entity);
            return entity;
        }
        public void Dispose()
        {
            _entitys.Clear();
        }
    }
}
