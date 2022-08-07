﻿using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSingals : MonoSingleton<CoreGameSingals>
    {
        public UnityAction onGameOpen = delegate {  };
        public UnityAction onGameClose = delegate {  };
        public UnityAction<bool> onGamePause = delegate {  };
        public UnityAction onPlay = delegate {  };
        public UnityAction<GameStates> onChangeGameState=delegate {  };
        public UnityAction onReset=delegate { };
    }
}