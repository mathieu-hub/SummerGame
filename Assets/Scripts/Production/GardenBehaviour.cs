using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField]
    private Image durationBar;
    [SerializeField]
    private Image durationBarBackground;
    public TextMeshProUGUI storedCount;

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
        UpdateUi();

        if (timer.finished)
        {
            storedVegetable += 1;
            timer = new Clock(duration);
        }

        if(storedVegetable == 6)
        {
            timer.Pause();
        }
        else
        {
            print(timer.time);
        }


    }

    void UpdateUi()
    {
        


        storedCount.text = storedVegetable.ToString();
        durationBar.fillAmount = (float)timer.time / (float)duration;

        if (storedVegetable == 6)
        {
            //changer couleur de la bar

        }
    }
}
