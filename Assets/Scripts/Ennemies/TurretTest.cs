using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ennemies
{
	public class TurretTest : MonoBehaviour
	{
        [Header("Making Damage")]
        public Transform target;
        public float range = 15f;
        public int turretDamage;

        [Header("Life")]
        public int maxHealth;
        public int currentHealth;

		// Start is called before the first frame update
		void Start()
		{
            currentHealth = maxHealth;
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
		}

        private void Update()
        {
            UpdateTarget();            
        }

        //permet de prendre l'ennemy le plus proche dans la range en target et de lui infliger des dégâts 
        void UpdateTarget()
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
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
                //Inflige des dégâts aux ennemis 
                nearestEnnemy.gameObject.GetComponent<EnnemiesHealth>().TakeDammage(turretDamage);

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
        
    }
}

