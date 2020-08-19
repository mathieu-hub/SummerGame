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
        [SerializeField] private float initialspeed;
        [SerializeField] private float stopSpeed = 0f;
        [SerializeField] private Transform targetMovement;
        [SerializeField] private int wayPointIndex = 0;
        [SerializeField] private int crossWayPointIndex = 0;
      
        public float rangeForCrosspoint;
        public Transform droneStation;
        public bool droneIsInStation = false;

        [HideInInspector] public bool isPushed = false;

        //Make Damage
        [Header("Damage")]
        public Transform defenseTarget;
        public float rangeForDefense;
        public bool canMakeDamage = false;
        public bool doingDamage = false;
        [SerializeField] private int ennemyDamage;
        [SerializeField] private int speedAttack;
        public bool isNotDrone;

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

        public GameObject parentRef;

        public bool needToCheck = true;
        public bool doingChecking = false;

        public Transform crossPointGauche;
        public Transform crossPointDroit;

        public bool canGoLeft = false;
        public bool canGoRight = false;

   

        //TYPE OF ENNEMY
        public enum TypeOfEnnemy { Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone }
        public TypeOfEnnemy typeOfEnnemy;        

        void Awake()
        {

            Initialisation();
            
        }

        void Start()
        {
            speed = initialspeed;
            if (pushTest)
                return;
            StartingWay();            
        }

        //Ca c'est good à 100%
        void StartingWay()
        {
            randomSpawn = 0;//Random.Range(0, GameMaster.Instance.WayMaster.numberOfWay)            
            Debug.Log("randomspawn" + randomSpawn);

            if (randomSpawn == 0)
            {
                targetMovement = GameMaster.Instance.WayMaster.way01[0];
            }
            else if (randomSpawn == 1 && GameMaster.Instance.WayMaster.way2.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way02[0];
            }
            else if (randomSpawn == 2 && GameMaster.Instance.WayMaster.way3.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way03[0];
            }
            else if (randomSpawn == 3 && GameMaster.Instance.WayMaster.way4.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way04[0];
            }
            else if (randomSpawn == 4 && GameMaster.Instance.WayMaster.way5.activeInHierarchy)
            {
                targetMovement = GameMaster.Instance.WayMaster.way05[0];
            }
            else
            {
                randomSpawn = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[0];
            }
        }

        private void Update()
        {
            Debug.Log(targetMovement);

            //Check if enemy is being pushed by Tronçronce bullet
            if (isPushed || pushTest)
                return;

            //if (pushedCount >= maxPushes && !inResistance)
            //    StartCoroutine(ResistToPush());

            //Déplacements des ennemies
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            
           


            //limité le Max pour pas sortir de l'Array
            if (wayPointIndex > 5)
            {
                wayPointIndex = 5;
            }
            //limité le Min pour pas sortir de l'Array
            if (crossWayPointIndex > 3)
            {
                crossWayPointIndex = 3;
            }

            //Si y'a pas d'obstacle tu continues.
            if (defenseTarget == null)
            {
                speed = initialspeed;
               
            }
            

            if (droneIsInStation == true)
            {
                speed = stopSpeed;
            }
        }

        public void UpdateParent()
        {
            //crossPointGauche = parentRef.GetComponent<CrossBrain>().crosspointGauche;

//            crossPointDroit = parentRef.GetComponent<CrossBrain>().crosspointDroit;
        }

        public void NeedToCheck()
        {

                //Changer 4 quand plus de points
                if (wayPointIndex != 4)
                {
                    Debug.Log("proximité");

                    GetNextWaypoint();
                }
                else if (wayPointIndex >= GameMaster.Instance.WayMaster.way01.Length - 1)
                {
                    //Changer 4 quand plus de points
                    targetMovement = GameMaster.Instance.WayMaster.way01[4];
                }
                else if (wayPointIndex >= GameMaster.Instance.WayMaster.way02.Length - 1)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way01[4];
                }
                else if (wayPointIndex >= GameMaster.Instance.WayMaster.way03.Length - 1)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way01[4];
                }
                else if (wayPointIndex >= GameMaster.Instance.WayMaster.way04.Length - 1)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way01[4];
                }
                else if (wayPointIndex >= GameMaster.Instance.WayMaster.way05.Length - 1)
                {
                    targetMovement = GameMaster.Instance.WayMaster.way01[4];
                }


            
        }

        private void GetNextWaypoint()
        {
            //WAYPOINT FOR WAY01
            if (randomSpawn == 0)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way01.Length-1)
                {
                    if (!canMakeDamage && isNotDrone)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = stopSpeed;
                        StartCoroutine(EndPath());
                        return;
                    }
                    else if (!isNotDrone)
                    {
                        targetMovement = droneStation;
                    }
                }
                else
                {
                    Debug.Log("Else");
                  
                        Checking();
                    



                }
            }            

            //WAYPOINT FOR WAY02
           else if (randomSpawn == 1)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way02.Length-1)
                {
                    
                    if (!canMakeDamage && isNotDrone)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = stopSpeed;
                        StartCoroutine(EndPath());
                        return;
                    }
                    else if (!isNotDrone)
                    {
                        targetMovement = droneStation;
                    }
                }
                else
                {
                  
                        Checking();
                    
                    

                   
                }
            }
            

            //WAYPOINT FOR WAY03
            else if (randomSpawn == 2)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way03.Length - 1)
                {
                    Debug.Log("On est La");
                    if (!canMakeDamage && isNotDrone)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = stopSpeed;
                        StartCoroutine(EndPath());
                        return;
                    }
                    else if (!isNotDrone)
                    {
                        targetMovement = droneStation;
                    }
                }
                else
                {
                   
                        Checking();
                    

                }
            }
            

            //WAYPOINT FOR WAY04
            else if (randomSpawn == 3)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way04.Length - 1)
                {
                    if (!canMakeDamage && isNotDrone)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = stopSpeed;
                        StartCoroutine(EndPath());
                        return;
                    }
                    else if (!isNotDrone)
                    {
                        targetMovement = droneStation;
                    }
                }
                else
                {
                   
                        Checking();
                    

                }
            }
            

            //WAYPOINT FOR WAY05
            else if (randomSpawn == 4)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way05.Length - 1)
                {
                    if (!canMakeDamage && isNotDrone)
                    {
                        doingDamage = false;
                        canMakeDamage = true;
                        speed = stopSpeed;
                        StartCoroutine(EndPath());
                        return;
                    }
                    else if (!isNotDrone)
                    {
                        targetMovement = droneStation;
                    }
                }
                else
                {
                   
                        Checking();
                    

                }
            }            
        }

        // À chaque Waypoint, vérifie si un rempart ou une tourelle est proche et déclenche le bon comportement à avoir pour chaque type d'ennemis 
        private void Checking()
        {

            Debug.Log("Checking");
            //Checking des Défenses
            GameObject[] Defenses = GameObject.FindGameObjectsWithTag("Defense");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestDefense = null;
       

            int randomCrosspoint;

            foreach (GameObject Defense in Defenses)
            {
                float distanceToDefense = Vector3.Distance(transform.position, Defense.transform.position);
                if (distanceToDefense < shortestDistance)
                {
                    shortestDistance = distanceToDefense;
                    nearestDefense = Defense;
                }
            }

            if (nearestDefense != null && shortestDistance <= rangeForDefense)
            {                              
                defenseTarget = nearestDefense.transform;

                if (defenseTarget.GetComponent<DefenseType>().isTurret == true)
                {
                    doingDamage = false;
                    canMakeDamage = true;
                    speed = stopSpeed;
                    StartCoroutine(AttackOnTurret()); //Si type of ennemy is ...                    
                }
                else if (defenseTarget.GetComponent<DefenseType>().isRempart == true)
                {
                    speed = stopSpeed;

                    if (typeOfEnnemy == TypeOfEnnemy.Walker)
                    {
                        if (crossPointGauche.GetComponent<Crosspoints>().cantCross == true)
                        {
                            canGoLeft = false;
                            AlternativeChemin();
                        }
                        else if (crossPointDroit.GetComponent<Crosspoints>().cantCross == true)
                        {
                            canGoRight = false;
                            AlternativeChemin();
                        }

                        if (crossPointGauche.GetComponent<Crosspoints>().isOpen == true)
                        {
                            canGoLeft = true;
                        }

                        if (crossPointDroit.GetComponent<Crosspoints>().isOpen == true)
                        {
                            canGoRight = true;
                        }

                        if (canGoLeft && canGoRight)
                        {
                            randomCrosspoint = Random.Range(0, 2);

                            if (randomCrosspoint == 0)
                            {
                                GoingLeft();
                            }

                            if (randomCrosspoint == 1)
                            {
                                GoingRight();
                            }
                        }
                        else if (canGoLeft && !canGoRight)
                        {
                            GoingLeft();

                        }
                        else if (!canGoLeft && canGoRight)
                        {
                            GoingRight();

                        }
                        else if (!canGoLeft && !canGoRight)
                        {

                            doingDamage = false;
                            canMakeDamage = true;
                            StartCoroutine(AttackOnRempart()); //Si type of ennemy is ...  

                        }


                    }


                }
                else if (defenseTarget == null)
                {
                    defenseTarget = nearestDefense.transform;
                }
                
               

            }   //ELSE = Tu Continues ton chemin sans changer de lignes
            else 
            {

                AlternativeChemin();
                
            }

            
        }
        void AlternativeChemin()
        {
            if (randomSpawn == 0)
            {
                wayPointIndex++;
                targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
                speed = initialspeed;


            }
            if (randomSpawn == 1)
            {
              
                wayPointIndex++;
                targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
                speed = initialspeed;


            }
            if (randomSpawn == 2)
            {
            
                wayPointIndex++;
                targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];
                speed = initialspeed;


            }
            if (randomSpawn == 3)
            {
                wayPointIndex++;
                targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
                speed = initialspeed;


            }
            if (randomSpawn == 4)
            {
                wayPointIndex++;
                targetMovement = GameMaster.Instance.WayMaster.way05[wayPointIndex];
                speed = initialspeed;


            }

            
        }

        // Fait des dégâts à la tourelle tant qu'elle possède des points de vies  
        IEnumerator AttackOnTurret()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                doingDamage = true;
                defenseTarget.GetComponent<TurretTest>().currentHealth -= ennemyDamage;
                yield return new WaitForSeconds(0.3f);
                canMakeDamage = false;
            }
            Checking();
        
        }


        // Fait des dégâts au rempart tant qu'il possède des points de vies  
        IEnumerator AttackOnRempart()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                doingDamage = true;
                defenseTarget.GetComponent<RempartTest>().currentHealth -= ennemyDamage;
         
                yield return new WaitForSeconds(0.3f);
               
                canMakeDamage = false;

            }
            defenseTarget = null;
            Checking();

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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeForDefense);
        }

        void GoingLeft()
        {
            Debug.Log("GoingLeft");

            if (randomSpawn == 2)
            {
                randomSpawn = 4;
                targetMovement = GameMaster.Instance.WayMaster.way05[wayPointIndex];
            }
            else if (randomSpawn == 0)
            {
                randomSpawn = 2;
                targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];

            }
            else if (randomSpawn == 1)
            {
                randomSpawn = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
            }
            else if (randomSpawn == 3)
            {
                randomSpawn = 1;
                targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
            }

            /*if (randomSpawn == 4)
            {
                randomSpawn = 4;
            }*/

            speed = initialspeed;

            canGoLeft = false;
            canGoRight = false;
        }

        void GoingRight()
        {

            if (randomSpawn == 0)
            {
                randomSpawn = 1;
                targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
            }

            if (randomSpawn == 3)
            {
                randomSpawn = 3;
                targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
            }

            if (randomSpawn == 2)
            {
                randomSpawn = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
            }

            /*if (randomSpawn == 3)
            {
                randomSpawn = 3;
            }*/
            if (randomSpawn == 4)
            {
                randomSpawn = 2;
                targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];
            }

            speed = initialspeed;
            canGoLeft = false;
            canGoRight = false;
        }

        void Initialisation()
        {
            if (typeOfEnnemy == TypeOfEnnemy.Walker)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                rangeForDefense = 3f;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                initialspeed = 10f;
                ennemyDamage = 0;
                speedAttack = 0;
            }
        }
    }
}

