using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera shortCam;
    public CinemachineVirtualCamera largeCam;
    bool shortCamIsActivate = true;


    // Transition de caméra          
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shortCam.gameObject.SetActive(false);
            largeCam.gameObject.SetActive(true);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shortCam.gameObject.SetActive(true);
            largeCam.gameObject.SetActive(false);
        }
    }
}
