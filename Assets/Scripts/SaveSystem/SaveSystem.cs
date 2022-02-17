using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{


    public static void SaveSaveData(int _lastEquippedVehicleMesh, int _lastEquippedVehicleColliderMesh, int _lastEquippedWeaponPrefab, int _lastEquippedMaterial, bool[] _boughtShips, bool[] _boughtWeapons, bool[] _boughtMaterials)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_lastEquippedVehicleMesh, _lastEquippedVehicleColliderMesh, _lastEquippedWeaponPrefab, _lastEquippedMaterial, _boughtShips, _boughtWeapons, _boughtMaterials);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/data.deathrace";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }

    public static void SaveMoneyData(int _milkyCoins)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/money.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_milkyCoins);

        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static SaveData LoadMoneyData()
    {
        string path = Application.persistentDataPath + "/money.deathrace";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }
    public static void SaveOptionsData(float _masterVolume, float _musicVolume, float _effectVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/options.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_masterVolume, _musicVolume, _effectVolume);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData LoadOptionsData()
    {
        string path = Application.persistentDataPath + "/options.deathrace";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }

    public static void SaveHighscoreData(float highScore, int _lastGhostVehicleIndex, int _lastGhostMaterialIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(highScore, _lastGhostVehicleIndex, _lastGhostMaterialIndex);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SaveData LoadHighscoreData()
    {
        string path = Application.persistentDataPath + "/score.deathrace";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }
}
