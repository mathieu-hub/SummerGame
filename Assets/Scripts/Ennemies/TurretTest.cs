using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class TurretTest : MonoBehaviour
	{
        private Transform target;
        public float range = 15f;

		// Start is called before the first frame update
		void Start()
		{
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
    }
}

