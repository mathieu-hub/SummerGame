using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        [SerializeField]
        private Transform ennemyPrefab;
        [SerializeField]
        private float timeBreak = 20f;

        private float countdown = 2f;

		void Update()
		{
			if (countdown <= 0f)
            {
                SpawnWave();
                countdown = timeBreak;
            }

            countdown -= Time.deltaTime;
		}

        void SpawnWave()
        {
            Debug.Log("une nouvelle vague apparaît");
        }
	}
}

