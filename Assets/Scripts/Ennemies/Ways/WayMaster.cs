using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class WayMaster : MonoBehaviour
{
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

    void Start()
    {
        way01 = way1.GetComponent<Waypoints>().points;
        way02 = way2.GetComponent<Waypoints>().points;
        way03 = way3.GetComponent<Waypoints>().points;
        way04 = way4.GetComponent<Waypoints>().points;
        way05 = way5.GetComponent<Waypoints>().points;
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
        }

        if (WaveSpawner.waveIndex == 3)
        {
            way3.SetActive(true);
            numberOfWay = 3;

        }

        if (WaveSpawner.waveIndex == 4)
        {
            way4.SetActive(true);
            numberOfWay = 4;

        }

        if (WaveSpawner.waveIndex == 5)
        {
            way5.SetActive(true);
            numberOfWay = 5;
        }
    }

}

