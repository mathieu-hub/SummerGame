using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Cinemachine;



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
        public List<GameObject> activeAnimals = new List<GameObject>();
        public int totalAnimalWeight;
        public bool tooMuchAnimals = false;
       

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
        public GameObject siloPoint;
        public bool isPods = false;
        public bool siloAttacked = false;
        public int wavesBeforeSeller;
        public bool isMarchand = false;
        public bool needToRefeshShop = false;
        public bool inPause = false;

        [Header("Cinématique Stuff")]
        public bool arrivedFirst = false;
        public bool arrivedSecond = false;

        public GameObject baseArea;

        public CinemachineVirtualCamera vCam;

        public GameObject Seller;
        public GameObject podsPositions = null;

        public bool underAttack = false;

        public float volume;

        void Awake()
        {
            MakeSingleton(true);
        }
        private void Start()
        {
            wavesBeforeSeller = 5;
        
        }

        private void Update()
        {
            if (Input.GetButtonDown("X_Button"))
            {
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
