using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class PodAnimator : MonoBehaviour
	{
		#region Variables
		public Animator animator;
		#endregion

		// Start is called before the first frame update
		void Start()
		{
			animator.SetBool("isAtteris", true);
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}
}

