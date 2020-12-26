using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    interface IGroup
    {
        void Add(IEntity entity);
        void Remove(int id);
        List<IEntity> GetEntities();
        void HandleEntitySilently(IEntity entity);
    }
}
