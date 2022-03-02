using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{
    #region Data
    public int lastEquippedVehicleMesh;
    public int lastEquippedVehicleColliderMesh;
    public int lastEquippedWeaponPrefab;
    public int lastEquippedMaterial;
    public bool[] boughtShips;
    public bool[] boughtWeapons;
    public bool[] boughtMaterials;

    public bool hasSaveData;
    public bool hasMoneyData;
    public bool hasOptionData;
    public bool hasBestTimeData;
    #endregion

    #region References
    public SpaceShipConfigurator spaceConfig;
    #endregion

    #region Money
    public int milkyCoins;
    public int maxMilkyCoins = 9999999;
    #endregion

    #region Best Time
    public float bestTime;
    public string currentMinAsString;
    public string currentSecAsString;
    public string currentMiliAsString;
    public int lastGhostVehicleIndex;
    public int lastGhostMaterialIndex;
    #endregion

    #region Audio
    [Range(0.0001f, 1f)]
    public float masterVolume = 1f;
    [Range(0.0001f, 1f)]
    public float musicVolume = 1f;
    [Range(0.0001f, 1f)]
    public float effectVolume = 1f;
    #endregion

    private void Awake()
    {
        LoadSaveData();
        LoadMoneyData();
        LoadOptionsData();
        LoadHighScoreData();
    }

    /// <summary>
    /// Gets all the bought ships in the game
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Gets all the bought weapons in the game
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Gets all the bought materials in the game
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Saves the last equipped/saved vehicle
    /// </summary>
    public void SaveSaveData()
    {
        SaveSystem.SaveSaveData(lastEquippedVehicleMesh, lastEquippedVehicleColliderMesh, lastEquippedWeaponPrefab, lastEquippedMaterial, GetBoughtShips(), GetBoughtWeapons(), GetBoughtMaterials());
        hasSaveData = true;
    }

    /// <summary>
    ///  Loads the last equipped/saved vehicle
    /// </summary>
    public void LoadSaveData()
    {
        SaveData data = SaveSystem.LoadSaveData();
        if (data == null)
        {
            Debug.LogWarning("Load Data empty");
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

    /// <summary>
    /// Saves the money the player has
    /// </summary>
    public void SaveMoneyData()
    {
        SaveSystem.SaveMoneyData(milkyCoins);
        hasMoneyData = true;

    }

    /// <summary>
    /// Loads the money the player has
    /// </summary>
    public void LoadMoneyData()
    {
        SaveData data = SaveSystem.LoadMoneyData();
        if (data == null)
        {
            Debug.LogWarning("Moneydata empty");
            hasMoneyData = false;
        }
        else
        {
            milkyCoins = data.milkyCoins;
        }
    }

    /// <summary>
    /// Sets and saves the audio (masterVolume, musicVolume, effectVolume)
    /// </summary>
    /// <param name="_masterVolume"></param>
    /// <param name="_musicVolume"></param>
    /// <param name="_effectVolume"></param>
    public void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        masterVolume = _masterVolume;
        musicVolume = _musicVolume;
        effectVolume = _effectVolume;
        SaveSystem.SaveOptionsData(masterVolume, musicVolume, effectVolume);
        hasOptionData = true;
    }

    /// <summary>
    /// Loads the audio (masterVolume, musicVolume, effectVolume)
    /// </summary>
    public void LoadOptionsData()
    {
        SaveData data = SaveSystem.LoadOptionsData();
        if (data == null)
        {
            Debug.LogWarning("Data empty");
            hasOptionData = false;
            masterVolume = 0.5f;
            musicVolume = 0.5f;
            effectVolume = 0.5f;
        }
        else
        {
            masterVolume = data.masterVolume;
            musicVolume = data.musicVolume;
            effectVolume = data.effectVolume;
        }
    }

    /// <summary>
    /// Saves the best time of the player in ghostmode + the vehicle driven at the time
    /// </summary>
    /// <param name="_bestTime"></param>
    /// <param name="_currentMin"></param>
    /// <param name="_currentSec"></param>
    /// <param name="_currentMilliSec"></param>
    /// <param name="_lastGhostVehicleIndex"></param>
    /// <param name="_lastGhostMaterialIndex"></param>
    public void SaveHighScore(float _bestTime, string _currentMin, string _currentSec, string _currentMilliSec, int _lastGhostVehicleIndex, int _lastGhostMaterialIndex)
    {
        lastGhostVehicleIndex = _lastGhostVehicleIndex;
        lastGhostMaterialIndex = _lastGhostMaterialIndex;
        SaveSystem.SaveHighscoreData(_bestTime, _currentMin, _currentSec, _currentMilliSec, lastGhostVehicleIndex, lastGhostMaterialIndex);
        hasBestTimeData = true;
    }

    /// <summary>
    /// Loads the best time of the player in ghostmode + the vehicle driven at the time
    /// </summary>
    public void LoadHighScoreData()
    {
        SaveData data = SaveSystem.LoadHighscoreData();
        if (data == null)
        {
            //Debug.LogWarning("Data empty");
            hasBestTimeData = false;

        }
        else
        {
            bestTime = data.bestTime;
            currentMinAsString = data.currentMin;
            currentSecAsString = data.currentSec;
            currentMiliAsString = data.currentMilliSec;
            lastGhostVehicleIndex = data.lastGhostVehicleIndex;
            lastGhostMaterialIndex = data.lastGhostMaterialIndex;
            hasBestTimeData = true;


        }
    }
}



