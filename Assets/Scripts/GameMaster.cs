using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class GameMaster : Singleton<GameMaster>
{
    public WayMaster WayMaster = null;
    public GameObject rempartPrefab;
    private void Awake()
    {
        MakeSingleton(true);
    }
}

