using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

namespace Turret
{
	public class ChainsawBullet : Bullet
	{
        #region Variables
        List<Rigidbody2D> enemiesPushed = new List<Rigidbody2D>();
        [SerializeField] private float pushTime;
        #endregion

        protected override void DealDamage(GameObject enemy)
        {
            EnnemiesHealth enemyHit = enemy.GetComponent<EnnemiesHealth>();
            Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
            enemiesPushed.Add(enemyRB);

            enemyHit.TakeDammage(damage);
            
            StartCoroutine(PushEnemy(enemyRB));
        }

        IEnumerator PushEnemy(Rigidbody2D _enemyRB)
        {
            _enemyRB.velocity = myRB.velocity;
            yield return new WaitForSeconds(pushTime);
            _enemyRB.velocity = Vector2.zero;
            enemiesPushed.Remove(_enemyRB);
        }

        private void OnDestroy()
        {
            if (enemiesPushed.Count > 0)
            {
                foreach(Rigidbody2D enemy in enemiesPushed)
                {
                    enemy.velocity = Vector2.zero;
                    enemiesPushed.Remove(enemy);
                }
            }
        }
    }
}

