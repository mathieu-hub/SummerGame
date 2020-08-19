using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;


namespace Turret
{
	/// <summary>
	/// CHB -- A hit scan turret
	/// </summary>
	public class Snipic : TurretBehaviour
	{
		#region Variables
		private EnnemiesHealth targetEnemy;
        #endregion

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
            targetEnemy.TakeDammage(damage);
        }
    }
}

