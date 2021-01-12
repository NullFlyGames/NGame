using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Config;

namespace NGame.Game
{
    public interface IRoom
    {
        IConfiger Configer { get; }
        void OnStarted();
        void OnClose();
        void OnMessage();
        void DoPlayer();

    }
}
