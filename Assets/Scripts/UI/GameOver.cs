using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ennemies;
using SuperManagement;
using AudioManager;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        roundsText.text = WaveSpawner.rounds.ToString();


        SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().clip = SingletonAudioSource.Instance.soundmanager.sounds[25].clip;
        SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().Play();
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

