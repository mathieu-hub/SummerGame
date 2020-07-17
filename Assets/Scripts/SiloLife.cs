using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class SiloLife : MonoBehaviour
{
    public static int lives;
    public int startLives = 100;

    private bool gameEnded = false;

    void Start()
    {
        lives = startLives;
    }

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if(lives <= 0)
        {
            EndGame();
        }        
    }
    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over");
    }
}

