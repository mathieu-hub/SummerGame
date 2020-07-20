using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    //this script makes the player's UI  update

    public class PlayerUpdateUi : MonoBehaviour
    {

        [SerializeField] private Image attackbar;
        [SerializeField] private Image vegetable1;
        [SerializeField] private Image vegetable2;
        [SerializeField] private Image vegetable3;


        // Start is called before the first frame update
        void Start()
        {
            vegetable1.enabled = false;
            vegetable2.enabled = false;
            vegetable3.enabled = false;

        }

        // Update is called once per frame
        void Update()
        {
            attackbar.fillAmount = (float) PlayerManager.Instance.attack.loadingTime / (float) PlayerManager.Instance.attack.maxLoadingTime;

            if (PlayerManager.Instance.attack.isLoadingAttack)
            {
                if (PlayerManager.Instance.attack.numberOfVegetablesEat == 1)
                {
                    vegetable1.enabled = true;
                }
                if (PlayerManager.Instance.attack.numberOfVegetablesEat == 2)
                {
                    vegetable2.enabled = true;
                }
                if (PlayerManager.Instance.attack.numberOfVegetablesEat == 3)
                {
                    vegetable3.enabled = true;
                }
            }
            else
            {
                vegetable1.enabled = false;
                vegetable2.enabled = false;
                vegetable3.enabled = false;
            }

            
        }
    }
}

