using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

namespace Turret
{
    /// <summary>
    /// CHB -- Pushes any enemy for a short time
    /// </summary>
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
            EnnemiesMovement enemyMov = enemy.GetComponent<EnnemiesMovement>();
            enemiesPushed.Add(enemyRB);

            enemyHit.TakeDammage(damage);
            
            //if(!enemyMov.isPushed) ??
            StartCoroutine(PushEnemy(enemyRB, enemyMov));
        }

        IEnumerator PushEnemy(Rigidbody2D _enemyRB, EnnemiesMovement _enemyMov)
        {
            _enemyMov.isPushed = true;
            _enemyRB.velocity = myRB.velocity;
            yield return new WaitForSeconds(pushTime);
            _enemyRB.velocity = Vector2.zero;
            _enemyMov.isPushed = false;
            enemiesPushed.Remove(_enemyRB);
        }

        private void OnDestroy()
        {
            if (enemiesPushed.Count > 0)
            {
                foreach(Rigidbody2D enemy in enemiesPushed)
                {
                    enemy.velocity = Vector2.zero;
                    enemy.gameObject.GetComponent<EnnemiesMovement>().isPushed = false;
                    enemiesPushed.Remove(enemy);
                }
            }
        }
    }
}

