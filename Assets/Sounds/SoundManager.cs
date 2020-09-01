using UnityEngine.Audio;
using UnityEngine;
using System;
using Management;

namespace AudioManager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public Sounds[] sounds;

        private void Awake()
        {
           

            MakeSingleton(true);

            foreach (Sounds s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;

                s.source.volume = GameManager.Instance.volume;

                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
            }


        }


        public void Play(string name)
        {
            Sounds s = Array.Find(sounds, sound => sound.name == name);

            if(s == null)
            {
                Debug.LogWarning("Sound: " + name + "not found!");
                return;
            }

            s.source.Play();

        }
    }

}
