using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInButton : MonoBehaviour
{
    //Button reference
    public GameObject AButton;
    private SpriteRenderer boutonRenderer;

    public bool playerHe = false;
    public bool APressed = false;
    private bool needToVanish = false;

    void Start()
    {
        playerHe = false;

        boutonRenderer = AButton.GetComponent<SpriteRenderer>();

        AButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("A_Button") && !needToVanish)
        {
            needToVanish = true;
            AButton.SetActive(true);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerHe = true;
            AButton.SetActive(true);
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerHe = false;
            APressed = false;

        }
    }

   
}
