using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class GameManager : Singleton<GameManager>
    {

        #region Variables

        [Header("Ressources")]
        public int purinCount;
        public int vegetablesCount;
        public int scrapsCount;
        public int plansCount;

        #endregion
        void Awake()
        {
            MakeSingleton(true);
        }

    }

}
