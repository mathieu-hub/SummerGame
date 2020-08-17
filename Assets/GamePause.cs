using UnityEngine;
using Player;
using UnityEngine.UI;

namespace Management
{
    public class GamePause : MonoBehaviour
    {

        [SerializeField] private Image pauseMenu;

       
        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == false)
            {
                EnterInPauseMenu();
                GameManager.Instance.inPause = true;
                //Son
            }
            else if (Input.GetButtonDown("Start_Button") && GameManager.Instance.inPause == true)
            {
                LeavingPauseMenu();
                GameManager.Instance.inPause = false;

            }


        }

        void EnterInPauseMenu()
        {
            pauseMenu.enabled = true;
            PlayerManager.Instance.controller.needToStop = true;
            Time.timeScale = 0;
            
        }

        void LeavingPauseMenu()
        {
            pauseMenu.enabled = false;
            PlayerManager.Instance.controller.needToStop = false;
            Time.timeScale = 1;
        }
    }
}

