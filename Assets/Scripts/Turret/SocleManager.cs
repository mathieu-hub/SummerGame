using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocleManager : MonoBehaviour
{
    public GameObject[] Turret;

    // Start is called before the first frame update
    void Start()
    {
        Turret[0] = Resources.Load("Prefabs/Stroot") as GameObject;
        Turret[1] = Resources.Load("Prefabs/Bourlo") as GameObject;
        Turret[2] = Resources.Load("Prefabs/Snipic") as GameObject;
        Turret[3] = Resources.Load("Prefabs/Tronçoronce") as GameObject;
        Turret[4] = Resources.Load("Prefabs/Invasive Variant") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
