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

        public bool barIsComplete = false;

        public bool canIncrease = false;

        public bool initialState = false;
        
        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            initialState = true;
        }

        private void Update()
        {
            barImage.fillAmount = TheValue / 100f;

            if(GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count > 0 && initialState)
            {
                canIncrease = true;
            }

            if(canIncrease == true && GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived != null)
            {
                TheValue++;
            }

            if(TheValue > 100)
            {
               initialState = false;
               canIncrease = false;
               TheValue = 100;
               FullBar();
            }

        }

        void FullBar()
        {
            TheValue = 0;

            //Enleve tout les drones
            for (int i = GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count;  i > 0; i++)
            {
                GameObject thisEnemy = GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived[i];

                GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Remove(thisEnemy);
                Destroy(thisEnemy);
                WaveSpawner.ennemyAlive--;
            }


        }
     
    }




   
}

