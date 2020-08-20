using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ennemies
{
	public class ResearchBar : MonoBehaviour
	{
        private Research research;
        private Image barImage;

        public bool barIsComplete = false;

        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            research = new Research();
        }

        private void Update()
        {
            barImage.fillAmount = research.GetNormalized();

            if (research.researchIsComplete == true)
            {
                barIsComplete = true;
            }
        }

        public void IncreaseResearchBar()
        {
            research.Update(); //uniquement lorsque des drones sont dans DroneStation
        }
    }




    public class Research
    {
        public const int researchMax = 100;

        private float researchAmount;
        private float researchIncrease;
        public bool researchIsComplete = false;

        public Research()
        {
            researchAmount = 0f;
            researchIncrease = 3f;
        }

        public void Update()
        {
            researchAmount += researchIncrease * Time.deltaTime;
            researchAmount = Mathf.Clamp(researchAmount, 0f, researchMax);   
            
            if (researchAmount >= researchMax)
            {
                researchIsComplete = true;
            }
        }

        public float GetNormalized()
        {
            return researchAmount / researchMax;
        }

    }
}

