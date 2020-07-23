using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	/// <summary>
	/// CHB -- A hit scan turret
	/// </summary>
	public class Snipic : Turret
	{
		#region Variables
		//private EnemyHealth targetEnemy
		[SerializeField] private int damage;
        #endregion

        protected override void UpdateTarget()
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
			//targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();
		}
        protected override void Shoot()
        {
            //targetEnemy.TakeDamage(damage);
        }
    }
}

