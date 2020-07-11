using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

namespace Production
{
    /// <summary>
    /// This Script makes the Convertion between Vegetables into Slurry(Purin).
    /// This script need to be modular to feet to differents Animals.
    /// </summary>
    public class AnimalBehaviour : MonoBehaviour
    {
        #region Variables

        

        [Header("Values")]
        [Range(1, 10)]
        [SerializeField] private int inputValue;
        [Range(1, 10)]
        [SerializeField] private int OutputValue;

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

        #endregion

        void Start()
        {
            timer = new Clock(duration);
            timer.Pause();
            inProduction = false;
            green = durationBarBackground.color;
        }

         void Update()
         {
            UpdateUI();

            if (timer.finished)
            {
                durationBarBackground.color = Color.red;
                GetProduction();
            }

            if (Input.GetKeyDown(KeyCode.X)  && inProduction == false && GameManager.Instance.vegetablesCount >= inputValue)
            {
                inProduction = true;
                timer.Play();
                GameManager.Instance.vegetablesCount -= inputValue;
            }
         }

        void GetProduction()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                inProduction = false;
                GameManager.Instance.purinCount += OutputValue;
                durationBarBackground.color = green;
                timer = new Clock(duration);
                timer.Pause();
                
            }
        }

        void UpdateUI()
        {
            timerCountdown.text = timer.time.ToString("0");
            durationBar.fillAmount = (float)timer.time / (float)duration;
        }
    }
}
