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
        public int vegetablesCost;
        public int planCost;
        public int scrapCost;
        public int range;
        public int damage;
        public int fireRate;

        [Header("Investissement")]
        public int scrapUsedIn;
        public int purinUsedIn;

        [Header("HealthPoint")]
        public int currentHp;
        [Range(1,100)]
        public int maxHp;
        #endregion

        private void Start()
        {
            currentHp = maxHp;
            purinUsedIn = vegetablesCost;
            scrapUsedIn = scrapCost;
            turretName = gameObject.name;
        }
    }
}

