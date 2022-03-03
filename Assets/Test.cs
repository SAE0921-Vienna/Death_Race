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
    public int[][,] place;


    private void Awake()
    {
        ship = new int[labs, checkpoint];
        ai1 = new int[labs, checkpoint];
        ai2 = new int[labs, checkpoint];
        ai3 = new int[labs, checkpoint];

        place[0] = ship;
        place[1] = ai1;
        place[2] = ai2;
        place[3] = ai3;
    }
    // Update is called once per frame
    void Update()
    {
        Array.Sort(place);
    }
}
