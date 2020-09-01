using UnityEngine.Audio;
using UnityEngine;
using System;
using Management;

namespace AudioManager
{
    public class SoundManager : MonoBehaviour
    {
        public Sounds[] sounds;
        [Range(0.01f, 1f)]
        public float volumeManager;

        private void Start()
        {

            foreach (Sounds s in sounds)
            {

                s.volume = volumeManager;

            }


        }

        public void SetVolume()
        {
            foreach (Sounds s in sounds)
            {

                s.source.volume = GameManager.Instance.volume;

            }

        }

        public void setValues(AudioSource audioSource, int index)
        {
            audioSource.clip = sounds[index].clip;
            audioSource.volume = sounds[index].volume;
            audioSource.loop = sounds[index].loop;
            audioSource.volume = sounds[index].volume;
        }

       
    }

}
