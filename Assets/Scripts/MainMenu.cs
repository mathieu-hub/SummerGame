using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    #region Variables
    [Header("Credits")]
    [SerializeField] private GameObject creditsPack;
  

    [Header("OptionsReference")]
    public GameObject returnButton;
    public GameObject allOptionsInteractions;
    public Image inputsScreen;
    public TextMeshProUGUI inputsText;
    public Image controls;
    public GameObject inputsButton;
    public Slider fullscreenSlider;

    [Header("Main Menu")]
    [SerializeField] private GameObject AllInteractionsMain;
    [SerializeField] private GameObject playButton;

    [Header("Bools")]
    private bool mainMenu = true;
    private bool options = false;
    private bool credits = false;
    private bool commandes = false;

    [Header("Positions")]
    public RectTransform[] positions;
    public Image purin;
    #endregion

    private void Start()
    {
        purin.rectTransform.position = positions[0].position;
        inputsScreen.enabled = false;

        Debug.Log("MainMenu");
    }


    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);

        ReturnB();
        IconePositions();

        
    }

    public void PlayGame()
    {
        //Jouer Son Validation
        SceneManager.LoadScene(1);
        Debug.Log("Play");
    }

    public void QuitGame()
    {
        //Jouer Son Validation
        AppHelper.Quit();
    }

    public void Options()
    {
        options = true;
        mainMenu = false;
        inputsScreen.enabled = false;
        inputsText.enabled = false;
        controls.enabled = false; 
        //Jouer Son Validation
        AllInteractionsMain.SetActive(false);
        allOptionsInteractions.SetActive(true);
        gameObject.GetComponent<EventSystem>().SetSelectedGameObject(inputsButton);
    }

    public void Credits()
    {
        credits = true;
        mainMenu = false;

        //Jouer Son Validation
        creditsPack.SetActive(true);
        AllInteractionsMain.SetActive(false);
        
    }
    #region Options
    public void FullScreen(bool isFullScreen)
    {
        if(fullscreenSlider.value == 1)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        
    }

    public void SetVolume(float volume)
    {

        //audioMixer.SetFloat("volume", volume);
    }

    public void ShortCut()
    {
        Debug.Log("Shortcut");
        options = false;
        commandes = true;

        //Bruit Validation
        inputsScreen.enabled = true;
        inputsText.enabled = true;
        controls.enabled = true;

    }

    #endregion

    //Working
    public void RetourOptions()
    {
        mainMenu = true;
        options = false;

        //Jouer Son Retour
        EventSystem.current.SetSelectedGameObject(playButton);
        AllInteractionsMain.SetActive(true);
        allOptionsInteractions.SetActive(false);
    }

    public void RetourCommandes()
    {
        inputsScreen.enabled = false;
        inputsText.enabled = false;
        controls.enabled = false;
        options = true;
        commandes = false;
        //EventSystem.current.SetSelectedGameObject(backOptionsButton);
        //AllInteractionsOptions.SetActive(true);
        //optionsPack.SetActive(true);
    }

    //Working
    public void RetourCredits()
    {
        mainMenu = true;
        credits = false;

        //Jouer Son Retour
        EventSystem.current.SetSelectedGameObject(playButton);
        AllInteractionsMain.SetActive(true);
        creditsPack.SetActive(false);
    }

    public void ReturnB()
    {
        if (Input.GetButtonDown("B_Button"))
        {
            if (credits)
            {
                RetourCredits();
            }
            else if (options)
            {
                RetourOptions();
            }
            else if (commandes)
            {
                RetourCommandes();
            }
        }
                
    }

    public void IconePositions()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Commandes")
        {
            purin.rectTransform.position = positions[0].position;
            purin.GetComponent<Image>().enabled = false;

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Volume")
        {
            purin.GetComponent<Image>().enabled = true;
            purin.rectTransform.position = positions[1].position;
        }

        else if (EventSystem.current.currentSelectedGameObject.name == "FullScreen")
        {
            purin.GetComponent<Image>().enabled = true;
            purin.rectTransform.position = positions[2].position;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Resolutions")
        {
            purin.rectTransform.position = positions[3].position;
            purin.GetComponent<Image>().enabled = true;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "SBACK")
        {
            purin.GetComponent<Image>().enabled = false;
            purin.rectTransform.position = positions[4].position;
        }
    }
}
