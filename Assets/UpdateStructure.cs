using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;

namespace Management
{
    public class UpdateStructure : MonoBehaviour
    {
        #region Variables
        public GameObject[] gardens;
        public GameObject[] chickens;
        public GameObject[] cows;
        public GameObject[] pigs;
        public GameObject[] horses;
        #endregion

        void Update()
        {
            UpdateGarden();
            UpdateCows();
            UpdatePigs();
            UpdateHorses();
            UpdateChickens();
        }

        void UpdateGarden()
        {
            for (int i = 0; i < gardens.Length; i++)
            {
                gardens[GameManager.Instance.numberOfGardens].SetActive(true);
            }
        }

        void UpdateChickens()
        {
            for (int i = 0; i < chickens.Length; i++)
            {
                chickens[GameManager.Instance.chickensCount].SetActive(true);
            }
        }
        void UpdateCows()
        {
            for (int i = 0; i < cows.Length; i++)
            {
                cows[GameManager.Instance.cowsCount].SetActive(true);
            }
        }
        void UpdatePigs()
        {
            for (int i = 0; i < pigs.Length; i++)
            {
                pigs[GameManager.Instance.pigsCount].SetActive(true);
            }
        }
        void UpdateHorses()
        {
            for (int i = 0; i < horses.Length; i++)
            {
                horses[GameManager.Instance.horsesCount].SetActive(true);
            }
        }
    }
}
    

