using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickup : MonoBehaviour
{
    public int scrapQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {

        }
    }
}

