using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;
using Management;

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
        private float scrapUsedIn;
        private float purinUsedIn;
        private float scrapTotalAtSell;
        private float purinTotalAtSell;

        [Header("UpgradesValues/HealCost")]
        //UseCurrentLevel -1 comme index
        public int[] pCost;
        public int[] sCost;
        

        public int pCostUp1;
        public int sCostUp1;
        public int pCostUp2;
        public int sCostUp2;
        public int currentLevel = 1;
        public int healCost = 1;
        public bool needToHeal = false;

        //Upgrade Values need to be add to Base Values. They are use to show the values increasing in the UI in previsualisation
        public int upgradeRange1;
        public int upgradeDamage1;
        public int upgradeFireRate1;
        public int upgradeRange2;
        public int upgradeDamage2;
        public int upgradeFireRate2;


        [Header("HealthPoint")]
        public int currentHp;
        [Range(1,8)]
        public int maxHp;
        


        [Header("UI/Values")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private TextMeshProUGUI UIturretName;
        [SerializeField] private TextMeshProUGUI UIRange;
        [SerializeField] private TextMeshProUGUI UIDamage;
        [SerializeField] private TextMeshProUGUI UIFireRate;
        [SerializeField] private GameObject AButton;

        [Header("UI/Upgrade/Sell")]
        [SerializeField] private TextMeshProUGUI UPC;
        [SerializeField] private TextMeshProUGUI USC;
        [SerializeField] private TextMeshProUGUI SPC;
        [SerializeField] private TextMeshProUGUI SSC;
        [SerializeField] private TextMeshProUGUI ValidationAction;

        //Life
        public GameObject[] healPoints;

        [SerializeField] private Image validationCircle;
        [SerializeField] private float validationTime;
        [SerializeField] private Image deleteCircle;
        [SerializeField] private float deleteTime;

        private Color startColor;


        public float distance = 0f;


        [Header("Bools")]
        public bool playerHere = false;
        public bool APressed = false;
        public bool needToVanish = false;
        private bool canValidate = false;
        private bool canDelete = false;
        private bool broke = false;
        #endregion

        private void Start()
        {
            //Gère Ui
            startColor = healPoints[0].GetComponent<SpriteRenderer>().color;

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

            if(deleteTime == 100)
            {
                Sell();
            }

            UpdateUI();
            Distance();
            DetectInputValidation();
            DetectInputDelete();
            Heal();
          
            distance = Vector3.Distance(PlayerManager.Instance.transform.localPosition, transform.position);

            gameObject.GetComponent<CircleCollider2D>().radius = range;
        }

        void UpdateUI()
        {
            validationCircle.fillAmount = validationTime / 100;
            deleteCircle.fillAmount = deleteTime / 100;

            UIturretName.text = name;
           
            UIRange.text = range.ToString();
            UIDamage.text = damage.ToString();
            UIFireRate.text = fireRate.ToString();

            //Update Sell recipe
            scrapTotalAtSell = Mathf.Floor(scrapUsedIn / 100 * 40);
            purinTotalAtSell = Mathf.Floor(purinUsedIn / 100 * 40);

            SPC.text = purinTotalAtSell.ToString();
            SSC.text = scrapTotalAtSell.ToString();

            if (APressed)
            {
                canvas.SetActive(true);
                AButton.SetActive(false);
            }
            else
            {
                canvas.SetActive(false);
                AButton.SetActive(false);
            }

            //Life System
            #region LifeSystem
            if (currentHp == 1)
            {
                lifePoint1.color = green;
                lifePoint2.color = Color.red;
                lifePoint3.color = Color.red;
            }
            if(currentHp == 2)
            {
                lifePoint1.color = green;
                lifePoint2.color = green;
                lifePoint3.color = Color.red;
            }
            if(currentHp == 3)
            {
                lifePoint1.color = green;
                lifePoint2.color = green;
                lifePoint3.color = green;
            }

            for (int i = 0; i <= maxHp; i++)
            {
                healPoints[i].SetActive(true);
            }


            if (currentHp > 0)
            {
                for (int i = maxHp; i > healPoints.Length; i--)
                {
                    healPoints[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

            if(currentHp < 0)
            {
                currentHp = 0;
            }

            if(currentHp < maxHp)
            {
                ValidationAction.text = "Heal";
            }
            else
            {
                ValidationAction.text = "Upgrade";
            }
            #endregion
        }

        void Distance()
        {
            if(distance <= 2)
            {
                playerHere = true;

                if (!needToVanish)
                {
                    AButton.SetActive(true);
                }
                
            }
            else
            {
                APressed = false;
                playerHere = false;
                needToVanish = false;
            }
        }

        void DetectInputValidation()
        {
            if (Input.GetButtonDown("A_Button") && APressed && !canDelete)
            {
                //BlockerValidation();
                canValidate = true;
            }

            if (Input.GetButtonUp("A_Button") && playerHere)
            {
                canValidate = false;
                validationTime = 0;

            }

            if (canValidate)
            {
                validationTime += 0.2f;

                if (validationTime >= 100)
                {
                    validationTime = 100;
                }
            }
        }

        void DetectInputDelete()
        {
            if (Input.GetButtonDown("B_Button") && APressed && !canValidate)
            {
                canDelete = true;

            }

            if (Input.GetButtonUp("B_Button") && playerHere)
            {
                canDelete = false;
                deleteTime = 0;

            }

            if (canDelete)
            {
                deleteTime += 0.2f;

                if (deleteTime >= 100)
                {
                    deleteTime = 100;
                }
            }
        }

        void BlockerValidation()
        {
            //Check les Ressources pour le Heal
            if (needToHeal && GameManager.Instance.purinCount >= healCost)
            {
                canValidate = true;
            }
            //Check les Ressources pour les Améliorations
            else if (!needToHeal && GameManager.Instance.purinCount >= pCost[currentLevel -1] && GameManager.Instance.scrapsCount >= sCost[currentLevel - 1])
            {
                canValidate = true;
            }
            else
            {
                canValidate = false;
            }
            
           

           
        }


        void Sell()
        {
            canDelete = false;
            deleteTime = 0;
            GameManager.Instance.purinCount += (int)purinTotalAtSell;
            GameManager.Instance.scrapsCount += (int)scrapTotalAtSell;
            Instantiate(Resources.Load("Prefabs/Socle"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        void Heal()
        {
            healCost = currentLevel;

            if(currentHp < maxHp)
            {
                needToHeal = true;
            }

            if (needToHeal && validationTime ==100)
            {
                GameManager.Instance.purinCount -= healCost;
                validationTime = 0;
                currentHp += 1;
            }else if(!needToHeal && validationTime == 100)
            {

            }
        }

        private void OnDrawGizmos()
        {
            if (playerHere) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<CircleCollider2D>().radius * 100);
            }
            
        }
        
    }
}

