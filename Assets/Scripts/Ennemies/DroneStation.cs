using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneStation : MonoBehaviour
	{
        public GameObject Pods;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<NewEnnemiMovement>().isNotDrone == true)
            {
                other.gameObject.GetComponent<NewEnnemiMovement>().droneIsInStation = true;  
                Debug.Log("un drone est entré");
            }
        }

        void ResearchBar()
        {

        }

        void SpawnPods()
        {

        }
    }
}

