using UnityEngine;
using Player;
using UnityEngine.UI;
using AudioManager;

namespace Management
{
    public class GamePause : MonoBehaviour
    {

        [SerializeField] private Image pauseMenu;
        public Slider pauseSliderVolume;


        private void Start()
        {
            pauseSliderVolume.value = SingletonAudioSource.Instance.soundmanager.volumeManager;
            pauseSliderVolume.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == false)
            {
                EnterInPauseMenu();
                GameManager.Instance.inPause = true;
                pauseSliderVolume.enabled = true;
                //Son
            }
            else if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == true)
            {
                LeavingPauseMenu();
                GameManager.Instance.inPause = false;
                pauseSliderVolume.enabled = false;

            }

            SingletonAudioSource.Instance.soundmanager.volumeManager = pauseSliderVolume.value;


        }

        void EnterInPauseMenu()
        {
            pauseMenu.enabled = true;
            PlayerManager.Instance.controller.needToStop = true;
            Time.timeScale = 0;

            SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().clip = SingletonAudioSource.Instance.soundmanager.sounds[27].clip;
            SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().Play();

        }

        void LeavingPauseMenu()
        {
            pauseMenu.enabled = false;
            PlayerManager.Instance.controller.needToStop = false;
            Time.timeScale = 1;

            SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().clip = SingletonAudioSource.Instance.soundmanager.sounds[29].clip;
            SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().Play();
        }
    }
}

