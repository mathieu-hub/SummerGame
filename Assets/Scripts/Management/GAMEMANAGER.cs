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
        public int storedUnits;
        public int maxStoredUnits;

        [Header("Garden")]
        public int numberOfGardens;

        [Header("GameObject References")]
        public GameObject respawnPoint;
        #endregion

        [Header("Towers")]
        public bool strootUnlock = true;
        public bool bourloUnlock = false;
        public bool snipicUnlock = false;
        public bool tronçoronceUnlock = false;
        public bool invasiveUnlock = false;

        public SocleManager SocleManager = null;

        [Header("DéroulementDuJeu")]
        public int waves;
        public int wavesBeforeSeller;
        public bool needToRefeshShop = false;

        void Awake()
        {
            MakeSingleton(true);
        }
        private void Start()
        {
            wavesBeforeSeller = 5;
            waves = 0;
        }

        private void Update()
        {
            if (Input.GetButtonDown("X_Button"))
            {
                waves += 1;
                wavesBeforeSeller -= 1;
            }

            if(wavesBeforeSeller < 0)
            {
                wavesBeforeSeller = 4;
                needToRefeshShop = true;
            }
            else
            {
                needToRefeshShop = false;
            }

        }

    }

}
