using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    /// <summary>
    /// Need this script on each towers for socle behaviour
    /// </summary>
    public class TurretParent : MonoBehaviour
    {
        #region
        public string turretName;
        public int cost;
        public int range;
        public int damage;
        public int fireRate;
        #endregion

        private void Start()
        {
            turretName = gameObject.name;
        }
    }
}

