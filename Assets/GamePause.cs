using UnityEngine;
using Player;
using UnityEngine.UI;
using AudioManager;

namespace Management
{
    public class GamePause : MonoBehaviour
    {

        [SerializeField] private Image pauseMenu;
        public GameObject pauseSliderVolume;


        private void Start()
        {
            pauseSliderVolume.GetComponent<Slider>().value = SingletonAudioSource.Instance.soundmanager.volumeManager;
            pauseSliderVolume.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == false)
            {
                EnterInPauseMenu();
                GameManager.Instance.inPause = true;
                pauseSliderVolume.SetActive(true);
                //Son
            }
            else if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == true)
            {
                LeavingPauseMenu();
                GameManager.Instance.inPause = false;
                pauseSliderVolume.SetActive(false);

            }

            SingletonAudioSource.Instance.soundmanager.volumeManager = pauseSliderVolume.GetComponent<Slider>().value;

            SingletonAudioSource.Instance.soundmanager.GetComponent<AudioSource>().volume = pauseSliderVolume.GetComponent<Slider>().value;


        }

        void EnterInPauseMenu()
        {
            pauseMenu.enabled = true;
            PlayerManager.Instance.controller.needToStop = true;
            Time.timeScale = 0;

            SingletonAudioSource.Instance.soundmanager.setValues(SingletonAudioSource.Instance.GetComponent<AudioSource>(), 27);
            SingletonAudioSource.Instance.GetComponent<AudioSource>().Play();

        }

        void LeavingPauseMenu()
        {
            pauseMenu.enabled = false;
            PlayerManager.Instance.controller.needToStop = false;
            Time.timeScale = 1;

            SingletonAudioSource.Instance.soundmanager.setValues(SingletonAudioSource.Instance.GetComponent<AudioSource>(), 29);
            SingletonAudioSource.Instance.GetComponent<AudioSource>().Play();
        }
    }
}

