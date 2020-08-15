using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    #region Variables
    [Header("Credits")]
    [SerializeField] private GameObject creditsPack;
    [SerializeField] private GameObject creditsReturnButton;

    [Header("OptionsReference")]
    public GameObject returnButton;
    public GameObject optionsPack;

    [Header("Main Menu")]
    [SerializeField] private GameObject AllInteractionsMain;
    [SerializeField] private GameObject playButton;

    [Header("Bools")]
    private bool mainMenu = true;
    private bool options = false;
    private bool credits = false;
    private bool commandes = false;
    #endregion

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);

        ReturnB();
    }

    public void PlayGame()
    {
        //Jouer Son Validation
        SceneManager.LoadScene(1);
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
        //Jouer Son Validation
        AllInteractionsMain.SetActive(false);
        gameObject.GetComponent<EventSystem>().SetSelectedGameObject(returnButton);
    }

    public void Credits()
    {
        credits = true;
        mainMenu = false;

        //Jouer Son Validation
        creditsPack.SetActive(true);
        AllInteractionsMain.SetActive(false);
        gameObject.GetComponent<EventSystem>().SetSelectedGameObject(creditsReturnButton);
    }

    //Working
    public void RetourOptions()
    {
        mainMenu = true;
        options = false;

        //Jouer Son Retour
        EventSystem.current.SetSelectedGameObject(playButton);
        AllInteractionsMain.SetActive(true);
        optionsPack.SetActive(false);
    }

    public void RetourCommandes()
    {
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
}
