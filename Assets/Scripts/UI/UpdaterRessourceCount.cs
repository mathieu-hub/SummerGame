using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using TMPro;

namespace UI
{
    public class UpdaterRessourceCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI purinsCount;
        [SerializeField] private TextMeshProUGUI vegetablesCount;
        [SerializeField] private TextMeshProUGUI scrapsCount;
        [SerializeField] private TextMeshProUGUI plansCount;

        private void Update()
        {
            purinsCount.text = GameManager.Instance.purinCount.ToString();
            vegetablesCount.text = GameManager.Instance.vegetablesCount.ToString();
            scrapsCount.text = GameManager.Instance.scrapsCount.ToString();
            plansCount.text = GameManager.Instance.plansCount.ToString();
        }
    }
}

