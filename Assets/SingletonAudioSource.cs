using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using AudioManager;


namespace AudioManager
{
    public class SingletonAudioSource : Singleton<SingletonAudioSource>
    {

        public SoundManager soundmanager = null;

        

        private void Awake()
        {
            MakeSingleton(true);
            DontDestroyOnLoad(gameObject);
        }

        

    }

}

