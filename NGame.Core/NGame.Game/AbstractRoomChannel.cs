using NGame.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Game
{
    public abstract class AbstractRoomChannel : IRoom
    {
        public IConfiger Configer { get; protected set; }

        public abstract void DoPlayer();
        public abstract void OnClose();
        public abstract void OnMessage();
        public abstract void OnStarted();
    }
}
