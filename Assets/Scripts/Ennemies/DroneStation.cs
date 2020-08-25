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
            //Dès qu'un drone passe dans le trigger
            if (other.gameObject.GetComponent<DroneMovement>().isNotDrone == false && other.gameObject.GetComponent<DroneMovement>().isAdd == false)
            {
                //je l'ajoute à un tableau.  La Bool "isAdd" permet de bloquer un sur Ajout.
                    droneArrived.Add(other.gameObject);
                    other.gameObject.GetComponent<DroneMovement>().droneIsInStation = true;
                    Debug.Log("un drone est entré");           
            }
        }

        private void Update()
        {
            droneInTheStation = droneArrived.Count;
        }

        void ResearchComplete()
        {
            //bar.GetComponent<Research>().researchIsComplete = false;
            ////barIsReinitialized = true;
            //Debug.Log("la bar est complete");
            //bar.GetComponent<ResearchBar>().ReinitializeResearchBar();
            //bar.GetComponent<ResearchBar>().increaseResearchBar = false;
            //droneInTheStation = 0;

            //for (int i = 0; i < droneArrived.Count; i++)
            //{
            //    Debug.Log("Destruction Drone");
            //    Destroy(droneArrived[i]);
            //    WaveSpawner.ennemyAlive--;
            //}

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

