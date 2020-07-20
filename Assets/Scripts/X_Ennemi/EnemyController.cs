using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int healthPoint = 10;
   
    // Update is called once per frame
    public int TakeDamages
    {
        set
        {
            healthPoint -= value;
            UpdateHp();
        }
    }

    void UpdateHp()
    {
        if (healthPoint <= 0) {
            Destroy(gameObject);
        }
    }

}
