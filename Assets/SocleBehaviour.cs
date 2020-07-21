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
        [Header("Variables")]
        [SerializeField] private GameObject UI;
        [SerializeField] private int currentIndex = 0;

        [SerializeField] private bool hadChange = false;
        [SerializeField] private bool needToChange = false;
        [SerializeField] private bool changing = false;
        [SerializeField] private bool canChange = true;
        [SerializeField] private TextMeshProUGUI turretName;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI range;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI fireRate;

        private float horizontal;
        public int modifierIndex;

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
            Buying();


        }

        void UpdateUI()
        {
            if (gameObject.GetComponent<FadeInButton>().playerHe)
            {
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
            }

            turretName.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().turretName;
            cost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().vegetablesCost.ToString() + " Cout";
            fireRate.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().fireRate.ToString() + " Cadence";
            damage.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().damage.ToString() + " Dégats";
            range.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range.ToString() + " Range";
        }

        void Buying()
        {
            if(Input.GetButtonDown("A_Button") && gameObject.GetComponent<FadeInButton>().playerHe && GameManager.Instance.purinCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().vegetablesCost && GameManager.Instance.scrapsCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost)
            {
                if(currentIndex == 0 && GameManager.Instance.strootUnlock)
                {
                    InstantiateTurret();
                }
                else if(currentIndex == 0 && GameManager.Instance.strootUnlock == false)
                {
                    Debug.Log("Can't buy");
                }


                if (currentIndex == 1 && GameManager.Instance.bourloUnlock)
                {
                    InstantiateTurret();
                }
                else if (currentIndex == 1 && GameManager.Instance.bourloUnlock == false)
                {
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
        private void OnDrawGizmos()
        {
            if (gameObject.GetComponent<FadeInButton>().playerHe)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range);
            }
        }

        void UpdateIndex()
        {
            if (gameObject.GetComponent<FadeInButton>().playerHe )
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
        
    }
}

