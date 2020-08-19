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

        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            research = new Research();
        }

        private void Update()
        {
            research.Update();

            barImage.fillAmount = research.GetNormalized();
        }
    }

    public class Research
    {
        public const int researchMax = 100;

        private float researchAmount;
        private float researchIncrease;

        public Research()
        {
            researchAmount = 0f;
            researchIncrease = 30f;
        }

        public void Update()
        {
            researchAmount += researchIncrease * Time.deltaTime;
        }

        public float GetNormalized()
        {
            return researchAmount / researchMax;
        }

    }
}

