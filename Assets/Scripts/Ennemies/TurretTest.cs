using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ennemies
{
	public class TurretTest : MonoBehaviour
	{
        public Transform target;
        public float range = 15f;
        public int turretDamage;

		// Start is called before the first frame update
		void Start()
		{
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
		}

        private void Update()
        {
            UpdateTarget();
            
        }
        void UpdateTarget()
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnnemy = null;

            foreach (GameObject ennemy in ennemies)
            {
                float distanceToEnnemy = Vector3.Distance(transform.position, ennemy.transform.position);
                if (distanceToEnnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnnemy;
                    nearestEnnemy = ennemy;
                }
            }

            if(nearestEnnemy != null && shortestDistance <= range)
            {
                target = nearestEnnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        //Infliger des Dégâts aux Ennemis
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemy"))
            {
                Debug.Log("isInRange");
                EnnemiesHealth ennemieHealth = collision.transform.GetComponent<EnnemiesHealth>();
                ennemieHealth.TakeDammage(turretDamage);
            }
        }
    }
}

