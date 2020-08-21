using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;
using Management;

namespace Turret
{
    /// <summary>
    /// Need this script on each towers for socle behaviour
    /// </summary>
    
        
    public class TurretParent : MonoBehaviour
    {
        #region

        [SerializeField] private TurretBehaviour turretBehaviour = null;
        
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
        [SerializeField] private Image purinSell;
        [SerializeField] private Image scrapSell;
        [SerializeField] private Image purinUpgrade;
        [SerializeField] private Image scrapUpgrade;

        [Header("UpgradesValues/HealCost")]
        //UseCurrentLevel -1 comme index
        public int[] pCost;
        public int[] sCost;
        public int currentLevel = 1;
        public int healCost = 1;
        public bool needToHeal = false;

        //Upgrade Values need to be add to Base Values. They are use to show the values increasing in the UI in previsualisation
        public int[] upgradeRange;
        public int[] upgradeDamages;
        public int[] upgradeFireRate;
        public int[] upgradeHpMax;


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

        [Header("UI/Upgrade/Visuels")]
        [SerializeField] private TextMeshProUGUI upgradeVisuelHp;
        [SerializeField] private TextMeshProUGUI upgradeVisuelRange;
        [SerializeField] private TextMeshProUGUI upgradeVisuelDamages;
        [SerializeField] private TextMeshProUGUI upgradeVisuelFireRate;

        private int upgradeDiffHp = 0;
        private int upgradeDiffRange = 0;
        private int upgradeDiffDamages = 0;
        private int upgradeDiffFireRate = 0;


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
        [SerializeField] private float validationUpgrade = 0.2f;
        [SerializeField] private TextMeshProUGUI crossX;


        public Color startColor;


        public float distance = 0f;


        [Header("Bools")]
        public bool playerHere = false;
        public bool APressed = false;
        public bool needToVanish = false;
        private bool canValidate = false;
        private bool canDelete = false;
        public bool broke = false;

        [SerializeField] private GameObject brokeParticule;
        #endregion

        private void Start()
        {
            brokeParticule.SetActive(false);

            //Gère Ui
            startColor = healPoints[0].GetComponent<SpriteRenderer>().color;
            crossX.enabled = false;

            //give Variables
            currentHp = maxHp;
            purinUsedIn = purinsCost;
            scrapUsedIn = scrapCost;
            gameObject.name = turretName;
            AButton.SetActive(false);
            canvas.SetActive(false);

            for (int i = 0; i == maxHp ; i++)
            {
                healPoints[i].SetActive(false);
            }
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

            if (needToHeal)
            {
                validationUpgrade = 0.6f;
            }
            else
            {
                validationUpgrade = 0.4f;

            }

            distance = Vector3.Distance(PlayerManager.Instance.transform.localPosition, transform.position);

            gameObject.GetComponent<CircleCollider2D>().radius = range;


            UpdateUI();
            Distance();
            DetectInputValidation();
            DetectInputDelete();
            Actions();

            if (currentLevel == 3 || needToHeal)
            {
                upgradeVisuelHp.enabled = false;
                upgradeVisuelRange.enabled = false;
                upgradeVisuelDamages.enabled = false;
                upgradeVisuelFireRate.enabled = false;
            }

            if (currentHp == maxHp)
            {
                needToHeal = false;
            }
        }

        void UpdateUI()
        {
            //Afficher Heal Quand necessaire
            if (needToHeal)
            {
                scrapUpgrade.enabled = false;
                USC.enabled = false;
                UPC.text = currentLevel.ToString();
            }
            //Afficher Upgrade Quand necessaire
            else if (!needToHeal && currentLevel < 3)
            {
                USC.enabled = true;
                USC.text = sCost[currentLevel - 1].ToString();
                UPC.enabled = true;
                UPC.text = pCost[currentLevel - 1].ToString();

               
            }
            else if(!needToHeal && currentLevel == 3)
            {
                USC.enabled = false;
                scrapUpgrade.enabled = false;
                
                purinUpgrade.enabled = false;
            }

            SPC.text = purinTotalAtSell.ToString();
            SSC.text = scrapTotalAtSell.ToString();

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

            

            //Update Visuel Values
            if (currentLevel <= 2)
            {

                upgradeDiffDamages = upgradeDamages[currentLevel - 1] - damage;
                upgradeDiffFireRate = upgradeFireRate[currentLevel - 1] - fireRate;
                upgradeDiffRange = upgradeRange[currentLevel - 1] - range;
                upgradeDiffHp = upgradeHpMax[currentLevel];
                //Update Visuel Visuels
                upgradeVisuelHp.text = "=>" + upgradeDiffHp.ToString();
                upgradeVisuelDamages.text = "+" + upgradeDiffDamages.ToString();
                upgradeVisuelFireRate.text = "+" + upgradeDiffFireRate.ToString();
                upgradeVisuelRange.text = "+" + upgradeDiffRange.ToString();

                UPC.text = healCost.ToString();
            }
            

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

            if (currentHp < maxHp)
            {
                needToHeal = true;
                ValidationAction.text = "Heal";

            }

            if (currentHp == maxHp && currentLevel < 3)
            {
                needToHeal = false;
                ValidationAction.text = "Upgrade";

                if(currentLevel <= 2)
                {
                    //Afficher les améliorations
                    upgradeVisuelHp.enabled = true;
                    upgradeVisuelRange.enabled = true;
                    upgradeVisuelDamages.enabled = true;
                    upgradeVisuelFireRate.enabled = true;

                    
                }
                else
                {
                    upgradeVisuelHp.enabled = false;
                    upgradeVisuelRange.enabled = false;
                    upgradeVisuelDamages.enabled = false;
                    upgradeVisuelFireRate.enabled = false;

                    
                }

               
            }

            if (!needToHeal && currentLevel == 3)
            {
                ValidationAction.text = "";

                UPC.enabled = false;
                purinUpgrade.enabled = false;
            }if (needToHeal)
            {
                ValidationAction.text = "Heal";

                UPC.enabled = true;
                purinUpgrade.enabled = true;
            }

            for (int i = 0; i < maxHp; i++)
            {
                healPoints[i].SetActive(true);
            }

            //Mettre en rouge les Hp manquants
            if(currentHp < maxHp)
            {
                for (int i = currentHp; i < maxHp; i++)
                {
                    healPoints[i].GetComponent<SpriteRenderer>().color = Color.red;
                }


            }

            for (int i = 0; i < currentHp; i++)
            {
                healPoints[i].GetComponent<SpriteRenderer>().color = startColor;
            }


            if (currentHp <= 0)
            {
                currentHp = 0;
                turretBehaviour.Break(true);
            }
            else
            {
                turretBehaviour.Break(false);
            }

            if (turretBehaviour.GetBrokeState())
            {
                brokeParticule.SetActive(true);
            }
            else
            {
                brokeParticule.SetActive(false);
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
            else if (!needToHeal && GameManager.Instance.purinCount >= pCost[currentLevel -1] && GameManager.Instance.scrapsCount >= sCost[currentLevel - 1] && currentLevel < 3)
            {
                canValidate = true;
            }
            else
            {
                StartCoroutine("Error");
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

        void Actions()
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
                canValidate = false;
            }
            else if(!needToHeal && validationTime == 100 && currentLevel < 3)
            {
                //Restart Validation
                validationTime = 0;
                canValidate = false;
                //Améliorations
                //enlever les ressources
                GameManager.Instance.purinCount -= pCost[currentLevel-1];
                purinUsedIn += pCost[currentLevel-1];
                GameManager.Instance.scrapsCount -= sCost[currentLevel-1];
                scrapUsedIn += sCost[currentLevel-1];

                //Augmenter les Stats (range/dégats/fireRate/Level)
                turretBehaviour.Upgrade(upgradeRange[currentLevel - 1], upgradeDamages[currentLevel - 1], upgradeFireRate[currentLevel - 1]);
                range = upgradeRange[currentLevel-1];
                damage = upgradeDamages[currentLevel-1];
                fireRate = upgradeFireRate[currentLevel-1];
                maxHp = upgradeHpMax[currentLevel];
                currentLevel += 1;
                currentHp = maxHp;
               
                
            }

           
        }


        private void OnDrawGizmos()
        {
            if (playerHere) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<CircleCollider2D>().radius * 100);
            }
            
        }

        IEnumerator Error()
        {
            Debug.Log("Called");
            crossX.enabled = true;
            yield return new WaitForSeconds(0.5f);
            crossX.enabled = false;
        }

    }
}

