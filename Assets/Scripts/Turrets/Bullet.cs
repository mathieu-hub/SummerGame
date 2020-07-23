using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	public class Bullet : MonoBehaviour
	{
		#region Variables
		protected Rigidbody2D myRB;
		
		private Transform target;

		protected Vector2 enemyDir;
		//protected EnemyHealth enemyHit;

		[SerializeField] private string enemyTag = "Enemy";
		[Range(1f, 100f)]
		[SerializeField] protected float speed = 70f;
		[Range(0.1f, 10f)]
		[SerializeField] protected float lifetime = 5f;
		[Range(1, 50)]
		[SerializeField] private int damage = 2;

		[SerializeField] private GameObject spawnPrefab;
		GameObject spawnPoint;

		private bool hasHit = false;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			myRB = gameObject.GetComponent<Rigidbody2D>();
			spawnPoint = Instantiate(spawnPrefab, transform.position, transform.rotation);
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
				Destroy(spawnPoint);
				Destroy(gameObject);
            }

			lifetime -= Time.deltaTime;
		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == enemyTag)
            {
				DealDamage(collision.gameObject);
            }
        }

		protected virtual void DealDamage(GameObject enemy)
        {
            if (!hasHit)
            {
				hasHit = true;
				//enemyHit = enemy.GetComponent<EnemyHealth>();
				//enemyHit.TakeDamage(damage);
			}
		}
    }
}

