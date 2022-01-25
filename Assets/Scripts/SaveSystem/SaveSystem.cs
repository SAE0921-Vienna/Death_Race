using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveSaveData(int _lastEquippedVehicleMesh, int _lastEquippedVehicleColliderMesh, int _lastEquippedWeaponPrefab, int _lastEquippedMaterial, bool[] _boughtShips)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_lastEquippedVehicleMesh, _lastEquippedVehicleColliderMesh, _lastEquippedWeaponPrefab, _lastEquippedMaterial, _boughtShips);

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
