using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class RempartTest : MonoBehaviour
	{
        [Header("Life")]
        public int maxHealth;
        public int currentHealth;

        void Awake()
		{
            currentHealth = maxHealth;
        }

        private void Update()
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

