using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

namespace Player
{
	public class DirectionnalLogo : MonoBehaviour
	{
        [SerializeField] private Transform target;

        //GameObject References
        SpriteRenderer logoRenderer;
        public GameObject logo;
        public bool activateLogo = false;


        [HideInInspector] public Vector3 rotationVector = Vector3.zero;
        [HideInInspector] public Quaternion orientationQuaternion;

        [HideInInspector] public float horizontal;
        [HideInInspector] public float vertical;


        private void Start()
        {
            logoRenderer = logo.GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            LogoOrientation();
        }
        private void LogoOrientation()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");
            vertical = -Input.GetAxis("Right_Joystick_Y");

            //Checker si un input est enclenché.
            if (horizontal < -0.1 || horizontal > 0.1 || vertical < -0.1 || vertical > 0.1)
            {
                logoRenderer.enabled = true;
                //Permet de prendre la position sur un cercle.
                rotationVector = new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * 180 / Mathf.PI);
                //Obligé d'utiliser les Quaternions pour les rotations
                orientationQuaternion = Quaternion.Euler(rotationVector);
                //Oriente le sprite selon la position définie.
                gameObject.transform.rotation = orientationQuaternion;

            }
            else
            {
                logoRenderer.enabled = false;
            }


        }
        
	}
}

