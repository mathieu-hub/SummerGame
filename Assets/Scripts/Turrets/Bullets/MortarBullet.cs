using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
    /// <summary>
    /// CHB -- Makes an explosion hitting all enemies in range
    /// </summary>
	public class MortarBullet : Bullet
	{
        #region Variables
        [SerializeField] private GameObject explosion;
        #endregion
        private void Awake()
        {
            explosion.SetActive(false);
        }

        public override void Seek(Transform _target, Transform firePoint, int _damage)
        {
            base.Seek(_target, firePoint, _damage);
            explosion.GetComponent<MortarExplosion>().damage = damage;
        }

        protected override void EndLifetime()
        {
            DealDamage(null);
            base.EndLifetime();
        }

        protected override void DealDamage(GameObject enemy)
        {
            enemyDir = Vector2.zero;
            explosion.SetActive(true);
        }
    }
}

