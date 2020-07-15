using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class PlayerAim : MonoBehaviour
    {
        #region
        SpriteRenderer aimRenderer;
        public GameObject aim;


        [HideInInspector] public Vector3 rotationVector = Vector3.zero;
        [HideInInspector] public Quaternion orientationQuaternion;

        public float horizontal;
        public float vertical;


        #endregion
        private void Start()
        {
            aimRenderer = aim.GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            Aim();
        }
        private void Aim()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");
            vertical = -Input.GetAxis("Right_Joystick_Y");

            
            if (horizontal < -0.01 || horizontal > 0.01 || vertical < -0.01 || vertical > 0.01)
            {
                aimRenderer.enabled = true;
                //Permet de prendre la position sur un cercle.
                rotationVector = new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * 180 / Mathf.PI);
                //Obligé d'utiliser les Quaternions pour les rotations
                orientationQuaternion = Quaternion.Euler(rotationVector);
                //Oriente le sprite selon la position définie.
                gameObject.transform.rotation = orientationQuaternion;
            }
            else
            {
                aimRenderer.enabled = false;
            }


        }
    }
}

