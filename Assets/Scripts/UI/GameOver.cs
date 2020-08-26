using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ennemies;
using SuperManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    private void OnEnable()
    {
        roundsText.text = WaveSpawner.rounds.ToString();
    }

    public void Retry()
    {
        SuperGameManager.Instance.Reset();
        SceneManager.LoadScene(1);
        
    }

    public void Menu()
    {
        SuperGameManager.Instance.Reset();
        SceneManager.LoadScene(0);
        
    }
}

