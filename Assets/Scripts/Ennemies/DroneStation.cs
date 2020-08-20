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

        public int droneInTheStation = 0;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<DroneMovement>().isNotDrone == false)
            {
                other.gameObject.GetComponent<DroneMovement>().droneIsInStation = true;
                droneInTheStation++;
                Debug.Log("un drone est entré");
            }
        }

        private void Update()
        {
            if (droneInTheStation >= 1)
            {
                GetComponent<ResearchBar>().IncreaseResearchBar();
            }

            if (GetComponent<ResearchBar>().barIsComplete == true)
            {
                Debug.Log("la bar est complete");
                droneInTheStation = 0;
                GetComponent<ResearchBar>().ReinitializeResearchBar();
                GetComponent<Research>().researchIsComplete = false;
                GetComponent<ResearchBar>().barIsComplete = false;
                //SpawnPods();
            }
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

