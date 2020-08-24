using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class EnnemiesHealth : MonoBehaviour
    {
        //TYPE OF ENNEMY
        public enum TypeOfEnnemy {Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone}
        public TypeOfEnnemy typeOfEnnemy;
        

        //Health
        int maxHealth;
        public int currentHealth;

        public bool isInvincible = false;

        //LOOT
        public GameObject lootDrop;

        //Squad
        public GameObject squad;
        public GameObject troop;

        void Awake()
        {
            if (typeOfEnnemy == TypeOfEnnemy.Walker)
            {
                maxHealth = 10;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
            {
                maxHealth = 200;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                maxHealth = 150;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                maxHealth = 300;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                maxHealth = 300;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                maxHealth = 500;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                maxHealth = 50;
            }
        }

        void Start()
        {
            currentHealth = maxHealth;
            troop = null;
        }


        //Prise de Dégâts et Mort de l'ennemi (à utiliser dans les scripts tourelles par la suite)
        public void TakeDammage(int damage)
        {
            if (!isInvincible)
            {
                isInvincible = true;
                currentHealth -= damage;
                StartCoroutine(TakingDammage());
                Debug.Log("Enemy " + gameObject.name + " took " + damage + " damage!");
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            WaveSpawner.ennemyAlive--;
            Instantiate(lootDrop, transform.position, Quaternion.identity);

            if (GetComponent<NewEnnemiMovement>().droneIsInStation == true)
            {
                DroneStation.droneInTheStation--;
            }

            if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                troop = Instantiate(squad, transform.position, Quaternion.identity);
                troop.GetComponent<NewEnnemiMovement>().currentWay = gameObject.GetComponent<NewEnnemiMovement>().currentWay;
                //troop.GetComponent<NewEnnemiMovement>().wayPointIndex = gameObject.GetComponent<NewEnnemiMovement>().wayPointIndex;
            }

            Destroy(gameObject);
        }

        IEnumerator TakingDammage()
        {
            yield return new WaitForSeconds(2f);
            isInvincible = false;
        }
    }
}

