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
        public float speed = 10f;
        [SerializeField] private Transform target;
        [SerializeField] private int wayPointIndex = 0;

        [HideInInspector] public bool isPushed = false;

        //Make Damage
        [Header("Damage")]
        public bool canMakeDamage = false;
        public bool doingDamage = false;
        public int ennemyDamage;
        public int speedAttack;

        [Header("Push")]
        [HideInInspector] public GameObject pushingBullet = null;
        [HideInInspector] public int pushedCount = 0;
        [Range(2, 10)]
        public int maxPushes = 2;
        [Range(0.5f, 20f)]
        [SerializeField] private float resistanceTime = 3f;
        private bool inResistance = false;

        void Start()
        {
            //StartingWay();
            target = GameMaster.Instance.WayMaster.way01[0];
        }

        private void Update()
        {
            //Check if enemy is being pushed by Tronçronce bullet
            if (isPushed)
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
            //Répéter pour chaque way
            if (wayPointIndex >= GameMaster.Instance.WayMaster.way01.Length - 1)
            {
                if (!canMakeDamage)
                {
                    doingDamage = false;
                    canMakeDamage = true;
                    speed = 0f;
                    StartCoroutine(EndPath());
                    Debug.Log("called");
                    return;
                }

            }
            else
            {
                wayPointIndex++;
                target = GameMaster.Instance.WayMaster.way01[wayPointIndex];
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

