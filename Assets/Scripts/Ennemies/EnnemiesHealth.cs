using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class EnnemiesHealth : MonoBehaviour
    {
        //Health
        public int maxHealth = 100;
        public int currentHealth;

        public bool isInvincible = false;

        void Start()
        {
            currentHealth = maxHealth;
        }

        //Prise de Dégâts et Mort de l'ennemi
        public void TakeDammage(int damage)
        {
            if (!isInvincible)
            {
                currentHealth -= damage;
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

