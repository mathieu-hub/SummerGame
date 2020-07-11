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

        public static Clock timer;

        [Header("Clock")]
        [Range(0.0F, 15.0F)]
        public float duration;

        [Header("VisualClock")]
        [SerializeField]
        private Image durationBar;
        [SerializeField]
        private Image durationBarBackground;
        public TextMeshProUGUI storedCount;

        [Header("Storage")]
        public int storedVegetable;
        #endregion

        void Start()
        {
            //Start a clock when the Game Start.
            timer = new Clock(duration);
        }

        // Update is called once per frame
        void Update()
        {
            //Update Visual at all time
            UpdateUi();

            //Increase Count
            if (timer.finished)
            {
                storedVegetable += 1;
                timer = new Clock(duration);
            }
            //Stop Production at 6
            if (storedVegetable == 6)
            {
                timer.Pause();
            }
            else
            {
                print(timer.time);
            }

            if (Input.GetKeyDown(KeyCode.Space)){

                GameManager.Instance.vegetablesCount += storedVegetable;
            }
        }

        void UpdateUi()
        {

            storedCount.text = storedVegetable.ToString();
            durationBar.fillAmount = (float)timer.time / (float)duration;

            if (storedVegetable == 6)
            {
                //changer couleur de la bar

            }
        }
    }
}
