using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class blokerPlayer : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            PlayerManager.Instance.controller.needToStop = true;
            PlayerManager.Instance.controller.needToStop = false;
        }
    }
}
