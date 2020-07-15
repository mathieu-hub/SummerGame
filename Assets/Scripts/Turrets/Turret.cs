using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	public class Turret : MonoBehaviour
	{
		#region Variables
		[SerializeField]private Transform target;

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
		}

		// Update is called once per frame
		void Update()
		{
			LockOnTarget();

			if(fireCountdown <= 0)
            {
				Shoot();
				fireCountdown = 1 / fireRate;
            }

			fireCountdown -= Time.deltaTime;
		}

		void LockOnTarget()
		{
			Vector3 posTarget = target.position;
			Vector3 posTurret = transform.position;
			Vector3 relativPos = new Vector3(posTarget.x - posTurret.x, posTarget.y - posTurret.y, 0);

			float angle = Mathf.Atan2(relativPos.y, relativPos.x) /** (180 / Mathf.PI)*/ * Mathf.Rad2Deg;

			if (angle < 0) angle += 360;

			partToRotate.rotation = Quaternion.Euler(0, 0, angle);
		}

		void Shoot()
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

