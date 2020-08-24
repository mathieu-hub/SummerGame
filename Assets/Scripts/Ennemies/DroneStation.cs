using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DroneStation : MonoBehaviour
	{
        //PODS
        //public GameObject pods;
        //public Transform podsSpawn;

        //TROOPS
        //public GameObject troops;
        //public Transform troopsSpawn;

        public GameObject bar;
        public bool barIsReinitialized = false;
        public static int droneInTheStation = 0;
        public List<GameObject> droneArrived = new List<GameObject>();



        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<DroneMovement>().isNotDrone == false)
            {
                if (other.gameObject.GetComponent<DroneMovement>().isAdd == false)
                {
                    droneArrived.Add(other.gameObject);
                    other.gameObject.GetComponent<DroneMovement>().droneIsInStation = true;
                    Debug.Log("un drone est entré");
                }                
            }
        }

        private void Update()
        {          
            if (droneInTheStation > 0)
            {
                bar.GetComponent<ResearchBar>().increaseResearchBar = true;
            }
            

            if (bar.GetComponent<ResearchBar>().barIsComplete == true)
            {
                ResearchComplete();
            }
        }

        void ResearchComplete()
        {
            bar.GetComponent<ResearchBar>().barIsComplete = false;
            //barIsReinitialized = true;
            Debug.Log("la bar est complete");
            bar.GetComponent<ResearchBar>().ReinitializeResearchBar();
            bar.GetComponent<ResearchBar>().increaseResearchBar = false;
            droneInTheStation = 0;

            for (int i = 0; i < droneArrived.Count; i++)
            {
                Debug.Log("Destruction Drone");
                Destroy(droneArrived[i]);
                WaveSpawner.ennemyAlive--;
            }

            //SpawnPods();
        }

        /*void SpawnPods()
        {
            Instantiate(pods, podsSpawn.transform.position, Quaternion.identity);
            StartCoroutine(DropTroops());
        }

        IEnumerator DropTroops()
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(troops, troopsSpawn.transform.position, Quaternion.identity);
        }*/

    }
}

