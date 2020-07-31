using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;


namespace Turret
{
	public class Bullet : MonoBehaviour
	{
		#region Variables
		protected Rigidbody2D myRB;
		
		private Transform target;

		protected Vector2 enemyDir;

        [SerializeField] private string enemyTag = "Enemy";
		[Range(1f, 100f)]
		[SerializeField] protected float speed = 70f;
		[Range(0.1f, 10f)]
		[SerializeField] protected float lifetime = 5f;
		//[Range(1, 50)]
		/*[SerializeField]*/ protected int damage = 2;

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

		/// <summary>
		/// CHB -- Set target and damage, called by Turret
		/// </summary>
		/// <param name="_target"></param>
		/// <param name="firePoint"></param>
		/// <param name="_damage"></param>
		public virtual void Seek(Transform _target , Transform firePoint, int _damage)
        {
			target = _target;
			damage = _damage;
			enemyDir = target.position - firePoint.position;
        }

		// Update is called once per frame
		void Update()
		{
			if (target == null)
			{
				Destroy(spawnPoint);
				Destroy(gameObject);
				return;
			}

			myRB.velocity = enemyDir.normalized * speed;

			if(lifetime <= 0f)
            {
				EndLifetime();
            }

			lifetime -= Time.deltaTime;
		}

		protected virtual void EndLifetime()
        {
			Destroy(spawnPoint);
			Destroy(gameObject);
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

				EnnemiesHealth enemyHit = enemy.GetComponent<EnnemiesHealth>();
                enemyHit.TakeDammage(damage);

				Destroy(spawnPoint);
				Destroy(gameObject);
			}
		}
    }
}

