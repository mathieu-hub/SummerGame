using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Management;

namespace Production
{
    public class GardenBehaviour : MonoBehaviour
    {
        /// <summary>
        /// XP_This script makes the bahaviour of the Garden (Clock + Update Vegetable)
        /// </summary>

        #region Variables

        [HideInInspector] public bool playerHere = false;

        [Header("Clock")]
        [Range(0.0F, 15.0F)]
        public float duration;
        public Clock timer;

        [Header("VisualClock")]
        [SerializeField] private Image durationBar;
        [SerializeField] private Image durationBarBackground;
        public TextMeshProUGUI storedCount;
        public TextMeshProUGUI timerCountdown;
        private Color green;

        [Header("Storage")]
        public int storedVegetable;
        #endregion

        void Start()
        {
            green = durationBar.color;
            //Start a clock when the Game Start.
            timer = new Clock(duration);
        }

        // Update is called once per frame
        void Update()
        {
            //Update Visual at all time
            UpdateUi();

            //Increase Count
            if (timer.onFinish)
            {
                Production();
            }
            //Stop Production at 6
            if (storedVegetable == 6)
            {
                timer.Pause();
            }
            /*else   
            {
                print(timer.time);
            }*/

            if (Input.GetButtonDown("A_Button") && playerHere){
                GetProduction();
            }
        }
        #region OnTrigger
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


        #region Methods
        void UpdateUi()
        {
            timerCountdown.text = timer.time.ToString("0");
            storedCount.text = storedVegetable.ToString();
            durationBar.fillAmount = (float)timer.time / (float)duration;

            if (storedVegetable == 6)
            {
                durationBar.color = Color.red;
            }
        }

        void GetProduction()
        {
            if(storedVegetable == 6)
            {
                timer = new Clock(duration);
            }
            durationBar.color = green;
            GameManager.Instance.vegetablesCount += storedVegetable;
            storedVegetable = 0;
        }

        void Production()
        {
            storedVegetable += 1;
            timer = new Clock(duration);
        }
        #endregion
    }
}
