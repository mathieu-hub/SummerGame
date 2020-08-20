﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
	public class DetectionDrone : MonoBehaviour
	{
		public bool isStartPoint;
        public bool isEndPoint;
        public bool isSiloPoint;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<NewEnnemiMovement>().isNotDrone == true)
            {
                if (isStartPoint)
                {
                    other.gameObject.GetComponent<DroneMovement>().passedTheFirst = true;
                    other.gameObject.GetComponent<DroneMovement>().passedTheLast = false;
                    other.gameObject.GetComponent<DroneMovement>().passedTheSilo = false;

                }
                else if (isEndPoint)
                {
                    other.gameObject.GetComponent<DroneMovement>().passedTheFirst = false;
                    other.gameObject.GetComponent<DroneMovement>().passedTheLast = true;
                    other.gameObject.GetComponent<DroneMovement>().passedTheSilo = false;
                }
                else if (isSiloPoint)
                {
                    other.gameObject.GetComponent<DroneMovement>().passedTheFirst = false;
                    other.gameObject.GetComponent<DroneMovement>().passedTheLast = false;
                    other.gameObject.GetComponent<DroneMovement>().passedTheSilo = true;
                }
            }
        }
    }
}

