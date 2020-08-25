using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class RessourcesPickup : MonoBehaviour
{
    public enum PickupObject { scraps, planDeRecherches }
    public PickupObject currentObject;
    public int ressourceQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerController"))
        {
            if (currentObject == PickupObject.scraps)
            {
                GameManager.Instance.scrapsCount += 1;


            }
            else if (currentObject == PickupObject.planDeRecherches)
            {
                GameManager.Instance.plansCount += 1;
            }

            Destroy(gameObject);
            
        }
    }
}

