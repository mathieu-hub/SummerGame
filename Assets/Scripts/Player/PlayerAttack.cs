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
        [Header("Bools")]
        public bool isLoadingAttack = false;
        private bool isAttacking = false;
        private bool needToCharge = false;

        [Header("Floats")]
        [SerializeField] public float loadingTime;
        public int maxLoadingTime = 100;
        [HideInInspector] public int numberOfVegetablesEat = 0;
        [SerializeField] private int firstLevel;
        [SerializeField] private int secondLevel;

        [Header("AttackPosition")]
        [SerializeField] private GameObject instantiatePosition;
        [SerializeField] private Object bullet;
        #endregion

        private void Start()
        {
            Initialisation();

            bullet = Resources.Load("Prefabs/Bullet");
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
                PlayerManager.Instance.controller.moveSpeed = PlayerManager.Instance.controller.initialMoveSpeed;
            }

            if (isLoadingAttack && isAttacking && needToCharge)
            {
                Loading();
                Eating();
            }

            if (!isLoadingAttack && isAttacking)
            {
                isAttacking = false;
                Damages();
            }
        }

        void Loading()
        {
            PlayerManager.Instance.controller.moveSpeed = PlayerManager.Instance.controller.loadingMoveSpeed;

            if (loadingTime >= firstLevel && numberOfVegetablesEat == 1 && GameManager.Instance.vegetablesCount < 1)
            {

                    needToCharge = false;

            }
            else if (loadingTime == secondLevel && numberOfVegetablesEat == 2 && GameManager.Instance.vegetablesCount < 1)
            {

                needToCharge = false;

            }
            else if (needToCharge)
            {
                loadingTime += 0.1f;
            }

            if(loadingTime >= maxLoadingTime)
            {
                loadingTime = maxLoadingTime;
            }
        }

        void Eating()
        {
            if(loadingTime >= firstLevel && numberOfVegetablesEat == 1 && GameManager.Instance.vegetablesCount >= 1)
            {
                GameManager.Instance.vegetablesCount -= 1;
                numberOfVegetablesEat = 2;
            }

            if (loadingTime >= secondLevel && numberOfVegetablesEat == 2 && GameManager.Instance.vegetablesCount >= 1)
            {
                GameManager.Instance.vegetablesCount -= 1;
                numberOfVegetablesEat = 3;
            }

        }

        void Damages()
        {
            if(numberOfVegetablesEat == 1)
            {
                //Instantiate
                Debug.Log("Petit bullet");
            }else if (numberOfVegetablesEat == 2)
            {
                //Instantiate
                Debug.Log("Moyen bullet");
            }
            else if (numberOfVegetablesEat == 3)
            {
                //Instantiate
                Debug.Log("Gros bullet");
            }
            Instantiate(bullet, instantiatePosition.transform.position, Quaternion.identity);
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

