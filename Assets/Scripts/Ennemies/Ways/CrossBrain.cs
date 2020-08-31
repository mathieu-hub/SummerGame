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

    public bool isSiloPoint = false;

    public GameObject leftSocle = null;
    public GameObject rightSocle = null;
    public GameObject leftTurret = null;
    public GameObject rightTurret = null;

    public GameObject summonPosition;

    public GameObject theRempart = null;
    public bool rempartDead;

    public bool rempart = false;
    public bool SummonRempart = false;

    private void Start()
    {
        theRempart = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Trigger");
            if(collision.gameObject.GetComponent<EnnemiesHealth>().typeOfEnnemy != EnnemiesHealth.TypeOfEnnemy.Drone )
            {
                collision.gameObject.GetComponent<NewEnnemiMovement>().parentRef = gameObject;
                collision.gameObject.GetComponent<NewEnnemiMovement>().UpdateParent();
                Debug.Log("UpdateParentCross");
                collision.gameObject.GetComponent<NewEnnemiMovement>().NeedToCheck();
            }
        }
           
    }

    private void Update()
    {
        if (!isSiloPoint && leftSocle.GetComponent<SocleBehaviour>().turretSummoned)
        {
            leftTurret = leftSocle.GetComponent<SocleBehaviour>().turretSummon;
        }
        else
        {
            leftTurret = null;
        }

        if (!isSiloPoint && rightSocle.GetComponent<SocleBehaviour>().turretSummoned)
        {
            leftTurret = leftSocle.GetComponent<SocleBehaviour>().turretSummon;
        }
        else
        {
            rightTurret = null;
        }

        if (!isSiloPoint)
        {
            rightTurret = rightSocle.GetComponent<SocleBehaviour>().turretSummon;
        }
        

        if (SummonRempart && !rempartDead)
        {
            Summon();
        }
    }

    public void Dead()
    {
        gameObject.GetComponentInChildren<RempartBrain>().enabled = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        theRempart = null;
        rempartDead = true;
        rempart = false;
        SummonRempart = true;
    }

    public void Summon()
    {
        gameObject.GetComponent<RempartBrain>().validationTime = 0;
        theRempart = Instantiate(GameMaster.Instance.rempartPrefab, summonPosition.transform.position, Quaternion.identity);
        theRempart.GetComponent<RempartTest>().refParent = gameObject;
        //gameObject.GetComponentInChildren<CrossBrain>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        SummonRempart = false;
        rempart = true;
    }


}
