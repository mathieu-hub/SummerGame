using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// This script makes the Player's Bullet Moving and increasing damage zone;
    /// </summary>
    public class BulletBehaviour : MonoBehaviour
    {
        #region
        private float horizontal;
        private float vertical;

        private Vector2 direction;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //Checker le nombre de légumes consommés pour attaquer;
            Movement();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Movement()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");
            vertical = -Input.GetAxis("Right_Joystick_Y");

            direction = new Vector2(horizontal, vertical).normalized;
        }

        void DamageRange()
        {

        }
    }
}

