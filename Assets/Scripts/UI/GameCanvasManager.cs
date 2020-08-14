using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace GameCanvas{
    public class GameCanvasManager : Singleton<GameCanvasManager>
    {
        public ScreenFade blackScreen = null;
        //public UpdateStructure updateStructure = null;
        
 
        private void Awake()
        {
            MakeSingleton(true);
        }
    }
}

