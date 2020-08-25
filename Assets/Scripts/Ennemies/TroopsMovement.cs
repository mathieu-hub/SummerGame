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

        //[Header("Animator")]
        //public Animator animator;

        void Awake()
        {
            InitialisationValues();            
        }

        void Start()
		{
			
		}

		void Update()
		{
			
		}

        void InitialisationValues()
        {
            initialspeed = 10f;
            speed = initialspeed;
            //animator.SetBool("isDrone", true);
        }
    }
}

