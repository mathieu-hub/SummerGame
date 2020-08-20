using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneMovement : MonoBehaviour	
    {
        public bool isNotDrone;

        [Header("Values")]
        [SerializeField] private float speed;
        [SerializeField] private float initialspeed;
        [SerializeField] private float stopSpeed = 0f;

        [Header("Ways")]
        public float currentWay;
        [SerializeField] private Transform targetMovement;
        public bool passedTheFirst = false;
        public bool passedTheLast = false;
        public bool passedTheSilo = false;

        [Header("Drone Station")]
        public Transform droneStation;
        public bool droneIsInStation = false;

        //[Header("Animator")]
        //public Animator animator;


        void Awake()
        {
            InitialisationValues();
            StartingWay();
        }

		void Update()
		{
            //Movements
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            UpdateTargetMovement();

            //animations
            /*if (speed == 0)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }*/

            //Le Drone arrive dans DroneStation, il s'arrête.
            if (droneIsInStation == true)
            {
                speed = stopSpeed;
            }
            //le Drone est dans la station et la jauge de recherche est complète.
            if (droneIsInStation == true && GetComponent<ResearchBar>().barIsComplete == true)
            {
                WaveSpawner.ennemyAlive--;
                Destroy(gameObject);
            }
        }

        void InitialisationValues()
        {
            initialspeed = 10f;
            speed = initialspeed;
            //animator.SetBool("isDrone", true);
        }

        void StartingWay()
        {
            currentWay = Random.Range(0, GameMaster.Instance.WayMaster.numberOfWay);
            Debug.Log("randomspawn" + currentWay);

            if (currentWay == 0)
            {
                targetMovement = GameMaster.Instance.WayMaster.way01[0];
            }
            else if (currentWay == 1 && GameMaster.Instance.WayMaster.way2.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way02[0];
            }
            else if (currentWay == 2 && GameMaster.Instance.WayMaster.way3.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way03[0];
            }
            else if (currentWay == 3 && GameMaster.Instance.WayMaster.way4.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way04[0];
            }
            else if (currentWay == 4 && GameMaster.Instance.WayMaster.way5.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way05[0];
            }
            else
            {
                currentWay = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[0];
            }            
        }

        void UpdateTargetMovement()
        {
            if(passedTheFirst)
            {
                if (currentWay == 0)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way01[3];
                }
                else if (currentWay == 1 && GameMaster.Instance.WayMaster.way2.activeInHierarchy)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way02[3];
                }
                else if (currentWay == 2 && GameMaster.Instance.WayMaster.way3.activeInHierarchy)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way03[3];
                }
                else if (currentWay == 3 && GameMaster.Instance.WayMaster.way4.activeInHierarchy)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way04[3];
                }
                else if (currentWay == 4 && GameMaster.Instance.WayMaster.way5.activeInHierarchy)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way05[3];
                }
                else
                {                    
                    targetMovement = GameMaster.Instance.WayMaster.way01[3];
                }
            }
            else if(passedTheLast)
            {
                targetMovement = GameMaster.Instance.WayMaster.way01[4];
            }
            else if (passedTheSilo)
            {
                targetMovement = droneStation;
            }
        }
    }
}

