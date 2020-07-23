using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;


namespace Turret
{
	/// <summary>
	/// CHB -- Basic turret class
	/// </summary>
	public class Turret : MonoBehaviour
	{
		#region Variables
		/*[SerializeField]*/protected Transform target = null;

		protected List<GameObject> enemiesInRange = new List<GameObject>();

		[Header("General")]
		
		[Range(1f, 40f)]
		public float range = 15f;

		[Header("Use Bullets (default)")]
		
		public GameObject bulletPrefab;
		
		[Range(0.1f, 50f)]
		public float fireRate = 1f;

		[Range(0.1f, 20f)]
		private float fireCountdown = 0f;

		[Header("Unity Setup Fields")]

		public string enemyTag = "Enemy";

		public Transform partToRotate;
		public float turnSpeed = 10f;

		public Transform firePoint;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			gameObject.GetComponent<CircleCollider2D>().radius = range;
			InvokeRepeating("UpdateTarget", 0f, 0.133f);
		}

		// Update is called once per frame
		void Update()
		{
			//To do : On enemy's death, need to remove him from this turret enemiesInRange list if it was added ?
			Debug.Log("Nb of enemies in range: " + enemiesInRange.Count);
			Debug.Log("Targeting: "+ target);
			if (target == null)
            {
				fireCountdown = 0f;
				return;
			}

			LockOnTarget();

			if(fireCountdown <= 0)
            {
				Shoot();
				fireCountdown = 1f / fireRate;
            }

			fireCountdown -= Time.deltaTime;
		}

		/// <summary>
		/// CHB -- Updates target to closest enemy in range
		/// </summary>
		protected virtual void UpdateTarget()
        {
			if (enemiesInRange.Count == 0)
            {
				target = null;
				return;
			}

			float shortestDistance = Mathf.Infinity;
			GameObject nearestEnemy = null;

			foreach (GameObject enemy in enemiesInRange)
			{
				float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
				if (distanceToEnemy < shortestDistance)
				{
					shortestDistance = distanceToEnemy;
					nearestEnemy = enemy;
				}
			}

			target = nearestEnemy.transform;
		}

        #region Collider call-backs
        private void OnTriggerEnter2D(Collider2D collision)
        {
			//Add enemy to in-range list
			if (collision.gameObject.tag == enemyTag)
            {
				enemiesInRange.Add(collision.gameObject);
				//EnnemiesHealth spottedEnemy = collision.gameObject.GetComponent<EnnemiesHealth>();
				//spottedEnemy.targettingTurrets.Add(gameObject);
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
        {
			//Remove enemy from in-range list
			if (collision.gameObject.tag == enemyTag)
            {
				//Checking if exiting enemy was previously added to enemiesInRange
				//Comment if really unneeded
				bool enemyFound = false;

				foreach(GameObject enemy in enemiesInRange)
                {
					if (enemy == collision.gameObject)
                    {
						enemyFound = true;
						break;
					}
                }

				if (!enemyFound)
					return;
				//End of commentable range

				enemiesInRange.Remove(collision.gameObject);
				//Enemy enemyLost = collision.gameObject.GetComponent<Enemy>();
				//enemyLost.targettingTurrets.Remove(gameObject);
			}
		}
        #endregion

		/// <summary>
		/// CHB -- Points firepoint towards target
		/// </summary>
        void LockOnTarget()
		{
			Vector3 posTarget = target.position;
			Vector3 posTurret = transform.position;
			Vector3 relativPos = new Vector3(posTarget.x - posTurret.x, posTarget.y - posTurret.y, 0);

			float angle = Mathf.Atan2(relativPos.y, relativPos.x) /** (180 / Mathf.PI)*/ * Mathf.Rad2Deg;

			if (angle < 0) angle += 360;

			partToRotate.rotation = Quaternion.Euler(0, 0, angle);
		}

		/// <summary>
		/// Spawn a bullet travelling to target
		/// </summary>
		protected virtual void Shoot()
        {
			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(target, firePoint);
        }

		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, range);
			Gizmos.DrawWireSphere(firePoint.position, 0.2f);
		}

        private void OnDrawGizmos()
        {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(firePoint.position, 0.2f);
		}
    }
}

