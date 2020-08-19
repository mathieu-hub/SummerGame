using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemies
{
    public class NewEnnemiMovement : MonoBehaviour
    {

        #region Variables

        [Header("TypeOf")]
        public TypeOfEnnemy typeOfEnnemy;
        public bool isNotDrone;
        public enum TypeOfEnnemy { Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone }


        [Header("Values")]
        [SerializeField] private float speed;
        [SerializeField] private float initialspeed;
        [SerializeField] private float stopSpeed = 0f;
        [SerializeField] private int ennemyDamage;
        [SerializeField] private int speedAttack;
        public float rangeForDefense;

        [Header("Damage")]
        public bool canMakeDamage = true;
        public bool doingDamage = false;
        public bool canAttackTurret = false;
        public bool canAttackRempart = false;



        [Header("Ways")]
        [SerializeField] private int wayPointIndex = 0;
        public float currentWay;
        [SerializeField] private Transform targetMovement;
        private int random;

        public bool canLeft = false;
        public bool canRight = true;

        [Header("Reference GO")]
        public GameObject parentRef;
        public GameObject crossPointGauche;
        public GameObject crossPointDroit;
        public GameObject rempartTarget;
        public GameObject turretTarget = null;
        public GameObject turretTargetL = null;
        public GameObject turretTargetR = null;

        #region Push
        [Header("Push")]
        [Range(2, 10)]
        public int maxPushes = 2;
        [Range(0.5f, 20f)]
        [SerializeField] private float resistanceTime = 3f;
        [SerializeField] private bool pushTest = false;
        //private bool inResistance = false;

        [HideInInspector] public GameObject pushingBullet = null;
        [HideInInspector] public int pushedCount = 0;
        #endregion

        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            InitialisationValues();
            StartingWay();

            if (pushTest)
                return;
        }

        // Update is called once per frame
        void Update()
        {
            //Movements
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            LimitArray();
        }

        public void NeedToCheck()
        {
            crossPointGauche = parentRef.GetComponent<CrossBrain>().crosspointGauche;
            crossPointDroit = parentRef.GetComponent<CrossBrain>().crosspointDroit;

            canLeft = crossPointGauche.GetComponent<Crosspoints>().isOpen;
            canRight = crossPointDroit.GetComponent<Crosspoints>().isOpen;

            if (rempartTarget != null)
            {
                speed = stopSpeed;

                
                if (canLeft == true && canRight == false && crossPointGauche.GetComponent<Crosspoints>().cantCross == false)
                {
                    GoingLeft();
                }
                else if (canLeft == false && canRight == true && crossPointDroit.GetComponent<Crosspoints>().cantCross == false)
                {
                    Debug.Log("Right");
                    GoingRight();
                }
                else if (canLeft == true && canRight == true && crossPointDroit.GetComponent<Crosspoints>().cantCross == false && crossPointDroit.GetComponent<Crosspoints>().cantCross == false)
                {
                    Debug.Log("Double");
                    random = Random.Range(0, 2);
                    Debug.Log("Random = " + random);
                    if (random == 0)
                    {
                        GoingLeft();
                    }

                    if (random == 1)
                    {
                        GoingRight();
                    }
                }
                else 
                {
                    if(typeOfEnnemy == TypeOfEnnemy.Walker)
                    {
                        StartCoroutine("Attack");
                    }
                }
            }
            else
            {
                speed = initialspeed;
                doingDamage = false;
                canMakeDamage = true;
                GetNextWaypoint();
            }
        }

        //Change Waypoints ++
        public void GetNextWaypoint()
        {
            //WAYPOINT FOR WAY01
            if (currentWay == 0)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way01.Length - 1)
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
                        //targetMovement = droneStation;
                    }
                }
                else
                {
                    Debug.Log("Else");
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
                    
                }
            }

            //WAYPOINT FOR WAY02
            else if (currentWay == 1)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way02.Length - 1)
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
                        //targetMovement = droneStation;
                    }
                }
                else
                {
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
                }
            }


            //WAYPOINT FOR WAY03
            else if (currentWay == 2)
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
                        //targetMovement = droneStation;
                    }
                }
                else
                {
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];
                }
            }


            //WAYPOINT FOR WAY04
            else if (currentWay == 3)
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
                        //targetMovement = droneStation;
                    }
                }
                else
                {
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
                }
            }

            //WAYPOINT FOR WAY05
            else if (currentWay == 4)
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
                        //targetMovement = droneStation;
                    }
                }
                else
                {
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way05[wayPointIndex];
                }
            }
        }

        void InitialisationValues()
        {
            parentRef = null;
            crossPointGauche = null;
            crossPointDroit = null;
            rempartTarget = null;

            if (typeOfEnnemy == TypeOfEnnemy.Walker)
            {
                initialspeed = 10f;
                ennemyDamage = 10;
                speedAttack = 5;
                rangeForDefense = 3f;
                canAttackTurret = false;
                canAttackRempart = false;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = true;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = true;
                canAttackRempart = true;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = false;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = true;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                initialspeed = 10f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = true;
                canAttackRempart = true;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                initialspeed = 10f;
                ennemyDamage = 0;
                speedAttack = 0;
                canAttackTurret = false;
                canAttackRempart = false;

            }

            speed = initialspeed;
        }

        void StartingWay()
        {
            currentWay =0 /*Random.Range(0, GameMaster.Instance.WayMaster.numberOfWay)*/;
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

        void LimitArray()
        {
            //limité le Max pour pas sortir de l'Array
            if (wayPointIndex > 5)
            {
                wayPointIndex = 5;
            }

        }

        public void UpdateParent()
        {
            Debug.Log("updateParent");
            crossPointGauche = parentRef.GetComponent<CrossBrain>().crosspointGauche;
            crossPointDroit = parentRef.GetComponent<CrossBrain>().crosspointDroit;

            canLeft = crossPointGauche.GetComponent<Crosspoints>().isOpen;
            canRight = crossPointDroit.GetComponent<Crosspoints>().isOpen;

            Debug.Log(canRight && canLeft);

            /*turretTargetL = parentRef.GetComponent<CrossBrain>().leftTurret;
            turretTargetR = parentRef.GetComponent<CrossBrain>().rightTurret;

            float distanceToLeftTUrret = Vector3.Distance(transform.position, turretTargetL.transform.position);
            float distanceToRightTUrret = Vector3.Distance(transform.position, turretTargetR.transform.position);

            if(distanceToLeftTUrret < distanceToRightTUrret)
            {
                turretTarget = turretTargetL;
            }
            else
            {
                turretTarget = turretTargetR;
            }*/


            if (parentRef.GetComponent<CrossBrain>().rempart)
            {
                rempartTarget = parentRef.GetComponent<CrossBrain>().theRempart;
            }
        }
        void GoingLeft()
        {
            speed = initialspeed;
            doingDamage = false;
            canMakeDamage = true;
            Debug.Log("GoingLeft");

            if (currentWay == 2)
            {
                currentWay = 4;
                targetMovement = GameMaster.Instance.WayMaster.way05[wayPointIndex];
            }
            else if (currentWay == 0)
            {
                currentWay = 2;
                targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];

            }
            else if (currentWay == 1)
            {
                currentWay = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
            }
            else if (currentWay == 3)
            {
                currentWay = 1;
                targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
            }

            /*if (randomSpawn == 4)
            {
                randomSpawn = 4;
            }*/

          
            rempartTarget = null;

        }

        void GoingRight()
        {
            speed = initialspeed;
            doingDamage = false;
            canMakeDamage = true;

            if (currentWay == 0)
            {
                currentWay = 1;
                targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
            }

            else if(currentWay == 1)
            {
                currentWay = 3;
                targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
            }

            else if (currentWay == 3)
            {
                currentWay = 3;
                targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
            }

            else if (currentWay == 2)
            {
                currentWay = 0;
                targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
            }

            /*if (randomSpawn == 3)
            {
                randomSpawn = 3;
            }*/
            else if (currentWay == 4)
            {
                currentWay = 2;
                targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];
            }

           
            rempartTarget = null;


        }
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

        IEnumerator Attack()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                Debug.Log("A l'attaque");
                doingDamage = true;
                rempartTarget.GetComponent<RempartTest>().currentHealth -= ennemyDamage;

                yield return new WaitForSeconds(0.3f);

                canMakeDamage = false;
            }
            doingDamage = false;
            canMakeDamage = true;
            NeedToCheck();
        }
    }
}

