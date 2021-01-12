using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.UIForm
{
    public interface IUIPanle
    {
        string Name { get; }

        void OnAwake();
        void OnDestory();
        void OnDisable();
        void OnEnable();
    }
}
