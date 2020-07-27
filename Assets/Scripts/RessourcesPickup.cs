﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesPickup : MonoBehaviour
{
    public enum PickupObject { scraps, planDeRecherches }
    public PickupObject currentObject;
    public int ressourceQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (currentObject == PickupObject.scraps)
            {
                PlayerRessourcesTEST.playerRessourcesTEST.scraps += ressourceQuantity;
            }

            if (currentObject == PickupObject.planDeRecherches)
            {
                PlayerRessourcesTEST.playerRessourcesTEST.planDeRecherches += ressourceQuantity;
            }
        }
    }
}

