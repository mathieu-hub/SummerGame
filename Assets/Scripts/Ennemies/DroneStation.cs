using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneStation : MonoBehaviour
	{
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "DroneCollider")
            {
                other.gameObject.GetComponent<EnnemiesMovement>().droneIsInStation = true;
                Debug.Log("un drone est entré");
            }
        }
    }
}

