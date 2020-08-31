﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class EnnemiesHealth : MonoBehaviour
    {
        //TYPE OF ENNEMY
        public enum TypeOfEnnemy {Walker, Soldonaute, SpaceScoot, Démolisseur, Carboniseur, Rover, Drone, Trooper}
        public TypeOfEnnemy typeOfEnnemy;
        

        //Health
        int maxHealth;
        public int currentHealth;

        public bool isInvincible = false;

        //LOOT
        public GameObject lootDrop;

        //Squad
        public GameObject squad;
        

        //Particles
        [Header("Partciles")]
        public GameObject bigExplo;
        public GameObject lowExplo;

        void Awake()
        {
            if (typeOfEnnemy == TypeOfEnnemy.Walker)
            {
                maxHealth = 5;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Soldonaute)
            {
                maxHealth = 20;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.SpaceScoot)
            {
                maxHealth = 10;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Démolisseur)
            {
                maxHealth = 50;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Carboniseur)
            {
                maxHealth = 30;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                maxHealth = 45;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Drone)
            {
                maxHealth = 15;
            }
            else if (typeOfEnnemy == TypeOfEnnemy.Trooper)
            {
                maxHealth = 15;
            }

            //initialisation des explo
            bigExplo.SetActive(false);
            lowExplo.SetActive(false);
        }

        private void Update()
        {
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        void Start()
        {
            currentHealth = maxHealth;

            if (typeOfEnnemy != TypeOfEnnemy.Rover)
            {
                squad = null;
            }
        }


        //Prise de Dégâts et Mort de l'ennemi (à utiliser dans les scripts tourelles par la suite)
        public void TakeDammage(int damage)
        {
            currentHealth -= damage;

            /*if (!isInvincible)
            {
                isInvincible = true;
                currentHealth -= damage;
                StartCoroutine(TakingDammage());
                Debug.Log("Enemy " + gameObject.name + " took " + damage + " damage!");
            }*/

            
        }

       

        private void Die()
        {
            WaveSpawner.ennemyAlive--;
            Instantiate(lootDrop, transform.position, Quaternion.identity);

            if (typeOfEnnemy == TypeOfEnnemy.Drone && gameObject.GetComponent<DroneMovement>().isAdd == true)
            {
                Debug.Log("On est ici la");
                DroneStation.droneInTheStation--;

                GameMaster.Instance.DroneStation.GetComponent<DroneStation>().droneArrived.Remove(gameObject);

                Destroy(gameObject);

            }

            if (typeOfEnnemy == TypeOfEnnemy.Rover)
            {
                squad.GetComponent<NewEnnemiMovement>().currentWay = gameObject.GetComponent<NewEnnemiMovement>().currentWay;
                squad.GetComponent<NewEnnemiMovement>().wayPointIndex = gameObject.GetComponent<NewEnnemiMovement>().wayPointIndex;

                StartCoroutine(DropSquad());
            }

            if (typeOfEnnemy != TypeOfEnnemy.Rover)
            {
                ChooseExplo();
                Destroy(gameObject);
            }
            

        }

        //IEnumerator TakingDammage()
        //{
        //    yield return new WaitForSeconds(2f);
        //    isInvincible = false;
        //}

        private void ChooseExplo()
        {
            if( typeOfEnnemy != TypeOfEnnemy.Rover || typeOfEnnemy != TypeOfEnnemy.Démolisseur)
            {
                bigExplo.SetActive(false);
                lowExplo.SetActive(true);
            }
            else
            {
                lowExplo.SetActive(false);
                bigExplo.SetActive(true);
            }
        }

        IEnumerator DropSquad()
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(squad, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            Instantiate(squad, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            Instantiate(squad, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(0.3f);
            
            ChooseExplo();
            Destroy(gameObject);
        }
    }
}

