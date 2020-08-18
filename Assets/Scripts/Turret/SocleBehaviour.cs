using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Management;

namespace Tower
{
    public class SocleBehaviour : MonoBehaviour
    {
        #region Variables

        [Header("PlayerHere")]
        [SerializeField] private bool playerHere = false;

        [Header("ReferencesInCrossBrain")]
        [SerializeField] private bool leftTurret = false;
        [SerializeField] private bool rightTurret = false;
        public GameObject turretSummon;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI turretName;
        [SerializeField] private TextMeshProUGUI purinsCost;
        [SerializeField] private TextMeshProUGUI scrapsCost;
        [SerializeField] private TextMeshProUGUI range;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI fireRate;
        [SerializeField] private GameObject Ui;
        [SerializeField] private TextMeshProUGUI crossX;
        

        [SerializeField] private GameObject AButton;

        [Header("Position/TurretImage")]
        [SerializeField] private int currentIndex = 0;
        [SerializeField] private int modifierIndex = 0;
        [SerializeField] private Image[] positions;
        [SerializeField] private Image validationCircle;
        [SerializeField] private bool needToChange = false;
        [SerializeField] private float validationTime;
        [SerializeField] private bool canValidate = false;
        [SerializeField] private bool APressed = false;

        public bool needToIncrease = false;

        [SerializeField] private float horizontal;


        #endregion

        // Start is called before the first frame update
        void Start()
        {
            turretSummon = null;
            AButton.SetActive(false);
            crossX.enabled = false;
            Ui.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateUi();
            UpdateIndex();
            UnlockUI();
            Validation();

            if (playerHere && Input.GetButtonDown("A_Button"))
            {
                APressed = true;
                AButton.SetActive(false);
            }


            if (validationTime == 100)
            {
                InstantiateTurret();
            }

            

        }

        #region Methods
        void Validation()
        {
            validationCircle.fillAmount = validationTime / 100;

            if (Input.GetButtonUp("A_Button") && playerHere)
            {
                canValidate = false;
                validationTime = 0;

            }
            if (Input.GetButtonDown("A_Button") && playerHere)
            {

                BlockerValidation();

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

        void BlockerValidation()
        {
            if (currentIndex == 0 && (GameManager.Instance.strootUnlock == false || GameManager.Instance.purinCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost || GameManager.Instance.scrapsCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost))
            {

                Debug.Log("2");
                canValidate = false;
                validationTime = 0f;
                StartCoroutine("Error");
                return;
            }
      
            if (currentIndex == 1 && (GameManager.Instance.bourloUnlock == false || GameManager.Instance.purinCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost || GameManager.Instance.scrapsCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost))
            {
                
                canValidate = false;
                validationTime = 0f;
                StartCoroutine("Error");
                return;
            }
          



            if (currentIndex == 2 && (GameManager.Instance.snipicUnlock == false || GameManager.Instance.purinCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost || GameManager.Instance.scrapsCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost))
            {
                canValidate = false;
                validationTime = 0f;
                StartCoroutine("Error");
                return;
            }
            


            if (currentIndex == 3 && (GameManager.Instance.tronçoronceUnlock == false || GameManager.Instance.purinCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost || GameManager.Instance.scrapsCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost))
            {
                canValidate = false;
                validationTime = 0f;
                StartCoroutine("Error");
                return;
            }

            if (currentIndex == 4 && (GameManager.Instance.invasiveUnlock == false || GameManager.Instance.purinCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost || GameManager.Instance.scrapsCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost))
            {
                canValidate = false;
                validationTime = 0f;
                StartCoroutine("Error");
                return;
            }

            canValidate = true;
            
        }
        void InstantiateTurret()
        {
            turretSummon = Instantiate(GameManager.Instance.SocleManager.Turret[currentIndex], transform.position, Quaternion.identity);
            GameManager.Instance.purinCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost;
            GameManager.Instance.scrapsCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost;
            canValidate = false;
            validationTime = 0f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }
        void UpdateUi()
        {
            turretName.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().turretName;
            scrapsCost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().scrapCost.ToString();
            purinsCost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().purinsCost.ToString();
            fireRate.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().fireRate.ToString();
            damage.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().damage.ToString();
            range.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range.ToString();
            

            if (playerHere && APressed)
            {
                Ui.SetActive(true);
            }
            else
            {
                Ui.SetActive(false);
                currentIndex = 0;
            }

            validationCircle.transform.position = positions[currentIndex].transform.position;

        }

        void UpdateIndex()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");

            if (playerHere && !canValidate)
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

                if (horizontal < 0.15 && horizontal > -0.15)
                {
                    needToChange = true;
                }

                if (needToChange)
                {
                    needToChange = false;
                    currentIndex += modifierIndex;

                    if (currentIndex < 0)
                    {
                        currentIndex = 0;
                    }
                    if (currentIndex > 4)
                    {
                        currentIndex = 4;
                    }
                    modifierIndex = 0;
                }
            }

        }

        void UnlockUI()
        {
            if (GameManager.Instance.strootUnlock)
            {
                positions[0].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_selectionned");
            }
            else
            {
                positions[0].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_noUnlock");
            }

            if (GameManager.Instance.bourloUnlock)
            {
                positions[1].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_selectionned");
            }
            else
            {
                positions[1].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_noUnlock");
            }

            if (GameManager.Instance.snipicUnlock)
            {
                positions[2].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_selectionned");
            }
            else
            {
                positions[2].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_noUnlock");
            }

            if (GameManager.Instance.tronçoronceUnlock)
            {
                positions[3].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_selectionned");
            }
            else
            {
                positions[3].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_noUnlock");
            }

            if (GameManager.Instance.invasiveUnlock)
            {
                positions[4].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_selectionned");
            }
            else
            {
                positions[4].sprite = Resources.Load<Sprite>("Assets/M_poseTourelle_noUnlock");
            }
        }
        #endregion


        #region trigger

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = true;
                AButton.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = false;
                APressed = false;
            }
        }

        #endregion

        IEnumerator Error()
        {
            Debug.Log("Called");
            crossX.enabled = true;
            yield return new WaitForSeconds(0.5f);
            crossX.enabled = false;
        }
    }
}