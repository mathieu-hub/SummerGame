using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using AudioManager;

namespace Ennemies
{
	public class WaveSpawner : MonoBehaviour
	{
        private AudioSource audioSource;

        //Permet de Composer les différentes vagues via divers variables (types d'ennemis, nbr max, taux ...)
        [Header("Wave Compositor (mettre 6 dans enney size)")]
        public WaveCompositor[] waves;

        //Séléction de l'ennemi
        [Header("Ennemy Slector")]
        private float ennemySelector;

        //Le nombre d'ennemis en vie        
        public static int ennemyAlive = 0;
        public int ennemyAliveDisplay;

        //Le nombre d'ennemis présent pour chaque type        
        [Header("Ennemy Type Here")]
        public int ennemy01Here = 0;
        public int ennemy02Here = 0;
        public int ennemy03Here = 0;
        public int ennemy04Here = 0;
        public int ennemy05Here = 0;        

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
        public static int waveIndex;

        public static int rounds; //Pour l'UI de GAME OVER
        [SerializeField]
        private TextMeshProUGUI waveNumber;

        //Vague en cours ou non
        [Header("Wave In Preogress")]
        [SerializeField]
        private bool waveInProgress = false;        

        private void Start()
        {
            waveIndex = 0;
            rounds = 0;

            audioSource = GetComponent<AudioSource>();
        }

        void Update()
		{

            ennemyAliveDisplay = ennemyAlive;

            //Debug.Log("Wave" + waveIndex);

            if (ennemyAlive <= 0)
            {
                waveInProgress = false;

                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 22);
                audioSource.Play();
            }

            if (!waveInProgress)
            {
                

                if (countdown <= 0f && Cinématique.lastBool)
                {
                    ennemy01Here = 0;
                    ennemy02Here = 0;
                    ennemy03Here = 0;
                    ennemy04Here = 0;
                    ennemy05Here = 0;
                    StartCoroutine(SpawnWave());
                    countdown = timeBreak;
                }

                //Fonctionnement du compte à rebourd 
                countdown -= Time.deltaTime;
                waveCountdownTimer.text = Mathf.Round(countdown).ToString();
            }

            //Wave Number UI 
            waveNumber.text = waveIndex.ToString(); 
		}

        IEnumerator SpawnWave()
        {
            waveInProgress = true;

            WaveCompositor wave = waves[waveIndex];

            GameManager.Instance.wavesBeforeSeller -= 1;
            waveIndex++;
            rounds++;

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnnemy(wave.ennemy);
                yield return new WaitForSeconds(wave.rate);
            }

        }

        //Permet de gérer les règles d'apparitions ennemis et de compositions de vagues.
        void SpawnEnnemy(GameObject[] ennemy)
        {
            WaveCompositor wave = waves[waveIndex];

            ennemySelector = Random.Range(0, 6);
            Debug.Log(ennemySelector);

            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 20);
            audioSource.Play();

            if (ennemySelector == 0)
            {
                Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                ennemyAlive++;
            }
            else if (ennemySelector == 1 && ennemy01Here <= wave.maxEnnemy01)
            {
                if (ennemy[1] != null)
                {
                    ennemy01Here++;
                    Instantiate(ennemy[1], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[1] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 2 && ennemy02Here <= wave.maxEnnemy02)
            {
                if (ennemy[2] != null)
                {
                    ennemy02Here++;
                    Instantiate(ennemy[2], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[2] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 3 && ennemy03Here <= wave.maxEnnemy03)
            {
                if (ennemy[3] != null)
                {
                    ennemy03Here++;
                    Instantiate(ennemy[3], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[3] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 4 && ennemy04Here <= wave.maxEnnemy04)
            {
                if (ennemy[4] != null)
                {
                    ennemy04Here++;
                    Instantiate(ennemy[4], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[4] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else if (ennemySelector == 5 && ennemy05Here <= wave.maxEnnemy05)
            {
                if (ennemy[5] != null)
                {
                    ennemy05Here++;
                    Instantiate(ennemy[5], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
                else if (ennemy[5] == null)
                {
                    Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                    ennemyAlive++;
                }
            }
            else
            {
                Instantiate(ennemy[0], spawnPoint.position, spawnPoint.rotation);
                ennemyAlive++;
            }
            
        }
	}
}

