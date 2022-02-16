using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSmoothDamp : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 myVelocity;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref myVelocity, smoothTime);
    }
}
