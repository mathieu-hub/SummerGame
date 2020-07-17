using UnityEngine;
using UnityEngine.UI;
using Management;
using System.Collections;
using System.Collections.Generic;
using GameCanvas;

namespace Player
{
    /// <summary>
    /// XP - This script set the player's Health points.
    /// </summary>
    public class PlayerLife : MonoBehaviour
    {
        #region
        [Header("variables")]
        [Range(1,100)]
        public int maxHealthPoint;
        public int currentHealthPoint;
        private bool invicible = false;

        [Header("UI References")]
        [SerializeField] private Image healthBar;

        public Clock cooldown;
        [Range(1,10)]
        [SerializeField] private int waitingTime;
        #endregion

        private void Start()
        {
            cooldown = new Clock(waitingTime);
            cooldown.Pause();

            currentHealthPoint = maxHealthPoint;
        }

        private void Update()
        {
            UpdateUi();

            if (currentHealthPoint <= 0)
            {
                Death();
            }

            if (cooldown.onFinish)
            {
                PlayerManager.Instance.controller.needToStop = false;
                currentHealthPoint = maxHealthPoint;
                cooldown = new Clock(waitingTime);
                cooldown.Pause();
            }

            if (GameCanvasManager.Instance.blackScreen.fadeFinish)
            {
                GameCanvasManager.Instance.blackScreen.fadeFinish = false;
                GameCanvasManager.Instance.blackScreen.startFadingOUT();
            }
        }

        #region Methods
        public int TakeDamages
        {
            set
            {
                if (invicible)
                {
                    return;
                }

                //Value doit être défini lorsque l'on appelle cette fonction. On appelle cette méthods comme ceci: PlayerManager.Instance.Life.TakeDamages = damages;
                currentHealthPoint -= value;

            }
        }
        public int Heal
        {
            set
            {
                if (currentHealthPoint == maxHealthPoint)
                {
                    return;
                }

                currentHealthPoint += value;

                if(currentHealthPoint >= maxHealthPoint)
                {
                    currentHealthPoint = maxHealthPoint;
                }
            }
        }

        void Death()
        {
           PlayerManager.Instance.controller.needToStop = true;
           PlayerManager.Instance.transform.position = GameManager.Instance.respawnPoint.transform.position;
           cooldown.Play();
           GameCanvasManager.Instance.blackScreen.startFadingIN();
           
        }

        void UpdateUi()
        {
            healthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;
        }
        #endregion 
    }
}

