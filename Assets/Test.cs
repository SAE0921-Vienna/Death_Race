using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int labs = 0;
    public int checkpoint = 0;
    public int[,] ship;
    public int[,] ai1;
    public int[,] ai2;
    public int[,] ai3;
    public int[] place;

    private void Awake()
    {
        ship = new int[labs, checkpoint];
        //place = new(ship, ai1, ai2, ai3);
    }
    // Update is called once per frame
    void Update()
    {
        Array.Sort(place);
    }
}
