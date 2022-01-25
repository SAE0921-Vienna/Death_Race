using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{


    ////Lists for Bought 
    ////Vehicles
    public bool[] boughtShips;

    ////Weapons
    //public List<WeaponData> boughtWeapons;

    //Skins


    public int lastEquippedVehicleMeshIndex;
    public int lastEquippedVehicleColliderMeshIndex;
    public int lastEquippedWeaponPrefabIndex;
    public int lastEquippedMaterialIndex;

    public SaveData(int _lastEquippedVehicleMesh, int _lastEquippedVehicleColliderMesh, int _lastEquippedWeaponPrefab, int _lastEquippedMaterial, bool[] _boughtShips)
    {
        this.lastEquippedVehicleMeshIndex = _lastEquippedVehicleMesh;
        this.lastEquippedVehicleColliderMeshIndex = _lastEquippedVehicleColliderMesh;
        this.lastEquippedWeaponPrefabIndex = _lastEquippedWeaponPrefab;
        this.lastEquippedMaterialIndex = _lastEquippedMaterial;
        this.boughtShips = _boughtShips;
    }


}
