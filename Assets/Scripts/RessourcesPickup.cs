using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using AudioManager;

public class RessourcesPickup : MonoBehaviour
{
    public enum PickupObject { scraps, planDeRecherches }
    public PickupObject currentObject;
    public int ressourceQuantity;

    private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerController"))
        {
            if (currentObject == PickupObject.scraps)
            {
                GameManager.Instance.scrapsCount += 1;
                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 31);
                audioSource.Play();

            }
            else if (currentObject == PickupObject.planDeRecherches)
            {
                GameManager.Instance.plansCount += 1;
                SingletonAudioSource.Instance.soundmanager.setValues(audioSource, 31);
                audioSource.Play();
            }

            Destroy(gameObject);
            
        }
    }
}

