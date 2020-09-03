using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using AudioManager;

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
        public bool isAttacking = false;
        public bool needToCharge = false;
        public bool canAttack = true;

        [Header("Floats")]
        [SerializeField] public float loadingTime;
        public int maxLoadingTime = 100;
        [HideInInspector] public int numberOfVegetablesEat = 0;
        [SerializeField] private int firstLevel;
        [SerializeField] private int secondLevel;
        [HideInInspector] public int lastNumberOfVegetablesEat = 0;
        [SerializeField] private float cooldown;

        [Header("AttackPosition")]
        [SerializeField] public GameObject instantiatePosition;
        [SerializeField] private Object bullet;
        #endregion

        private void Start()
        {
            Initialisation();

            bullet = Resources.Load("Prefabs/Bullet");
        }

        void Update()
        {
            if (Input.GetButtonDown("Right_Bumper") && canAttack)
            {
                canAttack = false;
                Debug.Log("Called");
                isLoadingAttack = true;
                lastNumberOfVegetablesEat = 0;
                isAttacking = true;
                needToCharge = true;
               
                numberOfVegetablesEat = 1;
            }

            if (Input.GetButtonUp("Right_Bumper"))
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

            if (loadingTime >= 33 && numberOfVegetablesEat == 1 && GameManager.Instance.vegetablesCount < 1)
            {

                    needToCharge = false;

            }
            else if (loadingTime >= firstLevel + 1 && numberOfVegetablesEat == 2 && GameManager.Instance.vegetablesCount < 1)
            {

                needToCharge = false;

            }
            else if (needToCharge)
            {
                loadingTime += 0.2f;
            }

            if(loadingTime >= maxLoadingTime)
            {
                loadingTime = maxLoadingTime;
                needToCharge = false;
            }
        }

        void Eating()
        {
            if(loadingTime >= firstLevel && numberOfVegetablesEat == 1)
            {
                numberOfVegetablesEat = 2;
            }

            if (loadingTime >= secondLevel && numberOfVegetablesEat == 2)
            {
                
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

            SingletonAudioSource.Instance.soundmanager.setValues(PlayerManager.Instance.audioSource, 34);
            PlayerManager.Instance.audioSource.Play();

            Instantiate(bullet, instantiatePosition.transform.position, Quaternion.identity);
            lastNumberOfVegetablesEat = numberOfVegetablesEat;
            Initialisation();
            StartCoroutine(Cooldown());
        }

        void Initialisation()
        {
            Debug.Log("Initialisation Called");
            isLoadingAttack = false;
            isAttacking = false;
            needToCharge = false;
          
            maxLoadingTime = 100;
            numberOfVegetablesEat = 0;
            
        }

        IEnumerator Cooldown()
        {
            

            if(loadingTime > 0)
            {
                loadingTime -= 3f;
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(Cooldown());
            }
            else
            {
                loadingTime = 0;
                canAttack = true;
            }



          
        }
        
    }
}

