using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    ////Lists for Bought 
    ////Vehicles
    //public List<ShipData> boughtShips;

    ////Weapons
    //public List<WeaponData> boughtWeapons;

    //Skins


    ////Equipped Last
    ////Vehicle
    //public Mesh lastEquippedVehicleMesh;
    //public Mesh lastEquippedVehicleColliderMesh;

    ////Wepaon
    //public GameObject lastEquippedWeaponPrefab;

    ////Skin/Material
    //public Material lastEquippedMaterial;


    public int lastEquippedVehicleMeshIndex;
    public int lastEquippedVehicleColliderMeshIndex;
    public int lastEquippedWeaponPrefabIndex;
    public int lastEquippedMaterialIndex;

    public SaveData(int _lastEquippedVehicleMesh, int _lastEquippedVehicleColliderMesh, int _lastEquippedWeaponPrefab, int _lastEquippedMaterial)
    {
        this.lastEquippedVehicleMeshIndex = _lastEquippedVehicleMesh;
        this.lastEquippedVehicleColliderMeshIndex = _lastEquippedVehicleColliderMesh;
        this.lastEquippedWeaponPrefabIndex = _lastEquippedWeaponPrefab;
        this.lastEquippedMaterialIndex = _lastEquippedMaterial;
    }


}
