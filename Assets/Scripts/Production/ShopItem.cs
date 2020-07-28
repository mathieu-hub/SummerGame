using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using Seller;

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

    // Start is called before the first frame update
    void Start()
    {
        AButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position) < 2 && pulled)
        {
            AButton.SetActive(true);

            if (Input.GetButtonDown("A_Button"))
            {
                Buy(); 
            }

        }
        else
        {
            AButton.SetActive(false);
        }


        if (GameManager.Instance.needToRefeshShop && pulled)
        {
            Recall();
        }
    }
    void Buy()
    {
        if(GameManager.Instance.vegetablesCount >= vegetablesCost)
        {
            GameManager.Instance.vegetablesCount -= vegetablesCost;
            Effects();
            Debug.Log("buy");
            Destroy(gameObject);
        }
        else
        {
            print("error");
        }
                
    }

    void Recall()
    {
        Debug.Log("Recall");
        pulled = false;
        SellerBehaviour.Instance.items.Add(gameObject);
        gameObject.transform.position = SellerBehaviour.Instance.stockPosition.position;
    }

    void Effects()
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
        if(isBarnUpgrade)
        {
            GameManager.Instance.numberOfGardens += 1;
        }
        if (isGardenUpgrade)
        {
            GameManager.Instance.maxStoredUnits += 3;

        }

    }

}
