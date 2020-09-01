﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using AudioManager;


namespace Production
{
    /// <summary>
    /// This Script makes the Convertion between Vegetables into Slurry(Purin).
    /// This script need to be modular to feet to differents Animals.
    /// </summary>
    public class AnimalBehaviour : MonoBehaviour
    {
        #region Variables

        public bool playerHere;
        private bool addedToTheList = false;

        [Header("Values")]
        [Range(1, 10)]
        [SerializeField] private int inputValue;
        [SerializeField] private TextMeshProUGUI inputText;
        [Range(1, 10)]
        [SerializeField] private int OutputValue;
        [SerializeField] private TextMeshProUGUI outputText;
        public int animalWeight;

        [Header("Clock")]
        [Range(1F, 60F)]
        public float duration;
        public Clock timer;
        [SerializeField] private bool inProduction;

        [Header("VisualClock")]
        [SerializeField] private Image durationBar;
        [SerializeField] private Image durationBarBackground;
        public TextMeshProUGUI timerCountdown;
        private Color green;

        private AudioSource audioSource;
        #endregion

        void Start()
        {
            //Lancer une clock et la mettre en pause pour éviter d'avoir des Null Reference
            timer = new Clock(duration);
            timer.Pause();

            inProduction = false;
            green = durationBarBackground.color;

            audioSource = GetComponent<AudioSource>();
        }

        #region AddToGMList
        private void OnEnable()
        {
            GameManager.Instance.activeAnimals.Add(gameObject);
        }
        private void OnDisable()
        {
            GameManager.Instance.totalAnimalWeight -= animalWeight;
            GameManager.Instance.activeAnimals.Remove(gameObject);
        }
        #endregion

        void Update()
         {
            UpdateUI();

            if (Input.GetButtonDown("A_Button") && playerHere)
            {
                if (timer.finished)
                {
                    Debug.Log("if");
                    GetProduction();
                }
                else if (inProduction == false && GameManager.Instance.vegetablesCount >= inputValue)
                {
                    Debug.Log("else");
                    inProduction = true;
                    timer.Play();
                    GameManager.Instance.vegetablesCount -= inputValue;
                }
            }

            
         }
        #region Methods
        void GetProduction()
        {
            durationBarBackground.color = Color.red;

            if (Input.GetButtonDown("A_Button") && playerHere)
            {
                inProduction = false;
                GameManager.Instance.purinCount += OutputValue;
                durationBarBackground.color = green;
                timer = new Clock(duration);
                timer.Pause();

                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 30);
                audioSource.Play();
            }
        }

        void UpdateUI()
        {
            timerCountdown.text = timer.time.ToString("0");
            durationBar.fillAmount = (float)timer.time / (float)duration;

            inputText.text = "Input Value : " + inputValue.ToString("0");
            outputText.text = "Output Value : " + OutputValue.ToString("0");
        }
        #endregion

        #region Triggers
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = false;
            }
        }
        

        #endregion
    }
}
