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
    public int starCoins;

    public float highScore;

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
    public SaveData(int milkyCoins, int starCoins)
    {
        this.milkyCoins = milkyCoins;
        this.starCoins = starCoins;
    }


    public SaveData(float highScore)
    {
        this.highScore = highScore;
    }

    //Options
    public SaveData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        this.masterVolume = _masterVolume;
        this.musicVolume = _musicVolume;
        this.effectVolume = _effectVolume;
    }

}

