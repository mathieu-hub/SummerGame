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
        List<EnnemiesMovement> enemiesPushed = new List<EnnemiesMovement>();
        [SerializeField] private float pushTime;
        #endregion

        protected override void DealDamage(GameObject enemy)
        {
            EnnemiesMovement enemyMov = enemy.GetComponent<EnnemiesMovement>();

            if(enemyMov.pushedCount >= enemyMov.maxPushes)
                return;

            if(enemyMov.pushingBullet != null)
                enemyMov.pushingBullet.GetComponent<ChainsawBullet>().enemiesPushed.Remove(enemyMov);
            
            enemyMov.pushingBullet = gameObject;
            enemyMov.pushedCount++;

            EnnemiesHealth enemyHit = enemy.GetComponent<EnnemiesHealth>();
            Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
            
            enemiesPushed.Add(enemyMov);

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
            if(_enemyMov.pushingBullet == gameObject)
            {
                _enemyMov.isPushed = false;
                if (_enemyMov.pushedCount >= _enemyMov.maxPushes)
                    _enemyMov.DoResistToPush();
                enemiesPushed.Remove(_enemyMov);
            }
        }

        private void OnDestroy()
        {
            if (enemiesPushed.Count > 0)
            {
                foreach (EnnemiesMovement enemyMov in enemiesPushed)
                {
                    if (enemyMov.pushingBullet == gameObject)
                    {
                        enemyMov.isPushed = false;
                        if (enemyMov.pushedCount >= enemyMov.maxPushes)
                            enemyMov.DoResistToPush();
                        enemiesPushed.Remove(enemyMov);
                    }
                        
                }
            }
        }
    }
}

