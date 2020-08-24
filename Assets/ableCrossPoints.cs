using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class ableCrossPoints : MonoBehaviour
{
    [Header("Way1Cross")]
    [SerializeField] private GameObject[] way1Cross;

    [Header("Way2Cross")]
    [SerializeField] private GameObject[] way2Cross;

    [Header("Way3Cross")]
    [SerializeField] private GameObject[] way3Cross;

    [Header("Way4Cross")]
    [SerializeField] private GameObject[] way4Cross;

    // Start is called before the first frame update
    void Start()
    {
        //Tous les Crosspoints doivent avoir cantCross en true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Instance.WayMaster.numberOfWay == 2)
        {
            for (int i = 0; i < way1Cross.Length; i++)
            {
                way1Cross[i].GetComponent<Crosspoints>().cantCross = false;
            }
        }

        if (GameMaster.Instance.WayMaster.numberOfWay == 3)
        {
            for (int i = 0; i < way2Cross.Length; i++)
            {
                way2Cross[i].GetComponent<Crosspoints>().cantCross = false;
            }
        }

        if (GameMaster.Instance.WayMaster.numberOfWay == 4)
        {
            for (int i = 0; i < way3Cross.Length; i++)
            {
                way3Cross[i].GetComponent<Crosspoints>().cantCross = false;
            }
        }

        if (GameMaster.Instance.WayMaster.numberOfWay == 5)
        {
            for (int i = 0; i < way4Cross.Length; i++)
            {
                way4Cross[i].GetComponent<Crosspoints>().cantCross = false;
            }
        }


    }
}
