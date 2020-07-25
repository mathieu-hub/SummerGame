using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using Seller;

public class ShopItem : MonoBehaviour
{
    private float distance;
    public int index;

    [SerializeField] private GameObject AButton;
    private bool APressed = false;

    [SerializeField] private int vegetablesCost;

    // Start is called before the first frame update
    void Start()
    {
        AButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position) < 2)
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

    }
    void Buy()
    {
        if(GameManager.Instance.vegetablesCount >= vegetablesCost)
        {
            GameManager.Instance.vegetablesCount -= vegetablesCost;

            SellerBehaviour.Instance.items.RemoveAt(index);
            Debug.Log("buy");
            Destroy(gameObject);
        }
        else
        {
            print("error");
        }
                
    }
}
