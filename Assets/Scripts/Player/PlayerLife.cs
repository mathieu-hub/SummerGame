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
        [Range(0.001f, 0.1f)]
        public float amountOfHeal;
        public float currentHealthPoint;
        private bool invicible = false;
        public bool needToHeal = false;

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

            if (needToHeal)
            {
                Heal = amountOfHeal;
            }

            if (currentHealthPoint <= 0 && PlayerManager.Instance.controller.playerDead == false)
            {
                Death();
            }

            if (currentHealthPoint == maxHealthPoint && PlayerManager.Instance.controller.playerDead == true)
            {
                needToHeal = false;
                PlayerManager.Instance.controller.playerDead = false;
                //cooldown = new Clock(waitingTime);
                //cooldown.Pause();
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
        public float Heal
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
            StartCoroutine("DeathEnum");
            
        }

        void UpdateUi()
        {
            healthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;
        }
        #endregion 

        IEnumerator DeathEnum()
        {
            needToHeal = true;
            //cooldown.Play();
            PlayerManager.Instance.controller.playerDead = true;
            GameCanvasManager.Instance.blackScreen.startFadingIN();
            yield return new WaitForSeconds(2f);
            PlayerManager.Instance.transform.position = GameManager.Instance.respawnPoint.transform.position;
        }
    }

    
}

