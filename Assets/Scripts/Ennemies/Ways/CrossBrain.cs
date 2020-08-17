using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Ennemies;
public class CrossBrain : MonoBehaviour
{

    public Transform crosspointGauche;
    public Transform crosspointDroit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            collision.gameObject.GetComponent<EnnemiesMovement>().parentRef = gameObject;

        }
    }

}
