using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;
using AudioManager;


namespace Turret
{
	/// <summary>
	/// CHB -- A hit scan turret
	/// </summary>
	public class Snipic : TurretBehaviour
	{
		#region Variables
		private EnnemiesHealth targetEnemy;
        private AudioSource audioSource;
        #endregion

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected override void UpdateTarget()
        {
			if (enemiesInRange.Count == 0 || broke)
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
			targetEnemy = nearestEnemy.GetComponent<EnnemiesHealth>();
		}


		protected override void Shoot()
        {
            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 1);
            audioSource.Play();

            anim.SetBool("Attack", true);
			targetEnemy.TakeDammage(damage);
			anim.SetBool("Attack", false);
		}
    }
}

