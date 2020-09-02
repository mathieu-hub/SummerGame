using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManager;

namespace Turret
{
    /// <summary>
    /// CHB -- Makes an explosion hitting all enemies in range
    /// </summary>
	public class MortarBullet : Bullet
	{
        #region Variables
        [SerializeField] private GameObject explosion;
        [SerializeField] private AudioSource audioSource;
        #endregion
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
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
            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 0);
            audioSource.Play();
            enemyDir = Vector2.zero;
            explosion.SetActive(true);
        }
    }
}

