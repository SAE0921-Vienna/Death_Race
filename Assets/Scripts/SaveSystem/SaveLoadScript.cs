using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{
    //public List<ShipData> boughtShips;
    //public List<WeaponData> boughtWeapons;
    ////Skins

    ////Equipped Last
    //public Mesh lastEquippedVehicleMesh;
    //public Mesh lastEquippedVehicleColliderMesh;
    //public GameObject lastEquippedWeaponPrefab;
    //public Material lastEquippedMaterial;

    public int lastEquippedVehicleMesh;
    public int lastEquippedVehicleColliderMesh;
    public int lastEquippedWeaponPrefab;
    public int lastEquippedMaterial;

    public void SaveSaveData()
    {
        SaveSystem.SaveSaveData(lastEquippedVehicleMesh, lastEquippedVehicleColliderMesh, lastEquippedWeaponPrefab, lastEquippedMaterial);
    }
    public void LoadSaveData()
    {
        SaveData data = SaveSystem.LoadSaveData();
        if (data == null)
        {
            Debug.LogWarning("Data empty");
        }
        else
        {
            lastEquippedVehicleMesh = data.lastEquippedVehicleMeshIndex;
            lastEquippedVehicleColliderMesh = data.lastEquippedVehicleColliderMeshIndex;
            lastEquippedWeaponPrefab = data.lastEquippedWeaponPrefabIndex;
            lastEquippedMaterial = data.lastEquippedMaterialIndex;
        }
    }
}



