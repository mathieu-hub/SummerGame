using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    /// <summary>
    /// This Script makes the player move
    /// </summary>
   
[HideInInspector] public enum lastMoveDirection { Top, Down, left, Right};
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [Range(0.1f, 1000f)]
        [SerializeField] private float moveSpeed = 1;

        [Header("Bools")]
        [SerializeField] bool isMoving = false;
        [SerializeField] bool isAttacking = false;
        [SerializeField] bool needToStop = false;

        [Header("References")]
        Rigidbody2D playerRb;
        //Animator playerAnim;

        //Movement Stuff
        float horizontal = 0f, vertical = 0f;
        Vector2 movementVector = Vector2.zero;

        #endregion

        void Start()
        {
            playerRb = PlayerManager.Instance.GetComponent<Rigidbody2D>();
            //anim = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            MoveInput();
            Move();
        }

        void MoveInput()
        {
            horizontal = Input.GetAxis("Left_Joystick_X");
            vertical = -Input.GetAxis("Left_Joystick_Y");

            if ((horizontal < -0.15 || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15) && !isAttacking /*&& !isDashing*/ )
            {
                isMoving = true;
                movementVector = new Vector2(horizontal, vertical);
            }
            else
            {
                isMoving = false;
                movementVector = Vector2.zero;
            }
        }

        void Move()
        {

         playerRb.velocity = movementVector * moveSpeed;
        
        }
    }
}

