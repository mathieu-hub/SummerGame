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
                //other.gameObject.GetComponent<EnnemiesMovement>().droneIsInStation = true; => à changer, utiliser comme condition pour faire augmenter la jauge. 
                Debug.Log("un drone est entré");
            }
        }

        void SpawnPods()
        {

        }
    }
}

