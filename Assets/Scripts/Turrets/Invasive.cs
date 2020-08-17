using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret
{
	public class Invasive : Turret
	{
        #region Variables

        #endregion

        public override void Upgrade(int newRange, int newDamage, int newFireRate)
        {
            base.Upgrade(newRange, newDamage, newFireRate);
            //Set active child turret
        }
    }
}

