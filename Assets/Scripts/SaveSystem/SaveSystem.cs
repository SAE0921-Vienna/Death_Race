using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    // public static bool Save(string saveName, object saveData)
    // {

    //     BinaryFormatter formatter = GetBinaryFormatter();

    //     if(!Directory.Exists(Application.persistentDataPath + "/saves"))
    //     {
    //         Directory.CreateDirectory(Application.persistentDataPath + "/saves");
    //     }

    //     string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

    //     FileStream file = File.Create(path);

    //     formatter.Serialize(file, saveData);

    //     file.Close();

    //     return true;


    // }


    // public static object Load(string path)
    // {

    //     if (!File.Exists(path))
    //     {
    //         return null;
    //     }

    //     BinaryFormatter formatter = GetBinaryFormatter();

    //     FileStream file = File.Open(path, FileMode.Open);

    //     try
    //     {
    //         object save = formatter.Deserialize(file);
    //         file.Close();
    //         return save;
    //     }
    //     catch
    //     {
    //         Debug.LogErrorFormat("Failed to load file at {0}", path);
    //         file.Close();
    //         return null;
    //     }


    // }


    //public static BinaryFormatter GetBinaryFormatter()
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();

    //     return formatter;

    // }

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



}
