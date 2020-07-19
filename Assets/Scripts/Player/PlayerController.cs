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
        [Range(100, 1000f)]
        [SerializeField] public float moveSpeed;
        [Range(100f, 1000f)]
        [SerializeField] public float initialMoveSpeed;
        [Range(100f, 1000f)]
        [SerializeField] public float loadingMoveSpeed;

        [Header("Bools")]
        [HideInInspector] bool isMoving = false;
        [HideInInspector] bool isAttacking = false;
        [HideInInspector] public bool needToStop = false;
        [HideInInspector] public bool playerDead = false;


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
            moveSpeed = initialMoveSpeed;
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
            if (!needToStop && !playerDead)
            {
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
          
        }

        void Move()
        {

            playerRb.velocity = movementVector * moveSpeed * Time.smoothDeltaTime;
        
        }
    }
}

