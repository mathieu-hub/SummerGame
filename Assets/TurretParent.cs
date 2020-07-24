using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;

namespace Tower
{
    /// <summary>
    /// Need this script on each towers for socle behaviour
    /// </summary>
    
        
    public class TurretParent : MonoBehaviour
    {
        #region
        [Header("Values")]
        public string turretName;
        public int purinsCost;
        public int planCost;
        public int scrapCost;
        public int range;
        public int damage;
        public int fireRate;

        [Header("Investissement")]
        public int scrapUsedIn;
        public int purinUsedIn;

        [Header("UpgradesValues")]
        public int pCostUp1;
        public int sCostUp1;
        public int pCostUp2;
        public int sCostUp2;

        [Header("HealthPoint")]
        public int currentHp;
        [Range(1,100)]
        public int maxHp;


        [Header("UI")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private TextMeshProUGUI UIturretName;
        [SerializeField] private TextMeshProUGUI UILife;
        [SerializeField] private TextMeshProUGUI UIRange;
        [SerializeField] private TextMeshProUGUI UIDamage;
        [SerializeField] private TextMeshProUGUI UIFireRate;
        [SerializeField] private GameObject AButton;

        public float distance = 0f;


        [Header("Bools")]
        public bool playerHere = false;
        public bool APressed = false;
        public bool needToVanish = false;
        #endregion

        private void Start()
        {
            //give Variables
            currentHp = maxHp;
            purinUsedIn = purinsCost;
            scrapUsedIn = scrapCost;
            turretName = gameObject.name;
            AButton.SetActive(false);
            canvas.SetActive(false);
        }

        private void Update()
        {
            if(playerHere && Input.GetButtonDown("A_Button"))
            {
                APressed = true;
                needToVanish = true;
                
            }

            UpdateUI();
            Distance();

            distance = Vector3.Distance(PlayerManager.Instance.transform.localPosition, transform.position);
        }

        void UpdateUI()
        {
            UIturretName.text = name;
            UILife.text = currentHp.ToString() + " / " + maxHp.ToString();
            UIRange.text = range.ToString();
            UIDamage.text = damage.ToString();
            UIFireRate.text = fireRate.ToString();

            if (APressed)
            {
                canvas.SetActive(true);
                AButton.SetActive(false);
            }

            if (playerHere && !needToVanish && distance <=2)
            {
                AButton.SetActive(true);
            }

        }

        void Distance()
        {
            if(distance <= 2)
            {
                playerHere = true;
                
            }
            else
            {
                APressed = false;
                playerHere = false;
                needToVanish = false;
            }
        }

       
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, gameObject.GetComponent<CircleCollider2D>().radius);
        }
        
    }
}

