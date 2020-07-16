using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        #endregion

        private void Start()
        {
            currentHealthPoint = maxHealthPoint;
        }

        private void Update()
        {
            UpdateUi();
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

                if (currentHealthPoint <= 0)
                {
                    Death();
                }
            }
        }

        void Death()
        {
            
            AppHelper.Quit();
        }

        void UpdateUi()
        {
            healthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;
        }
        #endregion 
    }
}

