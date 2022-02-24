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

    public int milkyCoins;

    public float bestTime;
    public string currentMin;
    public string currentSec;
    public string currentMilliSec;
    public int lastGhostVehicleIndex;
    public int lastGhostMaterialIndex;


    public float masterVolume;
    public float musicVolume;
    public float effectVolume;

    //SpaceShip/Progress
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

    //Progress (money)
    public SaveData(int milkyCoins)
    {
        this.milkyCoins = milkyCoins;
    }


    public SaveData(float _bestTime, string _currentMin, string _currentSec, string _currentMilliSec, int _lastGhostVehicleIndex, int _lastGhostMaterialIndex)
    {
        this.bestTime = _bestTime;
        this.currentMin = _currentMin;
        this.currentSec = _currentSec;
        this.currentMilliSec = _currentMilliSec;
        this.lastGhostVehicleIndex = _lastGhostVehicleIndex;
        this.lastGhostMaterialIndex = _lastGhostMaterialIndex;
    }

    //Options
    public SaveData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        this.masterVolume = _masterVolume;
        this.musicVolume = _musicVolume;
        this.effectVolume = _effectVolume;
    }

}

