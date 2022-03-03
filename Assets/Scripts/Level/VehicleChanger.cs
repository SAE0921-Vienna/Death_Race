using System.Collections;
using UnityEngine;
using AI;

public class VehicleChanger : MonoBehaviour
{
    /// <summary>
    /// Changes the vehicle when driving through.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(transform.GetSiblingIndex());
        if (other.CompareTag("Player"))
        {
            SpaceshipLoad spaceshipLoad = other.GetComponentInParent<SpaceshipLoad>();
            BaseVehicleManager vehicleManager = other.GetComponentInParent<BaseVehicleManager>();

            if (spaceshipLoad.currentShip != transform.GetSiblingIndex())
            {
                StartCoroutine(other.GetComponentInParent<BaseVehicleManager>().SpawnEffect());
            }

            spaceshipLoad.currentShip = transform.GetSiblingIndex();
            spaceshipLoad.SetVehicleMesh();
            spaceshipLoad.SetVFXPrefab();
            if (!vehicleManager.noSpeedLimit)
            {
                spaceshipLoad.SetVehicleStats();
            }
            spaceshipLoad.SetWeaponPosition();
        }
    }
}
