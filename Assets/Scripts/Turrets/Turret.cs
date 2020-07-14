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
			Vector3 dir = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);
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
		}
	}
}

