using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera shortCam;
    public CinemachineVirtualCamera largeCam;
    bool shortCamIsActivate = true;


    // Transition de caméra 
    void Update()
    {
        if(shortCamIsActivate == true && Input.GetButtonDown("Y_Button"))
        {
            shortCamIsActivate = false;            
            shortCam.gameObject.SetActive(false);
            largeCam.gameObject.SetActive(true);
        }

        //if (largeCam. && Input.GetButtonDown("Y_Button"))
    }  
}
