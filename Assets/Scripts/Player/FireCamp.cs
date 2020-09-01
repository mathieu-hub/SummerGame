using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using AudioManager;

namespace House
{
    /// <summary>
    /// This script makes the FireCamp Behaviour.
    /// </summary>

    public class FireCamp : MonoBehaviour
    {
        private AudioSource audioSource;

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
            audioSource = GetComponent<AudioSource>();
            


            circleCol = gameObject.GetComponent<CircleCollider2D>();
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = true;
                PlayerManager.Instance.Life.needToHeal = true;
                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 32);
              
                audioSource.Play();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerController"))
            {
                playerHere = false;
                PlayerManager.Instance.Life.needToHeal = false;
                audioSource.Stop();
            }
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, circleCol.radius);
        }

       
    }
}

