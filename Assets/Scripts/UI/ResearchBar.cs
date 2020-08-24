using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class ResearchBar : MonoBehaviour
	{
        public float TheValue;
        private Research research;
        private Image barImage;

        public bool barIsComplete = false;
        public bool increaseResearchBar = false;

        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            research = new Research();
        }

        private void Update()
        {
            barImage.fillAmount = research.GetNormalized();

            TheValue = research.researchAmount;

            if (increaseResearchBar == true)
            {
                research.Update(); //uniquement lorsque des drones sont dans DroneStation
            }            

            if (research.researchIsComplete == true)
            {
                barIsComplete = true;
            }
            else 
            {
                barIsComplete = false;
            }
        }


        public void ReinitializeResearchBar()
        {
            research.researchAmount = 0;
            barIsComplete = false;
            Debug.Log("Bar Reinitialized");
        }
    }




    public class Research
    {
        public const int researchMax = 100;

        public float researchAmount;
        private float researchIncrease;
        public bool researchIsComplete = false;

        public Research()
        {
            researchAmount = 0f;
            researchIncrease = 15f;
        }

        public void Update()
        {
            researchAmount += researchIncrease * Time.deltaTime;
            researchAmount = Mathf.Clamp(researchAmount, 0f, researchMax);   
            
            if (researchAmount >= researchMax)
            {
                researchIsComplete = true;
            }
            else if (researchAmount < researchMax)
            {
                researchIsComplete = false;
            }
        }

        public float GetNormalized()
        {
            return researchAmount / researchMax;
        }

    }
}

