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


    public int milkyCoins;
    public int starCoins;
    public int maxMilkyCoins = 9999999;
    public int maxStarCoins = 9999999;

    public float highScore;
    public int lastGhostVehicleIndex;
    public int lastGhostMaterialIndex;

    [Range(0.0001f, 1f)]
    public float masterVolume = 0.0001f;
    [Range(0.0001f, 1f)]
    public float musicVolume = 0.0001f;
    [Range(0.0001f, 1f)]
    public float effectVolume = 0.0001f;

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
        SaveSystem.SaveSaveData(lastEquippedVehicleMesh, lastEquippedVehicleColliderMesh, lastEquippedWeaponPrefab, lastEquippedMaterial, GetBoughtShips(), GetBoughtWeapons(), GetBoughtMaterials());
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

    public void SaveMoneyData()
    {
        SaveSystem.SaveMoneyData(milkyCoins, starCoins);
    }
    public void LoadMoneyData()
    {
        SaveData data = SaveSystem.LoadMoneyData();
        if (data == null)
        {
            Debug.LogWarning("Data empty");
        }
        else
        {
            milkyCoins = data.milkyCoins;
            starCoins = data.starCoins;
        }
    }

    public void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        SaveSystem.SaveOptionsData(_masterVolume, _musicVolume, _effectVolume);
    }
    public void LoadOptionsData()
    {
        SaveData data = SaveSystem.LoadOptionsData();
        if (data == null)
        {
            Debug.LogWarning("Data empty");
        }
        else
        {
            masterVolume = data.masterVolume;
            musicVolume = data.musicVolume;
            effectVolume = data.effectVolume;
        }
    }

    public void SaveHighScore(float highScore, int _lastGhostVehicleIndex, int _lastGhostMaterialIndex)
    {
        SaveSystem.SaveHighscoreData(highScore, _lastGhostVehicleIndex, _lastGhostMaterialIndex);
    }
    public void LoadHighScoreData()
    {
        SaveData data = SaveSystem.LoadHighscoreData();
        if (data == null)
        {
            //Debug.LogWarning("Data empty");
        }
        else
        {
            highScore = data.highScore;
            lastGhostVehicleIndex = data.lastGhostVehicleIndex;
            lastGhostMaterialIndex = data.lastGhostMaterialIndex;

        }
    }
}



