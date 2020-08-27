using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DetectionTroops : MonoBehaviour
	{
        public bool isSiloPoint;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<EnnemiesHealth>().typeOfEnnemy == EnnemiesHealth.TypeOfEnnemy.Trooper)
            {
                if(other.gameObject.GetComponent<TroopsMovement>().isNotDrone == true)
                {
                    if (isSiloPoint == true)
                    {
                        other.gameObject.GetComponent<TroopsMovement>().isOnTheSilo = true;
                    }
                }

               
            }
        }
    }
}

