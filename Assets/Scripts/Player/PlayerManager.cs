using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables
        //all scripts references.
        public PlayerController controller = null;
        public PlayerAim Aim = null;
        public PlayerLife Life = null;
        #endregion


        private void Awake()
        {
            MakeSingleton(true);
        }
        


    }
}
