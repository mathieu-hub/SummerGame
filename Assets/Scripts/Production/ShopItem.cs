using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;
using Player;
using Seller;
using TMPro;

public class ShopItem : MonoBehaviour
{
    private float distance;
    public bool pulled = false;
    [SerializeField] private GameObject AButton;
    [SerializeField] private int vegetablesCost;

    [Header("BoolsNeedToBeCheck")]
    public bool isChicken = false;
    public bool isCow = false;
    public bool isPig = false;
    public bool isHorse = false;
    public bool isGardenUpgrade = false;
    public bool isBarnUpgrade = false;

    [Header("UI")]
    [SerializeField]  private GameObject Canvas;
    [SerializeField] private TextMeshProUGUI storedUnits;
    [SerializeField] private TextMeshProUGUI maxLimit;
    [SerializeField] private GameObject[] positions;
    [SerializeField] private Image validationCircle;
    [SerializeField] private Image deleteCircle;
    [SerializeField] private TextMeshProUGUI crossX;
    [SerializeField] private TextMeshProUGUI price;

    [Header("Validation")]
    //Pour les Fill amount
    [SerializeField] private float deleteTime = 0;
    [SerializeField] private float validationTime = 0;

    //Pour Ne pas accepter et undo en meme temps
    [SerializeField] private bool canValidate = true;
    [SerializeField] private bool canDelete = true;

    //Changer l'index pour changer d'Actions
    [SerializeField] private bool needToCharge = false;
    [SerializeField] private bool needToChange = false;
    [SerializeField] private int currentIndex;
    [SerializeField] private int modifierIndex;
    [SerializeField] private float vertical;

    [SerializeField] private bool bought = false;


    // Start is called before the first frame update
    void Start()
    {
        crossX.enabled = false;
        AButton.SetActive(false);

        Canvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        if(GameManager.Instance.totalAnimalWeight <= GameManager.Instance.maxStoredUnits && bought)
        {
            GameManager.Instance.tooMuchAnimals = false;
            BuyPossible();
            PlayerManager.Instance.controller.needToStop = false;
        }

        if (Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position) < 1 && pulled)
        {
            AButton.SetActive(true);

            if (Input.GetButtonDown("A_Button") && GameManager.Instance.tooMuchAnimals == false)
            {
                //Le joueur veut Buy l'objet
                Buy(); 
            }

        }
        else
        {
            AButton.SetActive(false);
        }

        //Rappelle l'objet si 
        if (GameManager.Instance.needToRefeshShop && pulled)
        {
            Recall();
        }

        if(GameManager.Instance.tooMuchAnimals == true)
        {
           
            
            //ActiverCanvas

            if(validationTime == 100)
            {
                FreeAnimal();
                validationTime = 0;
            }

            if(deleteTime == 100)
            {
                Undo();
                deleteTime = 0;
            }
        }

        
        Validation();
        Delete();
        UpdateIndex();

    }
    void Buy()
    {
        //Si il a les ressources on applique les effets.
        if(GameManager.Instance.vegetablesCount >= vegetablesCost)
        {
            bought = true;
            FirstEffects();
            //On test si il a la place pour acceuillir l'animal
            UpdateWeight();

        }
        else
        {
            print("error");
        }
                
    }

    void Test()
    {
        if(currentIndex == 0 && GameManager.Instance.chickensCount > 0)
        {
            canValidate = true;
        }
        else if (currentIndex == 1 && GameManager.Instance.cowsCount > 0)
        {
            canValidate = true;
        }
        else if (currentIndex == 2 && GameManager.Instance.horsesCount > 0)
        {
            canValidate = true;
        }
        else if (currentIndex == 3 && GameManager.Instance.pigsCount > 0)
        {
            canValidate = true;
        }
        else
        {
            canValidate = false;
            StartCoroutine("Error");
        }
                
    }

    void FreeAnimal()
    {
        validationTime = 0;
        canValidate = false;

        if (currentIndex == 0)
        {
            GameManager.Instance.chickensCount -= 1;
            UpdateWeight();
        }
        if (currentIndex == 1)
        {
            GameManager.Instance.cowsCount -= 1;
            UpdateWeight();
        }
        if (currentIndex == 3)
        {
            GameManager.Instance.pigsCount -= 1;
            UpdateWeight();
        }
        if (currentIndex == 2)
        {
            GameManager.Instance.horsesCount -= 1;
            UpdateWeight();

        }

        BuyPossible();
    }

    void Recall()
    {
        Debug.Log("Recall");
        pulled = false;
        SellerBehaviour.Instance.items.Add(gameObject);
        gameObject.transform.position = SellerBehaviour.Instance.stockPosition.position;
    }

    void FirstEffects()
    {
        if (isChicken)
        {
            GameManager.Instance.totalAnimalWeight += 1;
            
        }
        if (isCow)
        {
            GameManager.Instance.totalAnimalWeight += 2;
            
        }
        if (isPig)
        {
            GameManager.Instance.totalAnimalWeight += 4;
            
        }
        if (isHorse)
        {
            GameManager.Instance.totalAnimalWeight += 3;
            
        }
        if(isBarnUpgrade)
        {
            GameManager.Instance.numberOfGardens += 1;
        }
        if (isGardenUpgrade)
        {
            GameManager.Instance.maxStoredUnits += 3;

        }

    }

    void SecondEffects()
    {
        if (isChicken)
        {
            GameManager.Instance.chickensCount += 1;

        }
        if (isCow)
        {
            GameManager.Instance.cowsCount += 1;

        }
        if (isPig)
        {
            GameManager.Instance.pigsCount += 1;

        }
        if (isHorse)
        {
            GameManager.Instance.horsesCount += 1;

        }

    }
    public void UpdateWeight()
    {
        //l'achat n'est pas validé car le joueur n'a pas l'espace necessaire
        if (GameManager.Instance.totalAnimalWeight > GameManager.Instance.maxStoredUnits)
        {
            //Debug.Log("Start If");
            GameManager.Instance.tooMuchAnimals = true;
            PlayerManager.Instance.controller.needToStop = true;
            Canvas.SetActive(true);
        }
        else if (GameManager.Instance.tooMuchAnimals == false)
        {
            //Debug.Log("Start ELse");
            SecondEffects();
            GameManager.Instance.vegetablesCount -= vegetablesCost;
            Destroy(gameObject);
        }
    }

    public void BuyPossible()
    {
        if (GameManager.Instance.totalAnimalWeight <= GameManager.Instance.maxStoredUnits)
        {
            Debug.Log("Start ELse");
            SecondEffects();
            GameManager.Instance.vegetablesCount -= vegetablesCost;
            Destroy(gameObject);
        }
    }

    void Validation()
    {
        validationCircle.fillAmount = validationTime / 100;

        if (Input.GetButtonUp("A_Button"))
        {
            canValidate = true;
            canDelete = true;
            validationTime = 0;

        }
        if (Input.GetButtonDown("A_Button") && bought)
        {
            Test();
            canDelete = false;

        }

        if (canValidate && !canDelete)
        {
            validationTime += 0.2f;

            if (validationTime >= 100)
            {
                validationTime = 100;
            }
        }
                 
        
    }

    void Delete()
    {
        deleteCircle.fillAmount = deleteTime / 100;

        if (Input.GetButtonUp("B_Button"))
        {
            deleteTime = 0;
            canDelete = true;
            canValidate = true;
        }

        if (Input.GetButtonDown("B_Button") && bought)
        {
            canDelete = true;
            canValidate = false;

        }

        if(canDelete && !canValidate)
        {
            deleteTime += 0.2f;

            if (deleteTime >= 100)
            {
                deleteTime = 100;
            }
        }

    }

    public void Undo()
    {
        if (GameManager.Instance.tooMuchAnimals && deleteTime == 100)
        {
            if (isChicken)
            {
                
                GameManager.Instance.totalAnimalWeight -= 1;
            }
            if (isCow)
            {
               
                GameManager.Instance.totalAnimalWeight -= 2;
            }
            if (isPig)
            {
                
                GameManager.Instance.totalAnimalWeight -= 4;
            }
            if (isHorse)
            {
                
                GameManager.Instance.totalAnimalWeight -= 3;
            }
        
            Canvas.SetActive(false);
            bought = false;
            GameManager.Instance.tooMuchAnimals = false;
            PlayerManager.Instance.controller.needToStop = false;
            GameManager.Instance.vegetablesCount += vegetablesCost;
        }

    }

    void UpdateIndex()
    {
        vertical = Input.GetAxis("Right_Joystick_Y");

        if (GameManager.Instance.tooMuchAnimals && canValidate && canDelete)
        {
            if (vertical > 0.15)
            {
                modifierIndex = 1;
                needToChange = false;
            }
            if (vertical < -0.15)
            {
                needToChange = false;
                modifierIndex = -1;
            }

            if (vertical < 0.15 && vertical > -0.15)
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

    void UpdateUI()
    {
        validationCircle.transform.position = positions[currentIndex].transform.position;

        maxLimit.text = GameManager.Instance.maxStoredUnits.ToString() + " Limit.";

        storedUnits.text = GameManager.Instance.totalAnimalWeight.ToString() + "Actual Weight.";

        price.text = vegetablesCost.ToString();
    }


    IEnumerator Error()
    {
        Debug.Log("Called");
        crossX.enabled = true;
        yield return new WaitForSeconds(0.5f);
        crossX.enabled = false;
    }
}
