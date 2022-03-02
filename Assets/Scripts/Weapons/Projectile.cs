using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PROJECTILE SCRIPT");
        Destroy(gameObject);
    }
}
