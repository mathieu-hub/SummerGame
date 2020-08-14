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

        void Start()
		{
            currentHealth = maxHealth;
        }	
	}
}

