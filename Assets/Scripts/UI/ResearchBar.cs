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

        public static bool barIsComplete = false;

        public bool canIncrease = false;

        [Header("PODS")]
        public GameObject pods;

        public GameObject podsSpawned;
        public Transform podsSpawn;
        public Transform podsSpawn2;
        public Transform podsSpawn3;
        private int randomSpawn;

        [Header("TROOPS")]
        public GameObject trooper;
        public Transform troopsSpawn;
        public Transform TS01;
        public Transform TS02;
        public Transform TS03;
        
        
        

        private void Awake()
        {
            barImage = transform.Find("jauge").GetComponent<Image>();

            trooper = Resources.Load("Prefabs/Trooper") as GameObject;
        }

        private void Update()
        {
            barImage.fillAmount = TheValue / 100f;

            if(GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count > 0 && barIsComplete == false)
            {
                print("Ca passe");
                canIncrease = true;
            }
            else
            {
                canIncrease = false;
            }
                    

            if(canIncrease == true)
            {
                TheValue += 0.1f;
            }

            if(TheValue > 100)
            {
                TheValue = 100;
                canIncrease = false;
            }

            if(TheValue == 100)
            {
               StartCoroutine(FullBar());
            }

        }

        IEnumerator FullBar()
        {
            barIsComplete = true;

            TheValue = 0;
            yield return new WaitForSeconds(0.2f);

            WaveSpawner.ennemyAlive -= GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Count;
            GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Clear();

            SpawnPods();
            barIsComplete = false;

        }

        public void SpawnPods()
        {
            randomSpawn = Random.Range(0, 2);
            Debug.Log("YAAAAAA" + randomSpawn);

            if (randomSpawn == 0)
            {
                podsSpawned =  Instantiate(pods, podsSpawn.transform.position, Quaternion.identity);
                troopsSpawn = TS01;
            }
            else if (randomSpawn == 1)
            {
                podsSpawned = Instantiate(pods, podsSpawn2.transform.position, Quaternion.identity);
                troopsSpawn = TS02;
            }
            else if (randomSpawn == 2)
            {
                podsSpawned = Instantiate(pods, podsSpawn3.transform.position, Quaternion.identity);
                troopsSpawn = TS03;
            }

            StartCoroutine(DropTroops());
        }

        IEnumerator DropTroops()
        {            
            yield return new WaitForSeconds(0.3f);
            Instantiate(trooper, troopsSpawn.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            Instantiate(trooper, troopsSpawn.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            Instantiate(trooper, troopsSpawn.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.3f);

            barIsComplete = false;
            podsSpawned = false;
            Destroy(podsSpawned);
        }

    }

}

