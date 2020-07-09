using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBehaviour : MonoBehaviour
{
    /// <summary>
    /// XP_This script makes the bahaviour of the Garden (Clock + Update Vegetable)
    /// </summary>

    #region Variables

    public static Clock timer;
    [Header("Clock")]
    [Range(0.0F, 15.0F)]
    public float duration;

    [Header("Storage")]
    public float storedVegetable;
    #endregion

    void Start()
    {
        timer = new Clock(duration);
    }

    // Update is called once per frame
    void Update()
    {
        print(timer.time);

        if (timer.finished)
        {
            storedVegetable += 1;
            timer = new Clock(duration);
        }

        if(storedVegetable == 6)
        {
            timer.Pause();
        }
    }
}
