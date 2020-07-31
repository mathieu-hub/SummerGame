using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

namespace Turret
{
	public class MortarExplosion : MonoBehaviour
	{
		#region Variables
		[SerializeField] private float explosionRadius;
		[SerializeField] private string enemyTag = "Enemy";

		[HideInInspector] public int damage;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			GetComponent<CircleCollider2D>().radius = explosionRadius;
		}


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == enemyTag)
            {
				collision.gameObject.GetComponent<EnnemiesHealth>().TakeDammage(damage);
			}
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
			if (collision.gameObject.tag == enemyTag)
			{
				collision.gameObject.GetComponent<EnnemiesHealth>().TakeDammage(damage);
			}
		}
    }
}

