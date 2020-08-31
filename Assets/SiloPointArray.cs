using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;
using Management;

public class SiloPointArray : MonoBehaviour
{
     public static List<GameObject> Ennemies = new List<GameObject>();

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnnemiesHealth>().typeOfEnnemy != EnnemiesHealth.TypeOfEnnemy.Drone)
        {
            if(other.gameObject.GetComponent<EnnemiesHealth>().typeOfEnnemy == EnnemiesHealth.TypeOfEnnemy.Trooper)
            {
                Ennemies.Add(other.gameObject);
                other.gameObject.GetComponent<TroopsMovement>().isAdd = true;
            }

            else if (other.gameObject.GetComponent<NewEnnemiMovement>().isNotDrone == false && other.gameObject.GetComponent<NewEnnemiMovement>().isAdd == false)
            {
                //je l'ajoute à un tableau.  La Bool "isAdd" permet de bloquer un sur Ajout.
                Ennemies.Add(other.gameObject);
                other.gameObject.GetComponent<NewEnnemiMovement>().isAdd = true;
                Debug.Log("un drone est entré");
            }
        }

        //Dès qu'un drone passe dans le trigger

        

    }

    private void Update()
    {
        Debug.Log(Ennemies.Count);

        if (Ennemies.Count !=0)
        {
            GameManager.Instance.underAttack = true;
        }
        else
        {
            GameManager.Instance.underAttack = false;
        }
    }
}
