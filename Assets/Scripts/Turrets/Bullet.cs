using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	public class Bullet : MonoBehaviour
	{
		#region Variables
		private Rigidbody2D myRB;
		
		private Transform target;

		private Vector2 enemyDir;
		public float speed = 70f;

		[SerializeField] private float lifetime = 10f;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			myRB = gameObject.GetComponent<Rigidbody2D>();
		}

		public void Seek(Transform _target , Transform firePoint)
        {
			target = _target;
			enemyDir = target.position - firePoint.position;
        }

		// Update is called once per frame
		void Update()
		{
			if (target == null)
			{
				Destroy(gameObject);
				return;
			}

			myRB.velocity = enemyDir.normalized * speed;

			if(lifetime <= 0f)
            {
				Destroy(gameObject);
            }

			lifetime -= Time.deltaTime;
		}
	}
}

