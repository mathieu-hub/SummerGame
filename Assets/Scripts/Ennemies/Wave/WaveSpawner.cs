using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        //Glisser les prefabs ennemis pour ensuite les faire spawn
        [Header("Ennemy Prefab")]
        [SerializeField]
        private Transform walker;
        [SerializeField]
        private Transform soldonaute;
        [SerializeField]
        private Transform spaceScoot;
        [SerializeField]
        private Transform démolisseur;
        [SerializeField]
        private Transform carboniseur;
        [SerializeField]
        private Transform rover;
        [SerializeField]
        private Transform drone;

        //Le nombre d'ennemis en vie        
        public static int ennemyAlive = 0;

        [Header("SpawnPoint")]
        [SerializeField]
        private Transform spawnPoint;

        [Header("Time")]
        [SerializeField]
        private float timeBreak = 20f;
        [SerializeField]
        private float countdown = 2f;
        [SerializeField]
        private Text waveCountdownTimer;

        //Le Numéro de la vague en cours
        [Header("Wave Number")]
        [SerializeField]
        private int waveIndex = 0;

        //Vague en cours ou non
        [Header("Wave In Preogress")]
        [SerializeField]
        private bool waveInProgress = false;



        void Update()
		{
            if (ennemyAlive <= 0)
            {
                waveInProgress = false;
            }

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
            ennemyAlive++;
        }
	}
}

