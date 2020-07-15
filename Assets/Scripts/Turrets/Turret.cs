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
			Vector3 dir = new Vector3(0, 0, Mathf.Atan2(target.position.x, target.position.y) * 180 / Mathf.PI);
			Quaternion orientationQuaternion = Quaternion.Euler(- dir);
			partToRotate.transform.rotation = orientationQuaternion;
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
			Gizmos.DrawWireSphere(firePoint.position, 0.3f);
		}
	}
}

