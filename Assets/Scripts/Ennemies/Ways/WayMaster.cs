﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemies;

public class WayMaster : MonoBehaviour
{
    public Transform[] way01;
    public Transform[] way02;
    public Transform[] way03;
    public Transform[] way04;
    public Transform[] way05;

    public static GameObject way1;
    public static GameObject way2;
    public static GameObject way3;
    public static GameObject way4;
    public static GameObject way5;

    void Start()
    {
        way01 = way1.GetComponent<Waypoints>().points;
        way02 = way2.GetComponent<Waypoints>().points;
        way03 = way3.GetComponent<Waypoints>().points;
        way04 = way4.GetComponent<Waypoints>().points;
        way05 = way5.GetComponent<Waypoints>().points;
    }


    void Update()
    {
                     
    }
}

