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

        private void Start()
        {
            foreach (Transform child in transform)
            {
                healPoints.Add(child.gameObject);
            }
        }


        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < gameObject.GetComponent<TurretParent>().maxHp; i++)
            {
                healPoints[i].SetActive(true);
            }

            if (gameObject.GetComponent<TurretParent>().currentHp < gameObject.GetComponent<TurretParent>().maxHp)
            {
                for (int i = gameObject.GetComponent<TurretParent>().currentHp; i < gameObject.GetComponent<TurretParent>().maxHp; i++)
                {
                    healPoints[i].GetComponent<SpriteRenderer>().color = Color.red;
                }


            }

            for (int i = 0; i < gameObject.GetComponent<TurretParent>().currentHp; i++)
            {
                healPoints[i].GetComponent<SpriteRenderer>().color = gameObject.GetComponent<TurretParent>().startColor;
            }
        }
    }
}

