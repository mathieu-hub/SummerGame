using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera shortCam;
    public CinemachineVirtualCamera largeCam;
    bool shortCamIsActivate = true;


    // Transition de caméra 
    private void Update()
    {
        if(shortCamIsActivate == true && Input.GetButtonDown("Y_Button"))
        {
            shortCam.gameObject.SetActive(false);
            largeCam.gameObject.SetActive(true);
            shortCamIsActivate = false;
        }

        if (shortCamIsActivate == false && Input.GetButtonDown("Y_Button"))
        {
            largeCam.gameObject.SetActive(false);
            shortCam.gameObject.SetActive(true);
            shortCamIsActivate = true;
        }
    }  
}
