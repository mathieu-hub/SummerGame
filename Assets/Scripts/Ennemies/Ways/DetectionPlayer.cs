using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPlayer : MonoBehaviour
{
    public bool playerInBase = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerInBase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerInBase = false;
        }
        
    }
}

