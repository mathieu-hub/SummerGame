using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Seller
{/// <summary>
/// This script spawn object according an array each 5 waves.
/// </summary>
    public class SellerBehaviour : Singleton<SellerBehaviour>
    {
        #region Variables
        [Header("Items to Sell")]
        public List<GameObject> items = new List<GameObject>();
        [SerializeField] private GameObject[] positions;
        public List<int> intUsed = new List<int>();

        private int numberOfItemCalled = 0;
        #endregion

        private void Awake()
        {
            MakeSingleton(true);
        }

        private void Start()
        {
            Initialisation();
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.wavesBeforeSeller == 0 && numberOfItemCalled < positions.Length && )
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
            Debug.Log("Seller");
            if(numberOfItemCalled < positions.Length)
            {

                int indexInArray = Random.Range(0, items.Count);
                intUsed.Add(indexInArray);
                Debug.Log(indexInArray);

                if (numberOfItemCalled > 1)
                {
                    //Pour les objets déja sortis dans le shop actuel.
                    for (int i = 0; i < intUsed.Count; i++)
                    {
                        if (indexInArray == intUsed[i])
                        {
                            return;
                        }
                    }

                    var myNewItem = Instantiate(items[indexInArray], positions[numberOfItemCalled].transform.position, Quaternion.identity);

                    myNewItem.GetComponent<ShopItem>().index = indexInArray;

                    numberOfItemCalled += 1;
                    Debug.Log(numberOfItemCalled + "number Of Item Called");
                }
                else
                {
                    var myNewItem = Instantiate(items[indexInArray], positions[numberOfItemCalled].transform.position, Quaternion.identity);

                    myNewItem.GetComponent<ShopItem>().index = indexInArray;

                    numberOfItemCalled += 1;
                    Debug.Log(numberOfItemCalled + "number Of Item Called");
                }
                

              
            }
        }

       void Initialisation()
       {
            numberOfItemCalled = 0;
            GameManager.Instance.needToRefeshShop = false;
       }

    }

}
