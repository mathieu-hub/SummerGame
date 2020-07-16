using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CamScale : MonoBehaviour
{
    public CinemachineVirtualCamera shortCam;
    public CinemachineVirtualCamera largeCam;
    public int camMode;

    void Update()
    {
        if (Input.GetButtonDown("Y_Button"))
        {
            if (camMode == 1)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }
            StartCoroutine(CameraChange());
        }
    }

    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            shortCam.gameObject.SetActive(true);
            largeCam.gameObject.SetActive(false);
        }
        if (camMode == 1)
        {
            shortCam.gameObject.SetActive(false);
            largeCam.gameObject.SetActive(true);
        }
    }


}

