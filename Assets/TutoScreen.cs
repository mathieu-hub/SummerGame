using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoScreen : MonoBehaviour
{

    [SerializeField] private string[] text;
    [SerializeField] private Image[] images;

    [SerializeField] private Image ImageView;
    [SerializeField] private TextMeshProUGUI textView;

    [SerializeField] private int currentState;
    [SerializeField] private int Modifier;
    [SerializeField] private bool needToChange = true;
    [SerializeField] private bool changing = false;

    [SerializeField] private Image leftArrowInt;
    [SerializeField] private Image rightArrowInt;

    [SerializeField] private Image leftArrowExt;
    [SerializeField] private Image rightArrowExt;

    private Color startcolor;

    private void Start()
    {
        startcolor = leftArrowInt.color;

        leftArrowExt.enabled = false;
        leftArrowInt.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(currentState == 0)
        {
            leftArrowExt.enabled = false;
            leftArrowInt.enabled = false;
        }
        else
        {
            leftArrowExt.enabled = true;
            leftArrowInt.enabled = true;
        }

        if(currentState == images.Length-1)
        {
            rightArrowExt.enabled = false;
            rightArrowInt.enabled = false;

        }
        else
        {
            rightArrowExt.enabled = true;
            rightArrowInt.enabled = true;
        }



        ImageView = images[currentState];
        textView.text = text[currentState];


        if(Input.GetAxisRaw("Left_Joystick_X") > 0.15 && !changing)
        {
            changing = true;
            Modifier = 1;

            rightArrowInt.color = Color.black;

        }
        if (Input.GetAxisRaw("Left_Joystick_X") < -0.15 && !changing)
        {
            changing = true;
            Modifier = -1;

            leftArrowInt.color = Color.black;

        }

        if (Input.GetAxisRaw("Left_Joystick_X") <= 0.15 && Input.GetAxisRaw("Left_Joystick_X") >= -0.15 && Modifier !=0)
        {
            Change();
        }

        void Change()
        {

            rightArrowInt.color = startcolor;
            leftArrowInt.color = startcolor;

            changing = false;
            needToChange = false;

            if(currentState != images.Length-1)
            {
                currentState += Modifier;
            }

            if(currentState == images.Length-1 && Modifier == -1)
            {
                currentState += Modifier;
            }

            Modifier = 0;

           

            if (currentState < 0)
            {
                currentState = 0;
            }
        }
    }
}
