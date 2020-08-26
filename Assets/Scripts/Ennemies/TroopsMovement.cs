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

        [Header("Location")]
        [SerializeField] private Transform targetMovement;
        public Transform player;
        public Transform siloPoint;
        public GameObject baseArea;
        public bool isOnTheSilo = false;

        [Header("Damage")]
        public bool canMakeDamage = true;
        public bool doingDamage = false;
        [SerializeField] private int ennemyDamage;
        [SerializeField] private int speedAttack;

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
            
            if (isOnTheSilo == true)
            {
                speed = stopSpeed;
                StartCoroutine(AttackSilo());
            }
        }

        void InitialisationValues()
        {
            initialspeed = 10f;
            speed = initialspeed;
            ennemyDamage = 5;
            speedAttack = 2;
            //animator.SetBool("isDrone", true);
        }   
        
        IEnumerator AttackSilo()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                canMakeDamage = false;
                doingDamage = true;
                SiloLife.lives -= ennemyDamage;
                yield return new WaitForSeconds(speedAttack);
                canMakeDamage = true;
                doingDamage = false;
            }
        }
        
    }
}

