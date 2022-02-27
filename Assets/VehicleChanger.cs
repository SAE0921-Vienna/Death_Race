using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(transform.GetSiblingIndex());
        if (other.CompareTag("Player"))
        {
            SpaceshipLoad spaceshipLoad = other.GetComponentInParent<SpaceshipLoad>();

            spaceshipLoad.currentShip = transform.GetSiblingIndex();
            spaceshipLoad.SetVehicleMesh();
            spaceshipLoad.SetVehicleStats();
            spaceshipLoad.SetWeaponPosition();
        }
  

    }
}
