using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("Cancel")]
    [SerializeField] private Image cancelCircle;
    public float cancelTime;
    public bool canCancel = false;

    private void Start()
    {
        startcolor = leftArrowInt.color;

        leftArrowExt.enabled = false;
        leftArrowInt.enabled = false;

        text[0] = "In this game you play as an alien named Bolb." +  " He lives peacefully in the company of his animals and vegetables.";
        text[1] = "However, one day, the ORS (Organisation of Space Research) discovered Bolb’s farm on a Saturn's satellite.";
        text[2] = "Bolb does not like to be disturbed. To keep his existence secret, Bolb will have to use his plants to defend himself.";
        text[3] = "Nevertheless the production of Bolb cannot stop facing these waves of assailants. Three resources will allow Bolb to build a worthy defense.";
        text[4] = "The ORS will not be doing any research and will be able to call for reinforcements. However, Bolb, is not alone. He can count on his friend Flint, the salesman.";
        text[5] = "Help Bolb keep his life peaceful.";

    }

    // Update is called once per frame
    void Update()
    {

        cancelCircle.fillAmount = cancelTime / 100f;

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



        //Bon
        ImageView.sprite = images[currentState].GetComponent<Image>().sprite; ;


        textView.text = text[currentState];

        Debug.Log(ImageView.sprite);


        if(Input.GetAxisRaw("Left_Joystick_X") > 0.15 && !changing && !canCancel)
        {
            changing = true;
            Modifier = 1;

            rightArrowInt.color = Color.black;

        }
        if (Input.GetAxisRaw("Left_Joystick_X") < -0.15 && !changing && !canCancel)
        {
            changing = true;
            Modifier = -1;

            leftArrowInt.color = Color.black;

        }

        if (Input.GetAxisRaw("Left_Joystick_X") <= 0.15 && Input.GetAxisRaw("Left_Joystick_X") >= -0.15 && Modifier !=0)
        {
            Change();
        }

        if(Input.GetButtonDown("B_Button") && changing == false)
        {
            canCancel = true;
            

            
        }

        if (canCancel)
        {
            cancelTime += 0.1f;
        }


        if (Input.GetButtonUp("B_Button"))
        {
            canCancel = false;
            cancelTime = 0f;
        }

        if(cancelTime >= 100)
        {
            cancelTime = 0;
            currentState = 0;
            SceneManager.LoadScene(0);
        }

        
    }

    void Change()
    {

        rightArrowInt.color = startcolor;
        leftArrowInt.color = startcolor;

        changing = false;
        needToChange = false;

        if (currentState != images.Length - 1)
        {
            currentState += Modifier;
        }

        if (currentState == images.Length - 1 && Modifier == -1)
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
