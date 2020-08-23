using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using Player;

namespace Ennemies
{
	public class RempartTest : MonoBehaviour
	{
        public GameObject refParent = null;

        [Header("PlayerRef")]
        public bool playerHere = false;
        private bool APressed = false;

        [SerializeField] private GameObject AButton;

        [Header("Life")]
        public int maxHealth = 20;
        public int currentHealth = 20;

        [Header("Values")]
        [SerializeField] private int currentLevel;
        [SerializeField] private int healCost;
        [SerializeField] private float purinUsedIn;
        [SerializeField] private float scrapUsedIn;
        [SerializeField] private float purinUsedInTotal;
        [SerializeField] private float scrapUsedInTotal;
        [SerializeField] private float validationUpgrade;


        [Header("UI")]
        [SerializeField] private GameObject Canvas;
        [SerializeField] private TextMeshProUGUI currentHP;
        [SerializeField] private TextMeshProUGUI maxHP;
        [SerializeField] private TextMeshProUGUI crossX;
        [SerializeField] private Image validationCircle;
        [SerializeField] private Image deleteCircle;
        [SerializeField] private float validationTime;
        [SerializeField] private float deleteTime;
        [SerializeField] private bool canValidate = false;
        [SerializeField] private bool canDelete = false;

        [Header("Update Visual")]
        [SerializeField] private TextMeshProUGUI validationAction;
        [SerializeField] private TextMeshProUGUI updateHp;
        [SerializeField] private TextMeshProUGUI pCostTextUpgrade;
        [SerializeField] private TextMeshProUGUI sCostTextUpgrade;
        [SerializeField] private TextMeshProUGUI pCostTextSell;
        [SerializeField] private TextMeshProUGUI sCostTextSell;
        [SerializeField] private Image SPC;
        [SerializeField] private Image SSC;
        [SerializeField] private Image UPC;
        [SerializeField] private Image USC;

        [Header("Upgrades")]
        [SerializeField] private int[] pCost;
        [SerializeField] private int[] sCost;
        [SerializeField] private int[] upgradeHealth;
        [SerializeField] private int diffHealth;

        [SerializeField] private bool needToHeal = false;
        public bool needToVanish = false;
        public Color startColor;
        public float distance = 0f;

        void Awake()
		{
            purinUsedIn = 20;
            currentHealth = maxHealth;
            currentLevel = healCost;

        }

        private void Update()
        {
            
            distance = Vector3.Distance(PlayerManager.Instance.transform.localPosition, transform.position);

           

            if (playerHere && Input.GetButtonDown("A_Button"))
            {
                APressed = true;
                needToVanish = true;

            }

            if (deleteTime == 100)
            {
                Debug.Log("Sell");
                Sell();
            }

            if (needToHeal)
            {
                validationUpgrade = 0.6f;
            }
            else
            {
                validationUpgrade = 0.4f;

            }

            scrapUsedInTotal = scrapUsedIn / 100 * 40;
            purinUsedInTotal = purinUsedIn / 100 * 40;

            pCostTextSell.text = purinUsedInTotal.ToString();
            sCostTextSell.text = scrapUsedInTotal.ToString();

            DetectInputDelete();
            updateHealth();
            Actions();
            Distance();
            UpdateUI();
            DetectInputValidation();
        }



        #region Methods

        void Distance()
        {
            
                if (distance <= 2)
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
                BlockerValidation();
            }

            if (Input.GetButtonUp("A_Button") && playerHere)
            {
                canValidate = false;
                validationTime = 0;

            }

            if (canValidate)
            {
                validationTime += validationUpgrade;

                if (validationTime >= 100)
                {
                    validationTime = 100;
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
            else if (!needToHeal && GameManager.Instance.purinCount >= pCost[currentLevel - 1] && GameManager.Instance.scrapsCount >= sCost[currentLevel - 1] && currentLevel < 3)
            {
                canValidate = true;
            }
            else
            {
                StartCoroutine("Error");
                canValidate = false;
            }

        }

        void UpdateUI()
        {
            //Afficher Heal Quand necessaire
            if (needToHeal)
            {
                sCostTextUpgrade.enabled = false;
                USC.enabled = false;
                pCostTextUpgrade.text = currentLevel.ToString();
            }
            //Afficher Upgrade Quand necessaire
            else if (!needToHeal && currentLevel < 3)
            {
                USC.enabled = true;
                sCostTextUpgrade.text = sCost[currentLevel - 1].ToString();
                UPC.enabled = true;
                pCostTextUpgrade.text = pCost[currentLevel - 1].ToString();


            }
            else if (!needToHeal && currentLevel == 3)
            {
                USC.enabled = false;
                UPC.enabled = false;

                sCostTextUpgrade.enabled = false;
            }

            //Update Sell recipe
           

           

            validationCircle.fillAmount = validationTime / 100;
            deleteCircle.fillAmount = deleteTime / 100;

           

            //Update Visuel Values
            if (currentLevel <= 2)
            {

                diffHealth = upgradeHealth[currentLevel-1];
                //Update Visuel Visuels
                updateHp.text = "=>" + diffHealth.ToString();


               
            }


            if (playerHere && !APressed)
            {
                AButton.SetActive(true);
            }
            else
            {
                AButton.SetActive(false);

            }

            if (APressed)
            {
                Canvas.SetActive(true);
                AButton.SetActive(false);
            }
            else
            {
                Canvas.SetActive(false);
            }

            currentHP.text = currentHealth.ToString() + "/ ";
            maxHP.text = maxHealth.ToString();
        }
        void Sell()
        {
            refParent.GetComponent<CrossBrain>().Dead();
            canDelete = false;
            deleteTime = 0;
            currentHealth = 0;
            GameManager.Instance.purinCount += (int)purinUsedInTotal;
            GameManager.Instance.scrapsCount += (int)scrapUsedInTotal;
            Debug.Log("Sell");
            Destroy(gameObject);
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

        void updateHealth()
        {
            if (currentHealth < maxHealth)
            {
                needToHeal = true;
                validationAction.text = "Heal";
                pCostTextUpgrade.text = healCost.ToString();

            }

            if (currentHealth == maxHealth && currentLevel < 3)
            {
                needToHeal = false;
                validationAction.text = "Upgrade";
                pCostTextUpgrade.text = pCost[currentLevel-1].ToString();
                if (currentLevel <= 2)
                {
                    //Afficher les améliorations
                    updateHp.enabled = true;
                }
                else
                {
                    updateHp.enabled = false;

                }


            }

            if (!needToHeal && currentLevel == 3)
            {
                validationAction.text = "";

                UPC.enabled = false;
                pCostTextUpgrade.enabled = false;
            }
            if (needToHeal)
            {
                validationAction.text = "Heal";

                UPC.enabled = true;
                pCostTextUpgrade.enabled = true;
            }


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                refParent.GetComponent<CrossBrain>().Dead();
                Destroy(gameObject);
            }

        }

        void Actions()
        {
            if (currentHealth < maxHealth)
            {
                needToHeal = true;
            }

            if (needToHeal && validationTime == 100)
            {
                GameManager.Instance.purinCount -= healCost;
                validationTime = 0;
                currentHealth += 1;
                canValidate = false;
            }
            else if (!needToHeal && validationTime == 100 && currentLevel < 3)
            {
                //Restart Validation
                validationTime = 0;
                canValidate = false;
                //Améliorations
                //enlever les ressources
                GameManager.Instance.purinCount -= pCost[currentLevel - 1];
                purinUsedIn += pCost[currentLevel - 1];
                GameManager.Instance.scrapsCount -= sCost[currentLevel - 1];
                scrapUsedIn += sCost[currentLevel - 1];

                //Augmenter les Stats (range/dégats/fireRate/Level)
                maxHealth = upgradeHealth[currentLevel-1];
                currentLevel += 1;
                currentHealth = maxHealth;
            }


        }

        #endregion

        #region Enumerator
        IEnumerator Error()
        {
            Debug.Log("Called");
            crossX.enabled = true;
            yield return new WaitForSeconds(0.5f);
            crossX.enabled = false;
        }
        #endregion 
    }
}

