using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        [SerializeField]
        private Transform ennemyPrefab;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private float timeBreak = 20f;

        private float countdown = 2f;

        [SerializeField]
        private Text waveCountdownTimer;

        private int waveIndex = 0;

		void Update()
		{
			if (countdown <= 0f)
            {
                StartCoroutine (SpawnWave());
                countdown = timeBreak;
            }

            countdown -= Time.deltaTime;
            waveCountdownTimer.text = Mathf.Round(countdown).ToString();
		}

        IEnumerator SpawnWave()
        {
            waveIndex++;

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }

        void SpawnEnnemy()
        {
            Instantiate(ennemyPrefab, spawnPoint.position, spawnPoint.rotation) ;
        }
	}
}

