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
        [SerializeField] private float pushTime;
        #endregion

        protected override void DealDamage(GameObject enemy)
        {
            enemyHit = enemy.GetComponent<EnnemiesHealth>();
            enemyRB = enemy.GetComponent<Rigidbody2D>();

            enemyHit.TakeDammage(damage);
            
            StartCoroutine(PushEnemy(enemyRB));
        }

        IEnumerator PushEnemy(Rigidbody2D _enemyRB)
        {
            _enemyRB.velocity = myRB.velocity;
            yield return new WaitForSeconds(pushTime);
            _enemyRB.velocity = Vector2.zero;
        }

        private void OnDestroy()
        {
            enemyRB.velocity = Vector2.zero;
        }
    }
}

