using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;

namespace Ennemies
{
	public class ResearchBar : MonoBehaviour
	{
        public float TheValue;
       
        private Image barImage;

        public static bool barIsComplete = false;

        public bool canIncrease = false;

    
        
        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            
        }

        private void Update()
        {
            barImage.fillAmount = TheValue / 100f;

            if(GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count >0 && barIsComplete == false)
            {
                canIncrease = true;
            }
            else
            {
                canIncrease = false;
            }
                    

            if(canIncrease == true)
            {
                TheValue += 0.1f;
            }

            if(TheValue > 100)
            {
                TheValue = 100;
                canIncrease = false;
            }

            if(TheValue == 100)
            {
               StartCoroutine(FullBar());
            }

        }

        IEnumerator FullBar()
        {
            barIsComplete = true;

            TheValue = 0;
            yield return new WaitForSeconds(0.2f);

            WaveSpawner.ennemyAlive -= GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count;
            GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Clear();

            barIsComplete = false;

        }
     
      
    }




   
}

