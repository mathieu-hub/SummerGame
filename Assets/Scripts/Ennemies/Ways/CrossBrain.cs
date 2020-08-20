using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Ennemies;
using Turret;
public class CrossBrain : MonoBehaviour
{

    public GameObject crosspointGauche;
    public GameObject crosspointDroit;


    public GameObject leftSocle;
    public GameObject rightSocle;
    //public GameObject leftTurret = null;
    //public GameObject rightTurret = null;

    public GameObject summonPosition;

    public GameObject theRempart;
    public bool rempartDead;

    public bool rempart = false;
    public bool SummonRempart = false;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Trigger");
            collision.gameObject.GetComponent<NewEnnemiMovement>().parentRef = gameObject;

            collision.gameObject.GetComponent<NewEnnemiMovement>().UpdateParent();
            Debug.Log("UpdateParentCross");
            collision.gameObject.GetComponent<NewEnnemiMovement>().NeedToCheck();
        }
    }

    private void Update()
    {
        if(!rempart && SummonRempart)
        {
            theRempart = Instantiate(GameMaster.Instance.rempartPrefab, summonPosition.transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<RempartBrain>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            SummonRempart = false;
            rempart = true;
        }

        if(rempart && theRempart.GetComponent<RempartTest>().currentHealth <= 0)
        {
            gameObject.GetComponentInChildren<RempartBrain>().enabled = true;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            theRempart = null;
            rempartDead = true;
            rempart = false;
        }


        //leftTurret = leftSocle.GetComponent<SocleBehaviour>().turretSummon;
        //rightTurret = rightSocle.GetComponent<SocleBehaviour>().turretSummon;
    }

}
