using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace House
{
    /// <summary>
    /// This script makes the FireCamp Behaviour.
    /// </summary>

    public class FireCamp : MonoBehaviour
    {
        #region
        //BoxCollider reference
        public CircleCollider2D circleCol;
        private bool playerHere;
  

        [Header("Variables")]
        [Range(1, 10)]
        [SerializeField] private int healAmount;

        [Header("Clock")]
        public Clock clock;
        [Range(1, 10)]
        [SerializeField] private float duration;
        private bool clockWorking;
        #endregion

        private void Start()
        {
            clock = new Clock(duration);
            clock.Pause();
            Debug.Log(clock.time);
            circleCol = gameObject.GetComponent<CircleCollider2D>();
        }
        private void Update()
        {
            if(playerHere && !clockWorking && PlayerManager.Instance.Life.currentHealthPoint < PlayerManager.Instance.Life.maxHealthPoint)
            {
                clockWorking = true;
                Heal();
            }

            if (clock.onFinish && playerHere)
            {
                PlayerManager.Instance.Life.Heal = healAmount;
                clock.SetTime(duration);
                clock.Pause();
                clockWorking = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = false;
                clockWorking = false;
                clock.SetTime(duration);
                clock.Pause();

            }
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, circleCol.radius);
        }

        void Heal()
        {
            clock.Play();

        }
    }
}

