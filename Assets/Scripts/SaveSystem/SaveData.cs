using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    ////Lists for Bought 
    public bool[] boughtShips;
    public bool[] boughtWeapons;
    public bool[] boughtMaterials;



    public int lastEquippedVehicleMeshIndex;
    public int lastEquippedVehicleColliderMeshIndex;
    public int lastEquippedWeaponPrefabIndex;
    public int lastEquippedMaterialIndex;

    public SaveData(int _lastEquippedVehicleMesh, int _lastEquippedVehicleColliderMesh, int _lastEquippedWeaponPrefab, int _lastEquippedMaterial, bool[] _boughtShips, bool[] _boughtWeapons, bool[] _boughtMaterials)
    {
        this.lastEquippedVehicleMeshIndex = _lastEquippedVehicleMesh;
        this.lastEquippedVehicleColliderMeshIndex = _lastEquippedVehicleColliderMesh;
        this.lastEquippedWeaponPrefabIndex = _lastEquippedWeaponPrefab;
        this.lastEquippedMaterialIndex = _lastEquippedMaterial;
        this.boughtShips = _boughtShips;
        this.boughtWeapons = _boughtWeapons;
        this.boughtMaterials = _boughtMaterials;
    }



}
