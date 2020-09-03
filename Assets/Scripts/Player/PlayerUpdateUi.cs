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
      
        [SerializeField] private Color stockColor;


        // Start is called before the first frame update
        void Start()
        {
            stockColor = attackbar.color;

           

        }

        // Update is called once per frame
        void Update()
        {
            attackbar.fillAmount = (float) PlayerManager.Instance.attack.loadingTime / (float) PlayerManager.Instance.attack.maxLoadingTime;

            


            if (PlayerManager.Instance.attack.isAttacking && PlayerManager.Instance.attack.needToCharge == false)
            {
                attackbar.color = Color.red;
            }
            else
            {
                attackbar.color = stockColor;
            }

        }
    }
}

