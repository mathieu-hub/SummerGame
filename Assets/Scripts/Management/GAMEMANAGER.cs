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

        [Header("Animals")]
        public int chickensCount;
        public int cowsCount;
        public int pigsCount;
        public int horsesCount;

        [Header("Garden")]
        public int numberOfGardens;

        [Header("GameObject References")]
        public GameObject respawnPoint;
        #endregion

        void Awake()
        {
            MakeSingleton(true);
        }

    }

}
