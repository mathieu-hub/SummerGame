using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class DrawSpawnPoint : MonoBehaviour
	{
		#region Variables
		
		#endregion

        private void OnDrawGizmos()
        {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 0.2f);
		}
    }
}

