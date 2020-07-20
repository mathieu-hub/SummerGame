using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInButton : MonoBehaviour
{
    //Button reference
    public GameObject AButton;
    private SpriteRenderer boutonRenderer;

   [HideInInspector] public bool playerHe = false;

    void Start()
    {
        playerHe = false;

        boutonRenderer = AButton.GetComponent<SpriteRenderer>();

        Color c = boutonRenderer.material.color;
        c.a = 0f;
        boutonRenderer.material.color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerHe = true;
            startFadingIN();
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            playerHe = false;
            startFadingOUT();
           
        }
    }

    public void startFadingIN()
    {
        StartCoroutine("FadeIn");
    }

    public void startFadingOUT()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.25f; f <= 1.1; f += 0.25f)
        {
            Color c = boutonRenderer.material.color;
            c.a = f;
            boutonRenderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

    }
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.1f)
        {
            Color c = boutonRenderer.material.color;
            c.a = f;
            boutonRenderer.material.color = c;
            yield return new WaitForSeconds(0.01f);
        }


    }
}
