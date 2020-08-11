using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        //Permet de Composer les différentes vagues via divers variables (types d'ennemis, nbr max, taux ...)
        [Header("Wave Compositor (mettre 6 dans enney size)")]
        public WaveCompositor[] waves;

        //Séléction de l'ennemi
        [Header("Ennemy Slector")]
        private float ennemySelector;

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
            waveInProgress = true;

            WaveCompositor wave = waves[waveIndex];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnnemy(wave.ennemy);
                yield return new WaitForSeconds(wave.rate);
            }

            waveIndex++;
        }

        //Permet de gérer les règles d'apparitions ennemis et de compositions de vagues.
        void SpawnEnnemy(GameObject[] ennemy)
        {
            ennemySelector = Random.Range(0, 6);
            Debug.Log(ennemySelector);

            if (ennemySelector == 0)
            {
                Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                ennemyAlive++;
            }
            else if (ennemySelector == 1)
            {
                if (ennemy[1] != null)
                {
                    Instantiate(ennemy[1], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[1] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 2)
            {
                if (ennemy[2] != null)
                {
                    Instantiate(ennemy[2], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[2] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 3)
            {
                if (ennemy[3] != null)
                {
                    Instantiate(ennemy[3], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[3] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 4)
            {
                if (ennemy[4] != null)
                {
                    Instantiate(ennemy[4], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[4] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 5)
            {
                if (ennemy[5] != null)
                {
                    Instantiate(ennemy[5], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[5] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            
        }
	}
}

