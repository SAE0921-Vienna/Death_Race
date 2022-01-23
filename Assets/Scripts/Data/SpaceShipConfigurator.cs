using System;
using UnityEngine;
using Weapons;

public class SpaceShipConfigurator : MonoBehaviour
{
    [SerializeField] private ShipData shipData;
    [SerializeField] private CustomizationData customizationData;
    [SerializeField] private WeaponData weaponData;


    [HideInInspector]
    public IWeapon vehicleWeaponScript;

    // Set all the values in the scriptable object, to the MonoBehavior object.
    private void Awake()
    {
        GetComponentInChildren<MeshFilter>().mesh = shipData.vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().convex = shipData.vehicleColliderMesh;

        GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterials[(int)customizationData.materialType];

        GameObject weaponClone = Instantiate(weaponData.vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null ) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[(int)customizationData.materialType];
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[(int)customizationData.materialType];
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[(int)customizationData.materialType];
    } 
}
