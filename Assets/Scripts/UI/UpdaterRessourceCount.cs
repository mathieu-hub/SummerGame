using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using TMPro;

namespace UI
{/// <summary>
/// XP - This script update the UI Counts
/// </summary>
    public class UpdaterRessourceCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI purinsCount;
        [SerializeField] private TextMeshProUGUI vegetablesCount;
        [SerializeField] private TextMeshProUGUI scrapsCount;
        [SerializeField] private TextMeshProUGUI plansCount;

        [SerializeField] private TextMeshProUGUI currentWeight;
        [SerializeField] private TextMeshProUGUI maxWeight;

        private void Update()
        {
            currentWeight.text = GameManager.Instance.totalAnimalWeight.ToString() + "/";
            maxWeight.text = GameManager.Instance.maxStoredUnits.ToString();
            purinsCount.text = GameManager.Instance.purinCount.ToString();
            vegetablesCount.text = GameManager.Instance.vegetablesCount.ToString();
            scrapsCount.text = GameManager.Instance.scrapsCount.ToString();
            plansCount.text = GameManager.Instance.plansCount.ToString();
        }
    }
}

