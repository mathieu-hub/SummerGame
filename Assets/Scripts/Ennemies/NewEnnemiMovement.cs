using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Turret;
using AudioManager;

namespace Ennemies
{
    public class NewEnnemiMovement : MonoBehaviour
    {

        #region Variables

        [Header("TypeOf")]
        public TypeOfEnnemy typeOfEnnemy;
        public bool isNotDrone;
        public enum TypeOfEnnemy { Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone }

        public bool isAdd;

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
        
        [Header("Shoot")]
        public GameObject projectile;
        public bool shooting = false;
        public Transform targetToShoot;
        [SerializeField] private float timeBtwShots;
        [SerializeField] [Range(0f, 2f)] private float startTimeBtwShots;

        [Header("Ways")]
        public int wayPointIndex = 0;
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

        //DroneStation
        public Transform droneStation = null;
        public bool droneIsInStation = false;

        public AudioSource audioSource;

        private SpriteRenderer spriteRend;

        private Color startColor;

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

        [Header("Animator")]
        public Animator animator;

        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            InitialisationValues();
            StartingWay();

            if (pushTest)
                return;
        }
        private void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();

            startColor = spriteRend.color;

            if(typeOfEnnemy != TypeOfEnnemy.SpaceScoot)
            {
                projectile = null;
                targetToShoot = null;
            }
        }



        // Update is called once per frame
        void Update()
        {
            //Movements
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            LimitArray();


            //animations
            if (speed == 0)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (shooting == true)
            {
                ShootProjectile();
            }

            //Le Drone arrive dans DroneStation, il s'arrête.
            if (droneIsInStation == true)
            {
                speed = stopSpeed;
            }
            //le Drone est dans la station est la jauge de recherche est complète.
            if (droneIsInStation == true && ResearchBar.barIsComplete == true)
            {
                WaveSpawner.ennemyAlive--;
                Destroy(gameObject);
            }
        }

        public void NeedToCheck()
        {
            UpdateParent();
            Debug.Log("needToCheck");

            //Si rempart alors on attaque
            if (rempartTarget != null)
            {
                speed = stopSpeed;

                if(typeOfEnnemy == TypeOfEnnemy.Carboniseur)
                {
                    StartCoroutine(AttackRempart());
                }
                //Si il y a un rempart mais que les deux points sont ouverts alors je me déplace;
                else if(canLeft || canRight && typeOfEnnemy != TypeOfEnnemy.Carboniseur)
                {
                    //test gauche droite blabla
                    if (canLeft == true && canRight == false && crossPointGauche.GetComponent<Crosspoints>().cantCross == false)
                    {
                        if(typeOfEnnemy == TypeOfEnnemy.Démolisseur)
                        {
                            /*Debug.Log("Bug1");

                            int chance = Random.Range(0, 11);

                            if(chance <= 2 && crossPointDroit.GetComponent<Crosspoints>().cantCross == false)
                            {
                                chance = 0;

                                parentRef.GetComponent<CrossBrain>().crosspointDroit.GetComponent<Crosspoints>().isOpen = true;
                                NeedToCheck();
                            }
                            else
                            {
                                chance = 0;
                                GoingLeft();
                            }*/
                            GoingLeft();
                        }
                        else
                        {
                            GoingLeft();
                        }

                        
                    }
                    else if (canLeft == false && canRight == true && crossPointDroit.GetComponent<Crosspoints>().cantCross == false)
                    {
                        if(typeOfEnnemy == TypeOfEnnemy.Démolisseur)
                        {

                            /*Debug.Log("Bug2");
                             int chance = Random.Range(0, 11);

                             if (chance <= 2 && crossPointGauche.GetComponent<Crosspoints>().cantCross == false)
                             {
                                 chance = 0;

                                 parentRef.GetComponent<CrossBrain>().crosspointGauche.GetComponent<Crosspoints>().isOpen = true;
                                 NeedToCheck();
                             }
                             else
                             {
                                 chance = 0;
                                 GoingLeft();
                             }*/
                            GoingRight();
                        }
                        else
                        {
                            
                            GoingRight();
                        }

                       
                    }
                    else if (canLeft == true && canRight == true && crossPointDroit.GetComponent<Crosspoints>().cantCross == false && crossPointDroit.GetComponent<Crosspoints>().cantCross == false)
                    {
                        Debug.Log("Bug3");
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

                    //Si aucun des deux n'est ouvert alors je dois attaquer//Créer un chemin//Rien faire
                }
                else
                {  //Créer Passages ou attaquer
                    if(typeOfEnnemy == TypeOfEnnemy.Démolisseur)
                    {
                        
                       Debug.Log("on est dans le démo");
                        //Créer des passages

                        bool canDigLeft = false;
                        bool canDigRight = true;

                        if (parentRef.GetComponent<CrossBrain>().crosspointGauche.GetComponent<Crosspoints>().cantCross == false)
                        {
                            canDigLeft = true;
                        }

                        if(parentRef.GetComponent<CrossBrain>().crosspointDroit.GetComponent<Crosspoints>().cantCross == false)
                        {
                            canDigRight = true;
                        }

                        

                       
                        if (canDigLeft && !canDigRight)
                        {
                            parentRef.GetComponent<CrossBrain>().crosspointGauche.GetComponent<Crosspoints>().isOpen = true;
                            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 11);
                            audioSource.Play();
                            NeedToCheck();
                        }
                        else if (!canDigLeft && canDigRight)
                        {
                            parentRef.GetComponent<CrossBrain>().crosspointDroit.GetComponent<Crosspoints>().isOpen = true;
                            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 11);
                            audioSource.Play();
                            NeedToCheck();
                        }else if(canDigLeft && canDigRight)
                        {
                            int randomCreating = Random.Range(0, 2);
                            Debug.Log("randomCreating " + randomCreating);
                            if (randomCreating == 0)
                            {
                                parentRef.GetComponent<CrossBrain>().crosspointGauche.GetComponent<Crosspoints>().isOpen = true;
                                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 11);
                                audioSource.Play();
                                NeedToCheck();
                            }
                            else
                            {
                                parentRef.GetComponent<CrossBrain>().crosspointDroit.GetComponent<Crosspoints>().isOpen = true;
                                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 11);
                                audioSource.Play();
                                NeedToCheck();
                            }

                            

                        }
                        else
                        {
                            StartCoroutine(WaitHere());
                        }

                       
                        

                    }
                    //si je peux attaquer rempart j'attaque
                    else
                    {

                        if (canAttackRempart)
                        {
                            Debug.Log("on est dans l'attaque");
                            StartCoroutine(AttackRempart());

                        }
                        else if (canAttackTurret && (turretTargetL !=null || turretTargetR != null))
                        {
                            if (turretTarget == null)
                            {
                                int maxRange = 0;

                                if (turretTargetL != null)
                                {
                                    maxRange += 1;
                                }
                                if (turretTargetR != null)
                                {
                                    maxRange += 1;
                                }

                                int sortRandom = Random.Range(0, maxRange);

                                if (sortRandom == 0)
                                {
                                    turretTarget = turretTargetL;

                                }
                                if (sortRandom == 1)
                                {
                                    turretTarget = turretTargetR;
                                }

                                StartCoroutine("AttackTurret");
                                maxRange = 0;
                            }
                            else
                            {
                                StartCoroutine("AttackTurret");
                            }

                        }
                        else if((canAttackTurret && turretTargetL == null && turretTargetR == null))
                        {
                            Debug.Log("Ici");
                            //Je check Constament que l'on m'ouvre la voie
                            StartCoroutine(WaitHere());
                            
                        }
                    }
                    
                    
                }

            }//Je continues mon chemin car pas de remparts
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
                if (wayPointIndex == GameMaster.Instance.WayMaster.way01.Length-1)
                {
                    Debug.Log("On est la");

                    if (canMakeDamage && isNotDrone)
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
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way01[wayPointIndex];
                    
                }
            }

            //WAYPOINT FOR WAY02
            else if (currentWay == 1)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way02.Length -1 )
                {
                 
                    if (canMakeDamage && isNotDrone)
                    {
                        Debug.Log("EndPath");
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
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way02[wayPointIndex];
                }
            }


            //WAYPOINT FOR WAY03
            else if (currentWay == 2)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way03.Length - 1)
                {
                  
                  
                    if (canMakeDamage && isNotDrone)
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
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way03[wayPointIndex];
                }
            }


            //WAYPOINT FOR WAY04
            else if (currentWay == 3)
            {
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way04.Length - 1)
                {
           
                    if (canMakeDamage && isNotDrone)
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
                    wayPointIndex++;
                    targetMovement = GameMaster.Instance.WayMaster.way04[wayPointIndex];
                }
            }

            //WAYPOINT FOR WAY05
            else if (currentWay == 4)
            {
          
                if (wayPointIndex >= GameMaster.Instance.WayMaster.way05.Length - 1)
                {
                    if (canMakeDamage && isNotDrone)
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
                initialspeed = 5f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = true;
                animator.SetBool("isSoldonaute", true);
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                initialspeed = 5f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = true;
                canAttackRempart = false;
                animator.SetBool("isScout", true);
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                initialspeed = 3f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = false;
                animator.SetBool("IsDestructeur", true);
                Debug.Log("bien lu");
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                initialspeed = 5f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = false;
                canAttackRempart = true;
                animator.SetBool("isCarboniseur", true);
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                initialspeed = 3f;
                ennemyDamage = 20;
                speedAttack = 5;
                canAttackTurret = true;
                canAttackRempart = true;
                animator.SetBool("isRover", true);
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                initialspeed = 6f;
                ennemyDamage = 0;
                speedAttack = 0;
                canAttackTurret = false;
                canAttackRempart = false;
                animator.SetBool("isDrone", true);

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

            turretTargetL = parentRef.GetComponent<CrossBrain>().leftTurret;
            turretTargetR = parentRef.GetComponent<CrossBrain>().rightTurret;

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

        void ShootProjectile()
        {
            if(typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                if (turretTarget != null)
                {
                    targetToShoot = turretTarget.transform;
                }
                else
                {
                    targetToShoot = null;
                }

                if (timeBtwShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;

                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 12);
                    audioSource.Play();
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            
        }


        IEnumerator EndPath()
        {
         

            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                if(typeOfEnnemy == TypeOfEnnemy.Carboniseur)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(gameObject.GetComponent<AudioSource>(), 10);
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(gameObject.GetComponent<AudioSource>(), 11);
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(gameObject.GetComponent<AudioSource>(), 13);
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(gameObject.GetComponent<AudioSource>(), 12);
                    gameObject.GetComponent<AudioSource>().Play();
                }



                canMakeDamage = false;
                doingDamage = true;
                SiloLife.lives -= ennemyDamage;
                yield return new WaitForSeconds(0.3f);
                canMakeDamage = true;
                doingDamage = false;
            }

            StartCoroutine("EndPath");
        }

        IEnumerator AttackRempart()
        {
            spriteRend.color = Color.red;

            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage && parentRef.GetComponent<CrossBrain>().theRempart != null)
            {
                if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 10);
                    audioSource.Play();
                }

                if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 12);
                    audioSource.Play();
                }

                if (typeOfEnnemy == TypeOfEnnemy.Rover)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 12);
                    audioSource.Play();
                }


                shooting = true;
                spriteRend.color = startColor;

                Debug.Log("A l'attaque");
                doingDamage = true;
                rempartTarget.GetComponent<RempartTest>().currentHealth -= ennemyDamage;

                yield return new WaitForSeconds(0.3f);
                doingDamage = false;
                canMakeDamage = true;
                shooting = false;
                NeedToCheck();

              
            }
            else
            {
                NeedToCheck();
            }
            
            
        }

        IEnumerator WaitHere()
        {
            if(parentRef.GetComponent<CrossBrain>().theRempart == null)
            {
                NeedToCheck();
            }
            else
            {
                yield return new WaitForSeconds(speedAttack);
                NeedToCheck();
            }

            
        }

        IEnumerator AttackTurret()
        {
            spriteRend.color = Color.red;
           

            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage && turretTarget != null)
            {
                shooting = true;
                spriteRend.color = startColor;

                Debug.Log("A l'attaque");
                doingDamage = true;
                turretTarget.GetComponent<TurretParent>().currentHp -= ennemyDamage;

                yield return new WaitForSeconds(0.3f);
                doingDamage = false;
                canMakeDamage = true;
                shooting = false;

                if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 10);
                    audioSource.Play();
                }

                if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 12);
                    audioSource.Play();
                }

                if (typeOfEnnemy == TypeOfEnnemy.Rover)
                {
                    SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 12);
                    audioSource.Play();
                }


            }
            else
            {
                NeedToCheck();
            }

        }
       
        

    }
}

