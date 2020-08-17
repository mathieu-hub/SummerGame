using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneStation : MonoBehaviour
	{
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Drone")
            {
                collision.gameObject.GetComponent<EnnemiesMovement>().droneIsInStation = true;
                Debug.Log("un drone est entré");
            }
        }
    }
}

