using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class WayMaster : MonoBehaviour
{
    //WAYS
    [Header("WAYS")]
    public int numberOfWay = 1;
    
    public Transform[] way01;
    public Transform[] way02;
    public Transform[] way03;
    public Transform[] way04;
    public Transform[] way05;

    public GameObject way1;
    public GameObject way2;
    public GameObject way3;
    public GameObject way4;
    public GameObject way5;

    //CROSSWAYS
    [Header("CROSSWAYS")]
    public int numberOfCrossWay = 1;

    public Transform[] Crossway01;
    public Transform[] Crossway02;
    public Transform[] Crossway03;
    public Transform[] Crossway04;



    public GameObject Crossway1;
    public GameObject Crossway2;
    public GameObject Crossway3;
    public GameObject Crossway4;





    void Start()
    {
        way01 = way1.GetComponent<Waypoints>().points;
        way02 = way2.GetComponent<Waypoints>().points;
        way03 = way3.GetComponent<Waypoints>().points;
        way04 = way4.GetComponent<Waypoints>().points;
        way05 = way5.GetComponent<Waypoints>().points;

        Crossway01 = Crossway1.GetComponent<Waypoints>().points;
        Crossway02 = Crossway2.GetComponent<Waypoints>().points;
        Crossway03 = Crossway3.GetComponent<Waypoints>().points;
        Crossway04 = Crossway4.GetComponent<Waypoints>().points;




    }

    private void Update()
    {
        if (WaveSpawner.waveIndex == 0)
        {
            way2.SetActive(false);
            way3.SetActive(false);
            way4.SetActive(false);
            way5.SetActive(false);

           
            
        }

        if (WaveSpawner.waveIndex == 2)
        {
            way2.SetActive(true);
           
            numberOfWay = 2;
            numberOfCrossWay = 1; 
        }

        if (WaveSpawner.waveIndex == 3)
        {
            way3.SetActive(true); 
            numberOfWay = 3;
            numberOfCrossWay = 2;
        }

        if (WaveSpawner.waveIndex == 4)
        {
            way4.SetActive(true);
           
            numberOfWay = 4;
            numberOfCrossWay = 3;
        }

        if (WaveSpawner.waveIndex == 5)
        {
            way5.SetActive(true);            
            numberOfWay = 5;
            numberOfCrossWay = 4;
        }
    }

}

