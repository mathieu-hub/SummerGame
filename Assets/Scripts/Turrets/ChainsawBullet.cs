using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

namespace Turret
{
	public class ChainsawBullet : Bullet
	{
        #region Variables
        Rigidbody2D enemyRB;
        #endregion

        protected override void DealDamage(GameObject enemy)
        {
            enemyHit = enemy.GetComponent<EnnemiesHealth>();
            enemyHit.TakeDammage(damage);
            enemyRB = enemy.GetComponent<Rigidbody2D>();
            StartCoroutine(PushEnemy(enemyRB));
        }

        IEnumerator PushEnemy(Rigidbody2D _enemyRB)
        {
            _enemyRB.velocity = myRB.velocity;
            yield return new WaitUntil(() => lifetime <= 0);
            _enemyRB.velocity = Vector2.zero;
        }
    }
}

