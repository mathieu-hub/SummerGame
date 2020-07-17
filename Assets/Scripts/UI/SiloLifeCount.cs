using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SiloLifeCount : MonoBehaviour
{
    public Text siloLivesText;


    // Update is called once per frame
    void Update()
    {
        siloLivesText.text = SiloLife.lives + " LIVES";
    }
}

