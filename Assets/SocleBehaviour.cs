using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tower
{
    /// <summary>
    /// This Script makes appeat the Ui for puting Turret on the battlefield
    /// </summary>
    public class SocleBehaviour : MonoBehaviour
    {
        #region
        [Header("Variables")]
        [SerializeField] private GameObject UI;
        
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            UI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

