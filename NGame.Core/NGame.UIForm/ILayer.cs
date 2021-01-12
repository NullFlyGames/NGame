using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.UIForm
{
    public interface ILayer
    {
        void OnAwake();
        void Set<T>(T target);
        void OnDisable();
        void OnEnable();
        void OnDestory();
    }
}
