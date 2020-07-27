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
        [SerializeField] private float explosionRadius;
        #endregion

        protected override void EndLifetime()
        {
            DealDamage(null);
            base.EndLifetime();
        }

        protected override void DealDamage(GameObject enemy)
        {
            //EXPLOSION
        }
    }
}

