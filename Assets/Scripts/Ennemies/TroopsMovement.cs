using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies 
{
	public class TroopsMovement : MonoBehaviour
	{
        public bool isNotDrone;

        [Header("Values")]
        [SerializeField] private float speed;
        [SerializeField] private float initialspeed;
        [SerializeField] private float stopSpeed = 0f;

        [SerializeField] private Transform targetMovement;
        public Transform player;
        public Transform siloPoint;
        public GameObject baseArea;

        //[Header("Animator")]
        //public Animator animator;

        void Awake()
        {
            InitialisationValues();            
        }

        void Start()
		{
			if (baseArea.GetComponent<DetectionPlayer>().playerInBase == true)
            {
                targetMovement = player;
            }
            else if (baseArea.GetComponent<DetectionPlayer>().playerInBase == false)
            {
                targetMovement = siloPoint;
            }
		}

		void Update()
		{
            //Movements
            Vector3 direction = targetMovement.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);            
        }

        void InitialisationValues()
        {
            initialspeed = 10f;
            speed = initialspeed;
            //animator.SetBool("isDrone", true);
        }        
        
    }
}

