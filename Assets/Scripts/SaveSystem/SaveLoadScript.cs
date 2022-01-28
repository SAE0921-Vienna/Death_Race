using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{
    public int lastEquippedVehicleMesh;
    public int lastEquippedVehicleColliderMesh;
    public int lastEquippedWeaponPrefab;
    public int lastEquippedMaterial;
    public bool[] boughtShips;
    public bool[] boughtWeapons;
    public bool[] boughtMaterials;

    public SpaceShipConfigurator spaceConfig;


    public bool[] GetBoughtShips()
    {
        if (spaceConfig)
        {
            for (int i = 0; i < spaceConfig.ships.Count; i++)
            {
                boughtShips[i] = spaceConfig.ships[i].shipBought;
            }
            return boughtShips;
        }
        else
        {
            Debug.LogWarning("SpaceShip Configurator has NOT been found");
            return null;
        }

    }

    public bool[] GetBoughtWeapons()
    {
        if (spaceConfig)
        {
            for (int i = 0; i < spaceConfig.weapons.Count; i++)
            {
                boughtWeapons[i] = spaceConfig.weapons[i].weaponBought;
            }
            return boughtWeapons;
        }
        else
        {
            Debug.LogWarning("SpaceShip Configurator has NOT been found");
            return null;
        }

    }

    public bool[] GetBoughtMaterials()
    {
        if (spaceConfig)
        {
            for (int i = 0; i < spaceConfig.materials.Count; i++)
            {
                boughtMaterials[i] = spaceConfig.materials[i].materialBought;
            }
            return boughtMaterials;
        }
        else
        {
            Debug.LogWarning("SpaceShip Configurator has NOT been found");
            return null;
        }

    }


    public bool hasSaveData;

    public void SaveSaveData()
    {
        SaveSystem.SaveSaveData(lastEquippedVehicleMesh, lastEquippedVehicleColliderMesh, lastEquippedWeaponPrefab, lastEquippedMaterial, GetBoughtShips(), GetBoughtWeapons(),GetBoughtMaterials());
    }

    public void LoadSaveData()
    {
        SaveData data = SaveSystem.LoadSaveData();
        if (data == null)
        {
            Debug.LogWarning("Data empty");
            hasSaveData = false;
        }
        else
        {
            hasSaveData = true;
            lastEquippedVehicleMesh = data.lastEquippedVehicleMeshIndex;
            lastEquippedVehicleColliderMesh = data.lastEquippedVehicleColliderMeshIndex;
            lastEquippedWeaponPrefab = data.lastEquippedWeaponPrefabIndex;
            lastEquippedMaterial = data.lastEquippedMaterialIndex;
            boughtShips = data.boughtShips;
            boughtWeapons = data.boughtWeapons;
            boughtMaterials = data.boughtMaterials;
        }
    }
}



