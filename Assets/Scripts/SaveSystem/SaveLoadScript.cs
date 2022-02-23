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
    public int maxMilkyCoins = 9999999;

    public float bestTime;
    public string currentMinAsString;
    public string currentSecAsString;
    public string currentMiliAsString;
    public int lastGhostVehicleIndex;
    public int lastGhostMaterialIndex;

    [Range(0.0001f, 1f)]
    public float masterVolume = 1f;
    [Range(0.0001f, 1f)]
    public float musicVolume = 1f;
    [Range(0.0001f, 1f)]
    public float effectVolume = 1f;

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
        SaveSystem.SaveMoneyData(milkyCoins);
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
        }
    }

    public void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        masterVolume = _masterVolume;
        musicVolume = _musicVolume;
        effectVolume = _effectVolume;
        SaveSystem.SaveOptionsData(masterVolume, musicVolume, effectVolume);
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

    public void SaveHighScore(float _bestTime, string _currentMin, string _currentSec, string _currentMilliSec, int _lastGhostVehicleIndex, int _lastGhostMaterialIndex)
    {
        SaveSystem.SaveHighscoreData(_bestTime, _currentMin, _currentSec, _currentMilliSec, _lastGhostVehicleIndex, _lastGhostMaterialIndex);
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
            bestTime = data.bestTime;
            currentMinAsString = data.currentMin;
            currentSecAsString = data.currentSec;
            currentMiliAsString = data.currentMilliSec;
            lastGhostVehicleIndex = data.lastGhostVehicleIndex;
            lastGhostMaterialIndex = data.lastGhostMaterialIndex;

        }
    }
}



