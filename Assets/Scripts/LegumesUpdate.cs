using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Production
{
    public class LegumesUpdate : MonoBehaviour
    {

        public List<GameObject> vegetables = new List<GameObject>();

        public int numberOfVegetablesCreated;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform child in transform)
            {
                vegetables.Add(child.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            VisuelUpdate();

            numberOfVegetablesCreated = gameObject.GetComponentInParent<GardenBehaviour>().storedVegetable;

            if (gameObject.GetComponentInParent<GardenBehaviour>().refresh)
            {
                for (int i = 0; i < vegetables.Count; i++)
                {
                    vegetables[i].GetComponent<SpriteRenderer>().sprite = null;
                }

                gameObject.GetComponentInParent<GardenBehaviour>().refresh = false;
            }
        }



        void VisuelUpdate()
        {
            if(numberOfVegetablesCreated == 0)
            {
                
                    vegetables[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                

            }
            else if (numberOfVegetablesCreated == 1)
            {
               
                    vegetables[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");
                    vegetables[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                

               
            }
            else if (numberOfVegetablesCreated == 2)
            {
                
                    vegetables[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");
                    vegetables[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                
            }
            else if (numberOfVegetablesCreated == 3)
            {
                
                    vegetables[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");
                    vegetables[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                
            }
            else if (numberOfVegetablesCreated == 4)
            {
                
                    vegetables[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");
                    vegetables[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                
            }
            else if(numberOfVegetablesCreated == 5)
            {
                
                    vegetables[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");
                    vegetables[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ1");
                
            }
            else if (numberOfVegetablesCreated == 6)
            {
                
                    vegetables[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/LGJ2");

            }
        }
    }

}
