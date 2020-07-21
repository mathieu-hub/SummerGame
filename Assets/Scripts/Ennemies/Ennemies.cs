using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class Ennemies : MonoBehaviour
	{
        //Health
        public int maxHealth = 100;
        public int currenthealth;
        
        //Movement
        public float speed = 10f;
        private Transform target;
        private int wayPointIndex = 0;

        void Start()
        {
            target = Waypoints.points[0];
            
        }


        //Prise de Dégâts et Mort de l'ennemi
        public void TakeDammage(int damage)
        {

            if (currenthealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
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
                StartCoroutine (EndPath());
                return;
            }
            wayPointIndex++;
            target = Waypoints.points[wayPointIndex];
        }

        // Fait des dégâts au Silo et se détruit une fois arrivé au dernier Waypoint.
        IEnumerator EndPath()
        {
            yield return new WaitForSeconds(2f);
            SiloLife.lives -= 1;
            Destroy(gameObject);
        }
    }
}

