using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;
using Management;

public class SiloPointArray : MonoBehaviour
{
    public List<GameObject> Ennemies = new List<GameObject>();

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<NewEnnemiMovement>().isNotDrone == false /*&& other.gameObject.GetComponent<NewEnnemiMovement>().isAdd == false*/)
            {
                //je l'ajoute à un tableau.  La Bool "isAdd" permet de bloquer un sur Ajout.
                Ennemies.Add(other.gameObject);
                //other.gameObject.GetComponent<NewEnnemiMovement>().isAdd = true;
                Debug.Log("un drone est entré");
            }
        }

        //Dès qu'un drone passe dans le trigger

        if (Ennemies.Count > 0)
        {
            GameManager.Instance.underAttack = true;
        }
        else
        {
            GameManager.Instance.underAttack = false;
        }

    }
}
