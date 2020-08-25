using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;
using Management;
public class Crosspoints : MonoBehaviour
{
    public bool isOpen = false;

    public bool cantCross = false;

    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOpen)
        {
            spriteRend.enabled = true;
        }
        else
        {
            spriteRend.enabled = false;
        }
    }
}

