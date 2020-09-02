using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Ennemies;



namespace Bullet
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
        private bool needToStop;
        [SerializeField]
        [Range(100, 1000)]
        private int speed;

       [Header("CircleColliderRadius")]
       [SerializeField] private float storedRadiuslevel1;
       [SerializeField] private float storedRadiuslevel2;
       [SerializeField] private float storedRadiuslevel3;
       [SerializeField] public int numberOfVegetablesUsed;

        private CircleCollider2D circleCol;
        private BoxCollider2D boxCol;
        private Rigidbody2D rbBullet;


        public List<GameObject> ennemiesTouched = new List<GameObject>();
        private GameObject thisEnemy;
     

        private bool doDamages = false;
        private bool damageDone = false;
        [Range(1,10)]
        [SerializeField] private int damages;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            

            rbBullet = GetComponent<Rigidbody2D>();
            circleCol = GetComponent<CircleCollider2D>();
            boxCol = GetComponent<BoxCollider2D>();
            
            
            //Checker le nombre de légumes consommés pour attaquer pour adapter la zone de dégats;
            
            Movement();
            DamageRange();
        }

        private void Update()
        {
            if (needToStop)
            {
                direction = Vector2.zero;
                boxCol.enabled = false;
                circleCol.enabled = true;
                if (doDamages)
                {
                    Damages();
                }
               
            }

            rbBullet.velocity = direction.normalized * speed * Time.smoothDeltaTime;

            if(!doDamages && damageDone)
            {
                Dead();
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                needToStop = true;
                doDamages = true;

            }

            if (doDamages)
            {
                ennemiesTouched.Add(collision.gameObject);
            }
        }


        void Movement()
        {
            horizontal = Input.GetAxis("Right_Joystick_X");
            vertical = -Input.GetAxis("Right_Joystick_Y");

            direction = new Vector2(PlayerManager.Instance.Aim.aim.transform.position.x - PlayerManager.Instance.attack.instantiatePosition.transform.position.x, PlayerManager.Instance.Aim.aim.transform.position.y - PlayerManager.Instance.attack.instantiatePosition.transform.position.y);

        }

        void DamageRange()
        {
            if(PlayerManager.Instance.attack.lastNumberOfVegetablesEat == 1)
            {
                circleCol.radius = storedRadiuslevel1;
                Debug.Log(circleCol.radius);
                
            }
            if (PlayerManager.Instance.attack.lastNumberOfVegetablesEat == 2)
            {
                circleCol.radius = storedRadiuslevel2;
                Debug.Log(circleCol.radius);

            }
            if (PlayerManager.Instance.attack.lastNumberOfVegetablesEat == 3)
            {
                circleCol.radius = storedRadiuslevel3;
                Debug.Log(circleCol.radius);

            }

            circleCol.enabled = false;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
      

        void Damages()
        {
            doDamages = false;

            for (int i = 0; i < ennemiesTouched.Count; i++)
            {
              
                Debug.Log("Called Once");
                damageDone = true;
              
                ennemiesTouched[i].GetComponent<EnnemiesHealth>().currentHealth -= 5;

                thisEnemy = ennemiesTouched[i];

                ennemiesTouched.Remove(thisEnemy);
                thisEnemy = null;

                if(ennemiesTouched.Count == 0)
                {
                    StartCoroutine("Dead");
                }
            }

            
        }
       
        IEnumerator Dead()
        {
            Debug.Log("calleddead");
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }

    }
}

