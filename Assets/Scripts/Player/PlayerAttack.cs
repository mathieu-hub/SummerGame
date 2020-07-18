using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;

namespace Player
{
    /// <summary>
    /// This script makes the player attack according an attack bar;
    /// </summary>

    public class PlayerAttack : MonoBehaviour
    {
        #region
        [Header("Variables")]
        [SerializeField] private bool isLoadingAttack = false;
        [SerializeField] private bool isAttacking = false;
        [SerializeField] private bool needToCharge = false;
        [SerializeField] private float loadingTime;
        [SerializeField] private int maxLoadingTime = 100;
        [SerializeField] private int numberOfVegetablesEat = 0;

        #endregion
        private void Start()
        {
            Initialisation();
        }

        void Update()
        {
            if (Input.GetButtonDown("B_Button") && GameManager.Instance.vegetablesCount >=1)
            {
                Debug.Log("Called");
                isLoadingAttack = true;
                isAttacking = true;
                needToCharge = true;
                GameManager.Instance.vegetablesCount -= 1;
                numberOfVegetablesEat = 1;
            }

            if (Input.GetButtonUp("B_Button"))
            {
                isLoadingAttack = false;
                
            }

            if (isLoadingAttack && isAttacking && needToCharge)
            {
                Loading();
            }

            if (!isLoadingAttack && isAttacking)
            {
                isAttacking = false;
                Damages();
            }
        }

        void Loading()
        {
            if (loadingTime == 33 && numberOfVegetablesEat == 1)
            {
                if (GameManager.Instance.vegetablesCount < 1)
                {
                    needToCharge = false;
                    Damages();
                    
                }
            }
            else if (loadingTime == 66 && numberOfVegetablesEat == 2)
            {
                if (GameManager.Instance.vegetablesCount < 1)
                {
                    
                    Damages();
                    
                }
            }
            else if (needToCharge)
            {
                loadingTime += 1;
            }


        }

        void Damages()
        {
            if(numberOfVegetablesEat == 1)
            {
                Debug.Log("Petit bullet");
            }else if (numberOfVegetablesEat == 2)
            {
                Debug.Log("Moyen bullet");
            }
            else if (numberOfVegetablesEat == 3)
            {
                Debug.Log("Gros bullet");
            }
            Initialisation();
        }

        void Initialisation()
        {
            Debug.Log("Initialisation Called");
            isLoadingAttack = false;
            isAttacking = false;
            needToCharge = false;
            loadingTime = 0;
            maxLoadingTime = 100;
            numberOfVegetablesEat = 0;
        }
        
    }
}

