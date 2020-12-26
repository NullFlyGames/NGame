using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    class Group : IGroup
    {
        readonly Dictionary<int, IEntity> _entitys;
        readonly IMatcher _matcher;
        public Group(IMatcher matcher)
        {
            _matcher = matcher;
            _entitys = new Dictionary<int, IEntity>();
        }
        public void Add(IEntity entity)
        {
            if (_entitys.ContainsKey(entity.id))
                throw new Exception("the entity is re exis this group.");
            _entitys.Add(entity.id, entity);
        }

        public void HandleEntitySilently(IEntity entity)
        {
            if (_matcher.Matche(entity) == false)
            {
                if (_entitys.ContainsKey(entity.id))
                {
                    _entitys.Remove(entity.id);
                }
            }
            else
            {
                if (_entitys.ContainsKey(entity.id) == false)
                {
                    _entitys.Add(entity.id, entity);
                }
            }
        }

        public List<IEntity> GetEntities()
        {
            return _entitys.Values.ToList();
        }

        public void Remove(int id)
        {
            if (_entitys.ContainsKey(id) == false) return;
            _entitys.Remove(id);
        }
    }
}
