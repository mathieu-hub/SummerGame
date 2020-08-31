using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Ennemies
{
	public class Projectile : MonoBehaviour
	{
        public float speed;

        private Transform turret;
        private Vector2 target;

        void Start()
        {
            turret = GetComponent<NewEnnemiMovement>().targetToShoot;

            target = new Vector2(turret.position.x, turret.position.y);
            
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if(transform.position.x == target.x && transform.position.y == target.y)
            {
                DestroyProjectile();
            }
        }


        void DestroyProjectile()
        {
            Destroy(gameObject);
        }

		
	}
}

