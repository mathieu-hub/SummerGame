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

        //Make Damage
        [Header("Damage")]
        public bool canMakeDamage = false;
        public bool doingDamage = false;
        public int ennemyDamage;
        public int speedAttack;

        void Start()
        {
            //StartingWay();
            target = GameMaster.Instance.WayMaster.way01[0];
        }

        private void Update()
        {
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
        
    }
}

