using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class SiloLife : MonoBehaviour
{
    public static int lives;
    public int startLives = 10;

    public static bool gameIsOver;

    public GameObject gameOverUi;

    void Start()
    {
        gameIsOver = false;
        lives = startLives;
    }

    void Update()
    {
        // à enlever, juste pour test le game over
        if (Input.GetKeyDown(KeyCode.L))
        {
            EndGame();
        }

        if (gameIsOver)
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
        Time.timeScale = 0f; //met le jeu en pause
        gameIsOver = true;
        gameOverUi.SetActive(true);
    }
}

