using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class GameMaster : Singleton<GameMaster>
{
    public WayMaster WayMaster = null;

    private void Awake()
    {
        MakeSingleton(true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

