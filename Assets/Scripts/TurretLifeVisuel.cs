using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tower
{
    public class TurretLifeVisuel : MonoBehaviour
    {
        #region Variables
        public List<GameObject> healPoints = new List<GameObject>();

       
        #endregion

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                healPoints.Add(child.gameObject);
            }

            
        }


        // Update is called once per frame
        void Update()
        {
            if(gameObject.GetComponentInParent<TurretParent>().APressed == false)
            {
                for (int i = 0; i < gameObject.GetComponentInParent<TurretParent>().maxHp; i++)
                {
                    Debug.Log(i);
                    healPoints[i].SetActive(true);
                }

                if (gameObject.GetComponentInParent<TurretParent>().currentHp < gameObject.GetComponentInParent<TurretParent>().maxHp)
                {
                    for (int i = gameObject.GetComponentInParent<TurretParent>().currentHp; i < gameObject.GetComponentInParent<TurretParent>().maxHp; i++)
                    {
                        healPoints[i].GetComponentInParent<SpriteRenderer>().color = Color.red;
                    }


                }

                for (int i = 0; i < gameObject.GetComponentInParent<TurretParent>().currentHp; i++)
                {
                    healPoints[i].GetComponentInParent<SpriteRenderer>().color = gameObject.GetComponentInParent<TurretParent>().startColor;
                }
            }
            else
            {
                for (int i = 0; i < healPoints.Count; i++)
                {
                    healPoints[i].SetActive(false);
                }
            }

           
        }
    }
}

