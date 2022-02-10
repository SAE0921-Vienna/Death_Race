using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Hello World!");
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
