using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class Ennemies : MonoBehaviour
	{
        public float speed = 10f;

        private Transform target;
        private int wayPointIndex = 0;

        void Start()
        {
            target = Waypoints.points[0];
        }

        private void Update()
        {
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
                //Destroy(gameObject);
                return;
            }
            wayPointIndex++;
            target = Waypoints.points[wayPointIndex];
        }
    }
}

