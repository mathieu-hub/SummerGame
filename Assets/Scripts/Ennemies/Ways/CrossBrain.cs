using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Ennemies;
public class CrossBrain : MonoBehaviour
{

    public Transform crosspointGauche;
    public Transform crosspointDroit;

    public GameObject theRempart;
    public bool rempartDead;

    public bool rempart = false;
    public bool SummonRempart = false;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            collision.gameObject.GetComponent<NewEnnemiMovement>().parentRef = gameObject;

            collision.gameObject.GetComponent<NewEnnemiMovement>().UpdateParent();
            collision.gameObject.GetComponent<NewEnnemiMovement>().NeedToCheck();
        }
    }

    private void Update()
    {
        if(!rempart && SummonRempart)
        {
            theRempart = Instantiate(GameMaster.Instance.rempartPrefab, transform.position, Quaternion.identity);
            SummonRempart = false;
            rempart = true;
        }

        if(rempart && theRempart.GetComponent<RempartTest>().currentHealth <= 0)
        {
            theRempart = null;
            rempartDead = true;
            rempart = false;
        }
    }

}
