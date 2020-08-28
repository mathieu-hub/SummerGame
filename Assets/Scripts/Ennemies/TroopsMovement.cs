using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Ennemies 
{
	public class TroopsMovement : MonoBehaviour
	{
        public bool isNotDrone;

        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private float initialspeed;
        [SerializeField] private float stopSpeed = 0f;
        [SerializeField] [Range(1f, 5f)] private float stoppingDistance;

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
        [SerializeField] [Range(1f, 5f)] private float distanceToAttack;

        private SpriteRenderer spriteRend;
        private Color startColor;

        //[Header("Animator")]
        //public Animator animator;

        void Awake()
        {
            InitialisationValues();            
        }

        void Start()
		{
            spriteRend = GetComponent<SpriteRenderer>();

            startColor = spriteRend.color;

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

            if (targetMovement == player)
            {
                if(Vector2.Distance(transform.position, targetMovement.position) > stoppingDistance)
                {
                    transform.Translate(direction.normalized * speed * Time.deltaTime);
                }

                if(Vector2.Distance(transform.position, targetMovement.position) < distanceToAttack)
                {                    
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }


            if (isOnTheSilo == true)
            {
                speed = stopSpeed;
                StartCoroutine(AttackSilo());
            }
        }

        void InitialisationValues()
        {
            initialspeed = 3f;
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

        IEnumerator AttackPlayer()
        {
            yield return new WaitForSeconds(speedAttack);
            if (canMakeDamage && !doingDamage)
            {
                if (Vector2.Distance(transform.position, targetMovement.position) < distanceToAttack)
                {
                    canMakeDamage = false;
                    doingDamage = true;
                    PlayerManager.Instance.Life.currentHealthPoint -= ennemyDamage;
                    yield return new WaitForSeconds(speedAttack);
                    canMakeDamage = true;
                    doingDamage = false;
                }                
            }
        }

    }
}

