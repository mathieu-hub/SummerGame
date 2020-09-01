using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using TMPro;
using UnityEngine.UI;
using AudioManager;

public class RempartBrain : MonoBehaviour
{
    #region Variables
    private AudioSource audioSource;

    //Présence du player;
    [SerializeField] private bool playerHere = false;
    [SerializeField] private bool APressed = false;
    [SerializeField] private GameObject AButton;
    

    [Header("Canvas")]
    [SerializeField] private GameObject canvasRempart;
    [SerializeField] private TextMeshProUGUI crossX;
    [SerializeField] private TextMeshProUGUI pCostUi;

    [Header("Validation")]
    [SerializeField] private bool canValidate;
    [SerializeField] public float validationTime = 0f;
    [SerializeField] private Image validationCircle;

    [Header("Cost")]
    [SerializeField] private int pCost;
    

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Initialisation();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHere && Input.GetButtonDown("A_Button") && gameObject.GetComponentInParent<CrossBrain>().rempart == false)
        {
           
            APressed = true;
        }

        if (Input.GetButtonDown("B_Button")&& APressed)
        {
            APressed = false;
            
        }

        if (APressed && playerHere)
        {
            canvasRempart.SetActive(true);
        }
        else
        {
            Initialisation();
        }

        if (validationTime == 100 && APressed)
        {
            playerHere = false;
            GameManager.Instance.purinCount -= pCost;
            Initialisation();
            SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 5);
            gameObject.GetComponentInParent<CrossBrain>().Summon();
            
        }

        DetectInputValidation();

        validationCircle.fillAmount = validationTime / 100f;

        if (playerHere && !APressed && gameObject.GetComponent<CrossBrain>().theRempart == null)
        {
            AButton.SetActive(true);
        }
        else
        {
            AButton.SetActive(false);
        }

        
    }

    #region Methods

    void Initialisation()
    {
        pCostUi.text = pCost.ToString();
        AButton.SetActive(false);
        canValidate = false;
        APressed = false;
        validationTime = 0f;
        canvasRempart.SetActive(false);
    }

    void DetectInputValidation()
    {
        if (Input.GetButtonDown("A_Button") && APressed)
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
            validationTime += 0.4f;

            if (validationTime >= 100)
            {
                validationTime = 100;
            }
        }
    }

    void BlockerValidation()
    {
        //Check les Ressources pour le Heal
        if(GameManager.Instance.purinCount >= pCost)
        {
            canValidate = true;
        }
        else
        {
            StartCoroutine("Error");
            canValidate = false;
        }

    }
    #endregion


    #region Trigger
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

    #region Enumerator
    IEnumerator Error()
    {
        Debug.Log("Called");
        crossX.enabled = true;
        yield return new WaitForSeconds(0.5f);
        crossX.enabled = false;
        SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 18);
    }
    #endregion
}
