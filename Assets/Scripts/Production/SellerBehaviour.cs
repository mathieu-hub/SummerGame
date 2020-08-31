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
        public Transform stockPosition;

        private SpriteRenderer spriteRendSeller;

        [Header("Socles")]
        [SerializeField] private GameObject socle1;
        [SerializeField] private GameObject socle2;
        [SerializeField] private GameObject socle3;
        [SerializeField] private GameObject socle4;

        private int numberOfItemCalled = 0;

        private bool isOut = false;
        #endregion

        private void Awake()
        {
            MakeSingleton(true);
        }

        private void Start()
        {
            spriteRendSeller = GetComponent<SpriteRenderer>();

            spriteRendSeller.enabled = false;

            
            socle1.SetActive(false);
            socle2.SetActive(false);
            socle3.SetActive(false);
            socle4.SetActive(false);
        }

        private void OnBecameVisible()
        {
            if (GameManager.Instance.isMarchand)
            {
                GameManager.Instance.isMarchand = false;
            }
        }

        // Update is called once per frame
        void Update()
        {

            if(GameManager.Instance.wavesBeforeSeller == 0 && numberOfItemCalled < positions.Length)
            {
                Seller();
            }

            if (GameManager.Instance.needToRefeshShop)
            {
                GameManager.Instance.isMarchand = false;

                Initialisation();

                socle1.SetActive(false);
                socle2.SetActive(false);
                socle3.SetActive(false);
                socle4.SetActive(false);

                spriteRendSeller.enabled = false;

                isOut = false;
            }

            if(GameManager.Instance.wavesBeforeSeller == 0)
            {
                socle1.SetActive(true);
                socle2.SetActive(true);
                socle3.SetActive(true);
                socle4.SetActive(true);

                spriteRendSeller.enabled = true;

                isOut = true;
            }

            if (isOut)
            {
                //Tu peux jouer l'anim ici en boucle
            }
            else
            {
                //Tu peux stop l'anim ici
            }

        }

        void Seller()
        {
            GameManager.Instance.isMarchand = true;
            Debug.Log("Seller");

            if (numberOfItemCalled < positions.Length)
            {

                int indexInArray = Random.Range(0, items.Count);
                intUsed.Add(indexInArray);
                Debug.Log(indexInArray);

                var myNewItem = items[indexInArray];
                myNewItem.transform.position = positions[numberOfItemCalled].transform.position;

                myNewItem.GetComponent<ShopItem>().pulled = true;

                myNewItem = items[indexInArray];

                
                positions[numberOfItemCalled].GetComponent<PositionSeller>().storedItem = myNewItem;
                positions[numberOfItemCalled].GetComponent<PositionSeller>().placed = true;

                items.Remove(myNewItem);

                numberOfItemCalled += 1;
                Debug.Log(numberOfItemCalled + "number Of Item Called");

            }
        }

       void Initialisation()
       {
            numberOfItemCalled = 0;

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].GetComponent<PositionSeller>().sprRenderer.enabled = false;
                positions[i].GetComponent<PositionSeller>().storedItem = positions[i].gameObject;
            }
            
       }

    }

}
