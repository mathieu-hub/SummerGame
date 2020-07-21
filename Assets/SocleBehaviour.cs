using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

namespace Tower
{
    /// <summary>
    /// This Script makes appeat the Ui for puting Turret on the battlefield
    /// </summary>
    public class SocleBehaviour : MonoBehaviour
    {
        #region


        [Header("UI")]
        [SerializeField] private TextMeshProUGUI turretName;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI range;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI fireRate;
        [SerializeField] private Image greenCircle;
        [SerializeField] private GameObject UI;

        [Header("Validation")]
        [SerializeField] private float validationTime;
        [SerializeField] private bool canValidate = false;
        private bool needToChange = false;

        [Header("Variables Index")]
        private float horizontal;
        public int modifierIndex;
        private int currentIndex = 0;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            UI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");

            UpdateUI();
            UpdateIndex();
            Validation();
            Buying();

        }

        void UpdateUI()
        {
            greenCircle.fillAmount = validationTime / 100;

            if (gameObject.GetComponent<FadeInButton>().playerHe)
            {
                UI.SetActive(true);

                if (Input.GetButtonDown("A_Button"))
                {
                    if (currentIndex == 0 && GameManager.Instance.strootUnlock)
                    {
                        canValidate = true;
                    }
                    else if (currentIndex == 0 && GameManager.Instance.strootUnlock == false)
                    {
                        canValidate = false;
                        Debug.Log("Can't buy");
                    }


                    if (currentIndex == 1 && GameManager.Instance.bourloUnlock)
                    {
                        canValidate = true;
                    }
                    else if (currentIndex == 1 && GameManager.Instance.bourloUnlock == false)
                    {
                        canValidate = false;
                        Debug.Log("Can't buy");
                    }


                    if (currentIndex == 2 && GameManager.Instance.snipicUnlock)
                    {
                        canValidate = true;
                    }
                    else if (currentIndex == 2 && GameManager.Instance.snipicUnlock == false)
                    {
                        Debug.Log("Can't buy");
                    }


                    if (currentIndex == 3 && GameManager.Instance.tronçoronceUnlock)
                    {
                        canValidate = true;
                    }
                    else if (currentIndex == 3 && GameManager.Instance.tronçoronceUnlock == false)
                    {
                        Debug.Log("Can't buy");
                    }


                    if (currentIndex == 4 && GameManager.Instance.invasiveUnlock)
                    {
                        canValidate = true;
                    }
                    else if (currentIndex == 4 && GameManager.Instance.invasiveUnlock == false)
                    {
                        Debug.Log("Can't buy");
                    }
                }

                if (Input.GetButtonUp("A_Button"))
                {
                    canValidate = false;
                    validationTime = 0;
                }

            }
            else
            {
                UI.SetActive(false);
                canValidate = false;
            }

            turretName.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().turretName;
            cost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().vegetablesCost.ToString() + " Cout";
            fireRate.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().fireRate.ToString() + " Cadence";
            damage.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().damage.ToString() + " Dégats";
            range.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range.ToString() + " Range";
        }

        void Buying()
        {
            if(validationTime == 100 && gameObject.GetComponent<FadeInButton>().playerHe && GameManager.Instance.purinCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().vegetablesCost && GameManager.Instance.scrapsCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost)
            {
                if(currentIndex == 0 && GameManager.Instance.strootUnlock)
                {
                    InstantiateTurret();
                }
                else if(currentIndex == 0 && GameManager.Instance.strootUnlock == false)
                {
                    canValidate = false;
                    Debug.Log("Can't buy");
                }


                if (currentIndex == 1 && GameManager.Instance.bourloUnlock)
                {
                    InstantiateTurret();
                }
                else if (currentIndex == 1 && GameManager.Instance.bourloUnlock == false)
                {
                    canValidate = false;
                    Debug.Log("Can't buy");
                }


                if (currentIndex == 2 && GameManager.Instance.snipicUnlock)
                {
                    InstantiateTurret();
                }
                else if (currentIndex == 2 && GameManager.Instance.snipicUnlock == false)
                {
                    Debug.Log("Can't buy");
                }


                if (currentIndex == 3 && GameManager.Instance.tronçoronceUnlock)
                {
                    InstantiateTurret();
                }
                else if (currentIndex == 3 && GameManager.Instance.tronçoronceUnlock == false)
                {
                    Debug.Log("Can't buy");
                }


                if (currentIndex == 4 && GameManager.Instance.invasiveUnlock)
                {
                    InstantiateTurret();
                }
                else if (currentIndex == 4 && GameManager.Instance.invasiveUnlock == false)
                {
                    Debug.Log("Can't buy");
                }

            }
        }
        void InstantiateTurret()
        {
            Instantiate(GameManager.Instance.SocleManager.Turret[currentIndex], transform.position, Quaternion.identity);
            GameManager.Instance.purinCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().vegetablesCost;
            GameManager.Instance.scrapsCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }
        

        void Validation()
        {
            if (canValidate)
            {
                validationTime += 0.2f;

                if(validationTime >= 100)
                {
                    validationTime = 100;
                }
            }
            
        }

        void UpdateIndex()
        {
            if (gameObject.GetComponent<FadeInButton>().playerHe && !canValidate)
            {
                if (horizontal > 0.15)
                {
                    modifierIndex = 1;
                    needToChange = false;
                }
                if (horizontal < -0.15)
                {
                    needToChange = false;
                    modifierIndex = -1;
                }
                 
                if(horizontal < 0.15 && horizontal > -0.15)
                {
                    needToChange = true;
                }
                
                if (needToChange) 
                {
                    needToChange = false;
                    currentIndex += modifierIndex;

                    if( currentIndex < 0)
                    {
                        currentIndex = 0;
                    }
                    if(currentIndex > 4)
                    {
                        currentIndex = 4;
                    }
                    modifierIndex = 0;
                }
            }

        }
        private void OnDrawGizmos()
        {
            if (gameObject.GetComponent<FadeInButton>().playerHe)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range);
            }
        }

    }
}

