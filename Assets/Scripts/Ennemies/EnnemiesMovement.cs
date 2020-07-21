using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class EnnemiesMovement : MonoBehaviour
    {
        //Movement
        public float speed = 10f;
        private Transform target;
        private int wayPointIndex = 0;

        //Make Damage
        public bool canMakeDamage = true;
        public int ennemyDamage;

        void Start()
        {
            target = Waypoints.points[0];

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
        }

        private void GetNextWaypoint()
        {
            if (wayPointIndex >= Waypoints.points.Length - 1)
            {
                speed = 0f;
                StartCoroutine(EndPath());
                return;
            }
            wayPointIndex++;
            target = Waypoints.points[wayPointIndex];
        }

        // Fait des dégâts au Silo et se détruit une fois arrivé au dernier Waypoint.
        IEnumerator EndPath()
        {
            yield return new WaitForSeconds(2f);
            if (canMakeDamage)
            {
                SiloLife.lives -= ennemyDamage;
                canMakeDamage = false;
            } 
            
            /*if (!canMakeDamage)
            {
                yield return new WaitForSeconds()
            }*/
        }
    }
}

