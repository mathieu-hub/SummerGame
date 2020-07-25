using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Seller
{/// <summary>
/// This script spawn object according an array each 5 waves.
/// </summary>
    public class SellerBehaviour : MonoBehaviour
    {
        #region Variables
        [Header("Items to Sell")]
        [SerializeField] private GameObject[] items;
        [SerializeField] private GameObject[] positions;

        private int numberOfItemCalled = 0;
        #endregion

        private void Start()
        {
            Initialisation();
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.wavesBeforeSeller == 0 && numberOfItemCalled < 5)
            {
                Seller();
            }

            if (GameManager.Instance.needToRefeshShop)
            {
                Initialisation();
            }
        }

        void Seller()
        {
            if(numberOfItemCalled < 5)
            {

                int index = Random.Range(0, items.Length);
                Debug.Log(index);

                if(items[index] = null)
                {
                    return;
                }

                Instantiate(items[index], positions[numberOfItemCalled].transform.position, Quaternion.identity);
                numberOfItemCalled += 1;
                
            }
        }

       void Initialisation()
        {
            numberOfItemCalled = 0;
            GameManager.Instance.needToRefeshShop = false;
        }
    }

}
