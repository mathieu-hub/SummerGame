using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;



namespace Bullet{
    /// <summary>
    /// This script makes the Player's Bullet Moving and increasing damage zone;
    /// </summary>
    public class BulletBehaviour : MonoBehaviour
    {
        #region
        [Header("")]
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

        private CircleCollider2D circleCol;
        private BoxCollider2D boxCol;
        private Rigidbody2D rbBullet;


        public List<GameObject> ennemiesTouched = new List<GameObject>();
        private GameObject thisEnemy;
        private int ennemiesCount = 0;

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
            //DamageRange();
            
            //Checker le nombre de légumes consommés pour attaquer pour adapter la zone de dégats;
            
            Movement();
        }

        private void Update()
        {
            if (needToStop)
            {
                direction = Vector2.zero;
                boxCol.enabled = false;
                circleCol.enabled = true;
                //DamageRange();
                Damages();
            }

            rbBullet.velocity = direction.normalized * speed * Time.smoothDeltaTime;

            if(doDamages && damageDone)
            {
                Dead();
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                needToStop = true;
                
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

            direction = new Vector2(horizontal, vertical);

        }

        void DamageRange()
        {
            if(PlayerManager.Instance.attack.numberOfVegetablesEat == 1)
            {
                circleCol.radius = storedRadiuslevel1;
            }
            if (PlayerManager.Instance.attack.numberOfVegetablesEat == 2)
            {
                circleCol.radius = storedRadiuslevel2;
            }
            if (PlayerManager.Instance.attack.numberOfVegetablesEat == 3)
            {
                circleCol.radius = storedRadiuslevel3;
            }

            circleCol.enabled = false;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        void Damages()
        {
            //DamageRange();

            doDamages = true;

            for (int i = 0; i < ennemiesTouched.Count; i++)
            {
                damageDone = true;
              
                ennemiesTouched[i].GetComponent<EnemyController>().TakeDamages = 5;

                thisEnemy = ennemiesTouched[i];

                ennemiesTouched.Remove(thisEnemy);
                thisEnemy = null;

                if(ennemiesTouched.Count == 0)
                {
                    StartCoroutine("Dead");
                }
            }

            Dead();
        }
       
        IEnumerator Dead()
        {
            Debug.Log("calleddead");
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }

    }
}

