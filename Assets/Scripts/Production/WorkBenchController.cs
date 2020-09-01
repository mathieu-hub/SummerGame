using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Management;
using AudioManager;

namespace Turret
{
    public class WorkBenchController : MonoBehaviour
    {
        #region Variables

        private AudioSource audioSource;

        [Header("PlayerHere")]
        [SerializeField] private bool playerHere = false;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI turretName;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI researchCost;
        [SerializeField] private TextMeshProUGUI range;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI fireRate;
        [SerializeField] private GameObject Ui;
        [SerializeField] private TextMeshProUGUI crossX;



        [Header("Position/TurretImage")]
        [SerializeField] private int currentIndex = 0;
        [SerializeField] private int modifierIndex = 0;
        [SerializeField] private Image[] positions;
        [SerializeField] private Image validationCircle;
        [SerializeField] private bool needToChange = false;
        [SerializeField] private float validationTime;
        [SerializeField] private bool canValidate = false;

        [SerializeField] private float horizontal;



        #endregion

        // Start is called before the first frame update
        void Start()
        {
            crossX.enabled = false;

            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateUi();
            UpdateIndex();
            UnlockUI();
            Validation();


            if (validationTime == 100)
            {
                Buying();
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

            if (Input.GetButtonDown("A_Button") && playerHere && currentIndex !=0)
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

        void Buying()
        {

          

            if (GameManager.Instance.bourloUnlock == false && validationTime == 100 && currentIndex == 1 && GameManager.Instance.plansCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {
              
                    GameManager.Instance.plansCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost;
                    GameManager.Instance.bourloUnlock = true;
                    canValidate = false;
                    validationTime = 0f;

                audioSource.clip = SingletonAudioSource.Instance.soundmanager.sounds[8].clip;
                audioSource.Play();
                return;

            }
            else
            {
               
                Debug.Log("Can't Buy Buddy"); 
            }


            if (GameManager.Instance.snipicUnlock == false && validationTime == 100 && currentIndex == 2 && GameManager.Instance.plansCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {

                GameManager.Instance.plansCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost;
                GameManager.Instance.snipicUnlock = true;
                canValidate = false;
                validationTime = 0f;
                audioSource.clip = SingletonAudioSource.Instance.soundmanager.sounds[8].clip;
                audioSource.Play();
                return;

            }
            else
            {
               
                Debug.Log("Can't Buy Buddy");
            }


            if (GameManager.Instance.tronçoronceUnlock == false && validationTime == 100 && currentIndex == 3 && GameManager.Instance.plansCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {

                GameManager.Instance.plansCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost;
                GameManager.Instance.tronçoronceUnlock = true;
                canValidate = false;
                validationTime = 0f;
                audioSource.clip = SingletonAudioSource.Instance.soundmanager.sounds[8].clip;
                audioSource.Play();
                return;

            }
            else
            {
                Debug.Log("Can't Buy Buddy");
                
            }


            if (GameManager.Instance.invasiveUnlock == false && validationTime == 100 && currentIndex == 4 && GameManager.Instance.plansCount >= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {

                GameManager.Instance.plansCount -= GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost;
                GameManager.Instance.invasiveUnlock = true;
                canValidate = false;
                validationTime = 0f;
                audioSource.clip = SingletonAudioSource.Instance.soundmanager.sounds[8].clip;
                audioSource.Play();
                return;

            }
            else
            {
                Debug.Log("Can't Buy Buddy");
               
            }
        }

        void BlockerValidation()
        {
            if (currentIndex == 1 && GameManager.Instance.bourloUnlock || GameManager.Instance.plansCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {
                canValidate = false;
                StartCoroutine("Error");

                return;
            }
            else
            {
                canValidate = true;
            }

            if (currentIndex == 2 && GameManager.Instance.snipicUnlock|| GameManager.Instance.plansCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {
                canValidate = false;
                StartCoroutine("Error");
            }
            else
            {
                canValidate = true;
            }

            if (currentIndex == 3 && GameManager.Instance.tronçoronceUnlock|| GameManager.Instance.plansCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {
                canValidate = false;
                StartCoroutine("Error");
            }
            else
            {
                canValidate = true;
            }

            if (currentIndex == 4 && GameManager.Instance.invasiveUnlock || GameManager.Instance.plansCount < GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost)
            {
                canValidate = false;
                StartCoroutine("Error");
            }
            else
            {
                canValidate = true;
            }
        }

        void UpdateUi()
        {
            turretName.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().turretName;
            cost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost.ToString() + " Cout";
            fireRate.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().fireRate.ToString() + " Cadence";
            damage.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().damage.ToString() + " Dégats";
            range.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().range.ToString() + " Range";
            researchCost.text = GameManager.Instance.SocleManager.Turret[currentIndex].GetComponent<TurretParent>().planCost.ToString() + " RCost";

            if (playerHere)
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
                positions[0].color = Color.white;
            }
            else
            {
                positions[0].color = Color.black;
            }

            if (GameManager.Instance.bourloUnlock)
            {
                positions[1].color = Color.white;
            }
            else
            {
                positions[1].color = Color.black;
            }

            if (GameManager.Instance.snipicUnlock)
            {
                positions[2].color = Color.white;
            }
            else
            {
                positions[2].color = Color.black;
            }

            if (GameManager.Instance.tronçoronceUnlock)
            {
                positions[3].color = Color.white;
            }
            else
            {
                positions[3].color = Color.black;
            }

            if (GameManager.Instance.invasiveUnlock)
            {
                positions[4].color = Color.white;
            }
            else
            {
                positions[4].color = Color.black;
            }
        }
        #endregion


        #region trigger

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = false;
            }
        }

        #endregion

        IEnumerator Error()
        {
            Debug.Log("Called");
            crossX.enabled = true;
            yield return new WaitForSeconds(0.5f);
            crossX.enabled = false;
            audioSource.clip = SingletonAudioSource.Instance.soundmanager.sounds[18].clip;
            audioSource.Play();
        }
    }
}

