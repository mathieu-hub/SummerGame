using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneStation : MonoBehaviour
	{
        
        public GameObject bar;
        public bool barIsReinitialized = false;
        public static int droneInTheStation = 0;
        public List<GameObject> droneArrived = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.GetComponent<DroneMovement>().isNotDrone == false && other.gameObject.GetComponent<DroneMovement>().isAdd == false)
                {
                    //je l'ajoute à un tableau.  La Bool "isAdd" permet de bloquer un sur Ajout.
                    droneArrived.Add(other.gameObject);
                    other.gameObject.GetComponent<DroneMovement>().droneIsInStation = true;
                    Debug.Log("un drone est entré");
                }
            }

            //Dès qu'un drone passe dans le trigger
            
        }

        private void Update()
        {
            droneInTheStation = droneArrived.Count;
            
        }        

    }
}

