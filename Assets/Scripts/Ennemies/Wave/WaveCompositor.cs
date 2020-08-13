using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    [System.Serializable]
	public class WaveCompositor 
	{
        public GameObject[] ennemy;
        public int count;
        public float rate;
        [Header("Max Ennemy Type")]        
        public int maxEnnemy01;
        public int maxEnnemy02;
        public int maxEnnemy03;
        public int maxEnnemy04;
        public int maxEnnemy05;        
    }
}

