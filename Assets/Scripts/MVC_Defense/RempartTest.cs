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
        [Header("PlayerRef")]
        private bool playerHere = false;
        private bool APressed = false;

        private GameObject AButton;

        [Header("Life")]
        public int maxHealth;
        public int currentHealth;

        [Header("Values")]
        [SerializeField] private int currentLevel;
        [SerializeField] private int healCost;
        [SerializeField] private int purinUsedIn;
        [SerializeField] private int scrapUsedIn;
        [SerializeField] private float purinUsedInTotal;
        [SerializeField] private float scrapUsedInTotal;
        [SerializeField] private float validationUpgrade;


        [Header("UI")]
        [SerializeField] private GameObject Canvas;
        [SerializeField] private GameObject[] HpsInUI;
        [SerializeField] private GameObject HPPARENT;
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
            currentHealth = maxHealth;
            currentLevel = healCost;



            HpsInUI = new GameObject[HPPARENT.transform.childCount];
            for (int i = 0; i < HpsInUI.Length; i++)
            {
                HpsInUI[i] = transform.GetChild(i).gameObject;
            }
        }

        private void Update()
        {
            distance = Vector3.Distance(PlayerManager.Instance.transform.localPosition, transform.position);

            if (currentHealth <= 0)
            {
                //changer la bool dans crossbrain
                Destroy(gameObject);
            }

            if (playerHere && Input.GetButtonDown("A_Button"))
            {
                APressed = true;
                needToVanish = true;

            }

            if (deleteTime == 100)
            {
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

            updateHealth();
            Actions();
            Sell();
            Distance();
            UpdateUI();
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
            scrapUsedInTotal = Mathf.Floor(scrapUsedIn / 100 * 40);
            purinUsedInTotal = Mathf.Floor(purinUsedIn / 100 * 40);

            pCostTextSell.text = purinUsedInTotal.ToString();
            sCostTextSell.text = scrapUsedInTotal.ToString();

            validationCircle.fillAmount = validationTime / 100;
            deleteCircle.fillAmount = deleteTime / 100;

           

           



            //Update Visuel Values
            if (currentLevel <= 2)
            {

                diffHealth = upgradeHealth[currentLevel];
                //Update Visuel Visuels
                updateHp.text = "=>" + diffHealth.ToString();


                pCostTextUpgrade.text = healCost.ToString();
            }


            if (APressed)
            {
                Canvas.SetActive(true);
                AButton.SetActive(false);
            }
            else
            {
                Canvas.SetActive(false);
                AButton.SetActive(false);
            }

        }
        void Sell()
        {
            canDelete = false;
            deleteTime = 0;
            GameManager.Instance.purinCount += (int)purinUsedInTotal;
            GameManager.Instance.scrapsCount += (int)scrapUsedInTotal;
            //Changer les bools du CrossBrain;
            Destroy(gameObject);
        }

        void updateHealth()
        {
            if (currentHealth < maxHealth)
            {
                needToHeal = true;
                validationAction.text = "Heal";

            }

            if (currentHealth == maxHealth && currentLevel < 3)
            {
                needToHeal = false;
                validationAction.text = "Upgrade";

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

            for (int i = 0; i < maxHealth; i++)
            {
                HpsInUI[i].SetActive(true);
            }

            //Mettre en rouge les Hp manquants
            if (currentHealth < maxHealth)
            {
                for (int i = currentHealth; i < maxHealth; i++)
                {
                    HpsInUI[i].GetComponent<SpriteRenderer>().color = Color.red;
                }


            }

            for (int i = 0; i < currentHealth; i++)
            {
                HpsInUI[i].GetComponent<SpriteRenderer>().color = startColor;
            }


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //Dead
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
                maxHealth = upgradeHealth[currentLevel];
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

