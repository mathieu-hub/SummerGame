using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SiloLifeCount : MonoBehaviour
{
    public TextMeshProUGUI siloLivesText;


    // Update is called once per frame
    void Update()
    {
        siloLivesText.text = SiloLife.lives.ToString();
    }
}

