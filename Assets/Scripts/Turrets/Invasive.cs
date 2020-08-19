using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	public class Invasive : TurretBehaviour
	{
        #region Variables
        [SerializeField] private TurretParent turretParent = null;
        
        [SerializeField] private GameObject[] childTurrets = new GameObject[2];
        //private TurretBehaviour[] childTurretsBehaviour = new TurretBehaviour[2];
        #endregion

        //Awake
        //GetComponent<TurretBehaviour>() for each childTurret

        public override void Upgrade(int newRange, int newDamage, int newFireRate)
        {
            base.Upgrade(newRange, newDamage, newFireRate);
            
            if(turretParent.currentLevel < 3)
            {
                childTurrets[turretParent.currentLevel - 1].SetActive(true);
            }

            //switch (turretParent.currentLevel)
            //{
            //    case 1:
            //        childTurrets[turretParent.currentLevel - 1].SetActive(true);
            //        break;

            //    case 2:
            //        childTurrets[turretParent.currentLevel - 1].SetActive(true);

            //        foreach(TurretBehaviour childTurret in childTurretsBehaviour)
            //        {
            //            childTurret.Upgrade(nRange, nDamage, nFireRate);
            //        }
            //        break;

            //    default:
            //        break;
            //}
        }
    }
}

