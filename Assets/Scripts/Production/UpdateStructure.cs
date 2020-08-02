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
            if(GameManager.Instance.numberOfGardens == 0)
            {
                gardens[0].SetActive(true);
                gardens[1].SetActive(false);
                gardens[2].SetActive(false);
                gardens[3].SetActive(false);
            } else if(GameManager.Instance.numberOfGardens == 1)
            {
                gardens[0].SetActive(true);
                gardens[1].SetActive(true);
                gardens[2].SetActive(false);
                gardens[3].SetActive(false);
            }
            else if (GameManager.Instance.numberOfGardens == 2)
            {
                gardens[0].SetActive(true);
                gardens[1].SetActive(true);
                gardens[2].SetActive(true);
                gardens[3].SetActive(false);
            }
            else if (GameManager.Instance.numberOfGardens == 4)
            {
                gardens[0].SetActive(true);
                gardens[1].SetActive(true);
                gardens[2].SetActive(true);
                gardens[3].SetActive(true);
            }

        }

        void UpdateChickens()
        {
            if(GameManager.Instance.chickensCount == 0)
            {
                chickens[1].SetActive(false);
                chickens[2].SetActive(false);
                chickens[3].SetActive(false);
                chickens[4].SetActive(false);
            }
            else if (GameManager.Instance.chickensCount == 1)
            {
                chickens[1].SetActive(true);
                chickens[2].SetActive(false);
                chickens[3].SetActive(false);
                chickens[4].SetActive(false);
            }
            else if (GameManager.Instance.chickensCount == 2)
            {
                chickens[1].SetActive(true);
                chickens[2].SetActive(true);
                chickens[3].SetActive(false);
                chickens[4].SetActive(false);
            }
            else if (GameManager.Instance.chickensCount == 3)
            {
                chickens[1].SetActive(true);
                chickens[2].SetActive(true);
                chickens[3].SetActive(true);
                chickens[4].SetActive(false);
            }
            else if (GameManager.Instance.chickensCount == 4)
            {
                chickens[1].SetActive(true);
                chickens[2].SetActive(true);
                chickens[3].SetActive(true);
                chickens[4].SetActive(true);
            }


        }
        void UpdateCows()
        {
            if (GameManager.Instance.cowsCount == 0)
            {
                cows[1].SetActive(false);
                cows[2].SetActive(false);
                cows[3].SetActive(false);
                cows[4].SetActive(false);
            }
            else if (GameManager.Instance.cowsCount == 1)
            {
                cows[1].SetActive(true);
                cows[2].SetActive(false);
                cows[3].SetActive(false);
                cows[4].SetActive(false);
            }
            else if (GameManager.Instance.cowsCount == 2)
            {
                cows[1].SetActive(true);
                cows[2].SetActive(true);
                cows[3].SetActive(false);
                cows[4].SetActive(false);
            }
            else if (GameManager.Instance.cowsCount == 3)
            {
                cows[1].SetActive(true);
                cows[2].SetActive(true);
                cows[3].SetActive(true);
                cows[4].SetActive(false);
            }
            else if (GameManager.Instance.cowsCount == 4)
            {
                cows[1].SetActive(true);
                cows[2].SetActive(true);
                cows[3].SetActive(true);
                cows[4].SetActive(true);
            }
        }
        void UpdatePigs()
        {
            if (GameManager.Instance.pigsCount == 0)
            {
                pigs[1].SetActive(false);
                pigs[2].SetActive(false);
                pigs[3].SetActive(false);
                pigs[4].SetActive(false);
            }
            else if (GameManager.Instance.pigsCount == 1)
            {
                pigs[1].SetActive(true);
                pigs[2].SetActive(false);
                pigs[3].SetActive(false);
                pigs[4].SetActive(false);
            }
            else if (GameManager.Instance.pigsCount == 2)
            {
                pigs[1].SetActive(true);
                pigs[2].SetActive(true);
                pigs[3].SetActive(false);
                pigs[4].SetActive(false);
            }
            else if (GameManager.Instance.pigsCount == 3)
            {
                pigs[1].SetActive(true);
                pigs[2].SetActive(true);
                pigs[3].SetActive(true);
                pigs[4].SetActive(false);
            }
            else if (GameManager.Instance.pigsCount == 4)
            {
                pigs[1].SetActive(true);
                pigs[2].SetActive(true);
                pigs[3].SetActive(true);
                pigs[4].SetActive(true);
            }
        }
        void UpdateHorses()
        {
            if (GameManager.Instance.horsesCount == 0)
            {
                horses[1].SetActive(false);
                horses[2].SetActive(false);
                horses[3].SetActive(false);
                horses[4].SetActive(false);
            }
            else if (GameManager.Instance.horsesCount == 1)
            {
                horses[1].SetActive(true);
                horses[2].SetActive(false);
                horses[3].SetActive(false);
                horses[4].SetActive(false);
            }
            else if (GameManager.Instance.horsesCount == 2)
            {
                horses[1].SetActive(true);
                horses[2].SetActive(true);
                horses[3].SetActive(false);
                horses[4].SetActive(false);
            }
            else if (GameManager.Instance.horsesCount == 3)
            {
                horses[1].SetActive(true);
                horses[2].SetActive(true);
                horses[3].SetActive(true);
                horses[4].SetActive(false);
            }
            else if (GameManager.Instance.horsesCount == 4)
            {
                horses[1].SetActive(true);
                horses[2].SetActive(true);
                horses[3].SetActive(true);
                horses[4].SetActive(true);
            }
        }
    }
}
    

