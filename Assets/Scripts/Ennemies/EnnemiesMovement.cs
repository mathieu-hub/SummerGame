using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemies
{
    public class EnnemiesMovement : MonoBehaviour
    {
        //Movement
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        [SerializeField] private int wayPointIndex = 0;

        [HideInInspector] public bool isPushed = false;

        //Make Damage
        [Header("Damage")]
        public bool canMakeDamage = false;
        public bool doingDamage = false;
        [SerializeField] private int ennemyDamage;
        [SerializeField] private int speedAttack;
        
        [Header("Push")]
        [Range(2, 10)]
        public int maxPushes = 2;
        [Range(0.5f, 20f)]
        [SerializeField] private float resistanceTime = 3f;
        [SerializeField] private bool pushTest = false;
        //private bool inResistance = false;

        [HideInInspector] public GameObject pushingBullet = null;
        [HideInInspector] public int pushedCount = 0;

        //Spawning
        float randomSpawn;        

        //TYPE OF ENNEMY
        public enum TypeOfEnnemy { Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone }
        public TypeOfEnnemy typeOfEnnemy;

        void Awake()
        {
            if (typeOfEnnemy == TypeOfEnnemy.Walker)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                speed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
        }

        void Start()
        {
            if (pushTest)
                return;
            StartingWay();            
        }

        void StartingWay()
        {
            randomSpawn = Random.Range(0, GameMaster.Instance.WayMaster.numberOfWay);            
            Debug.Log("randomspawn" + randomSpawn);
                      
            
            if (randomSpawn == 0)
            {
                target = GameMaster.Instance.WayMaster.way01[0];
            }
            else if (randomSpawn == 1 && GameMaster.Instance.WayMaster.way2.activeInHierarchy)
            {
                target = GameMaster.Instance.WayMaster.way02[0];
            }
            else if (randomSpawn == 2 && GameMaster.Instance.WayMaster.way3.activeInHierarchy)
            {
                target = GameMaster.Instance.WayMaster.way03[0];
            }
            else if (randomSpawn == 3 && GameMaster.Instance.WayMaster.way4.activeInHierarchy)
            {
                target = GameMaster.Instance.WayMaster.way04[0];
            }
            else if (randomSpawn == 4 && GameMaster.Instance.WayMaster.way5.activeInHierarchy)
            {
                target = GameMaster.Instance.WayMaster.way05[0];
            }
            else
            {
                randomSpawn = 0;
                target = GameMaster.Instance.WayMaster.way01[0];
            }
        }

        private void Update()
        {
            //Check if enemy is being pushed by Tronçronce bullet
            if (isPushed || pushTest)
                return;

            //if (pushedCount >= maxPushes && !inResistance)
            //    StartCoroutine(ResistToPush());

            //Déplacements des ennemies
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= 0.3f)
            {
                GetNextWaypoint();
            }

            if (wayPointIndex > 5)
            {
                wayPointIndex = 5;
            }
        }

        private void GetNextWaypoint()
        {
            //WAYPOINT FOR WAY01
            if (randomSpawn == 0)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way01.Length - 1)
                {
                    if (!canMakeDamage)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = 0f;
                        StartCoroutine(EndPath());
                        return;
                    }

                }
                else
                {
                    wayPointIndex++;
                    target = GameMaster.Instance.WayMaster.way01[wayPointIndex];
                }
            }            

            //WAYPOINT FOR WAY02
            if (randomSpawn == 1)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way02.Length - 1)
                {
                    if (!canMakeDamage)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = 0f;
                        StartCoroutine(EndPath());
                        return;
                    }

                }
                else
                {
                    wayPointIndex++;
                    target = GameMaster.Instance.WayMaster.way02[wayPointIndex];
                }
            }
            

            //WAYPOINT FOR WAY03
            if (randomSpawn == 2)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way03.Length - 1)
                {
                    if (!canMakeDamage)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = 0f;
                        StartCoroutine(EndPath());
                        return;
                    }

                }
                else
                {
                    wayPointIndex++;
                    target = GameMaster.Instance.WayMaster.way03[wayPointIndex];
                }
            }
            

            //WAYPOINT FOR WAY04
            if (randomSpawn == 3)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way04.Length - 1)
                {
                    if (!canMakeDamage)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = 0f;
                        StartCoroutine(EndPath());
                        return;
                    }

                }
                else
                {
                    wayPointIndex++;
                    target = GameMaster.Instance.WayMaster.way04[wayPointIndex];
                }
            }
            

            //WAYPOINT FOR WAY05
            if (randomSpawn == 4)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way05.Length - 1)
                {
                    if (!canMakeDamage)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = 0f;
                        StartCoroutine(EndPath());
                        return;
                    }

                }
                else
                {
                    wayPointIndex++;
                    target = GameMaster.Instance.WayMaster.way05[wayPointIndex];
                }
            }            
        }

        // Fait des dégâts au Silo et se détruit une fois arrivé au dernier Waypoint.       
        
        IEnumerator EndPath()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                doingDamage = true;
                SiloLife.lives -= ennemyDamage;
                yield return new WaitForSeconds(0.3f);
                canMakeDamage = false;
            }
        }

        public void DoResistToPush()
        {
            StartCoroutine(ResistToPush());
        }
        IEnumerator ResistToPush()
        {
            //inResistance = true;
            yield return new WaitForSeconds(resistanceTime);
            pushedCount = 0;
            //inResistance = false;
        }
        
    }
}

