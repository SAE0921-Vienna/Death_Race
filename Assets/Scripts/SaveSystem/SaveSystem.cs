using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveCustomizationData(Mesh vehicleMesh, Mesh vehicleColliderMesh, GameObject vehicleWeapon, Material vehicleMaterial)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.deathrace";
        FileStream stream = new FileStream(path, FileMode.Create);

        //CustomizationData data = new CustomizationData(vehicleMesh, vehicleColliderMesh, vehicleWeapon, vehicleMaterial);

        //formatter.Serialize(stream, data);
        stream.Close();
        
    }


    public static CustomizationData LoadPlayerData()
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

            CustomizationData data = formatter.Deserialize(stream) as CustomizationData;
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
