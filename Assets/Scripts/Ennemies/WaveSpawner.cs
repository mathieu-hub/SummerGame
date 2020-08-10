using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        [SerializeField]
        private Transform walker;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private float timeBreak = 20f;

        [SerializeField]
        private float countdown = 2f;

        [SerializeField]
        private Text waveCountdownTimer;

        [SerializeField]
        private int waveIndex = 0;

        [SerializeField]
        private bool waveInProgress = false;



        void Update()
		{
            if (!waveInProgress)
            {
                if (countdown <= 0f)
                {
                    StartCoroutine(SpawnWave());
                    countdown = timeBreak;
                }

                //Fonctionnement du compte à rebourd 
                countdown -= Time.deltaTime;
                waveCountdownTimer.text = Mathf.Round(countdown).ToString();
            }
			
		}

        IEnumerator SpawnWave()
        {
            waveIndex++;
            waveInProgress = true;

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnnemy();
                yield return new WaitForSeconds(0.3f);
            }
        }

        //Permet de gérer les règles d'apparitions ennemis et de compositions de vagues.
        void SpawnEnnemy()
        {
            Instantiate(walker, spawnPoint.position, spawnPoint.rotation) ;
        }
	}
}

