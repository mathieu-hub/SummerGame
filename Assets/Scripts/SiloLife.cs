using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class SiloLife : MonoBehaviour
{
    public static int lives;
    public int startLives = 100;

    void Start()
    {
        lives = startLives;
    }

    void Update()
    {
        if(lives <= 0)
        {
            EndGame();
        }

        void EndGame()
        {
            Debug.Log("Game Over");
        }
    }
}

