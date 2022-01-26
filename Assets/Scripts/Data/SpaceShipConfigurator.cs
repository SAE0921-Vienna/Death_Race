using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SpaceShipConfigurator : MonoBehaviour
{

    public GarageManager garageManager;
    [SerializeField]
    private SaveLoadScript saveLoadScript;

    [SerializeField]
    private List<ShipData> allShips;
    public List<ShipConfig> ships;
    public int currentShip = 0;
    public int maxShips;

    [SerializeField]
    private List<WeaponData> allWeapons;
    public List<WeaponConfig> weapons;
    public int currentWeapon = 0;
    public int maxWeapons;
    private GameObject weaponClone;

    [HideInInspector]
    public IWeapon vehicleWeaponScript;

    [Header("Skins")]
    public List<MaterialData> allMaterials;
    public List<MaterialConfig> materials;
    public int currentMaterial = 0;
    public int maxMaterials;


    private void Awake()
    {
        maxShips = allShips.Count;
        maxWeapons = allWeapons.Count;
        maxMaterials = allMaterials.Count;

        for (int i = 0; i < maxShips; i++)
        {
            ships[i].shipData = allShips[i];
        }
        for (int i = 0; i < maxWeapons; i++)
        {
            weapons[i].weaponData = allWeapons[i];
        }
        for (int i = 0; i < maxMaterials; i++)
        {
            materials[i].materialData = allMaterials[i];
        }

        CheckSaveLoadScript();

        ShipCustomization();

        GetComponentInChildren<MeshRenderer>().material = materials[currentMaterial].materialData.material;

        WeaponCustomization();
        weaponClone.transform.parent.localPosition = ships[currentShip].shipData.WeaponPosition;

        if (garageManager)
        {
            garageManager.shipName.text = ships[currentShip].shipData.name;
            garageManager.materialName.text = materials[currentMaterial].materialData.name;
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;
        }

    }

    private void CheckSaveLoadScript()
    {
        if (saveLoadScript)
        {
            saveLoadScript.LoadSaveData();
            if (saveLoadScript.hasSaveData)
            {
                currentShip = saveLoadScript.lastEquippedVehicleMesh;
                currentShip = saveLoadScript.lastEquippedVehicleColliderMesh;
                currentWeapon = saveLoadScript.lastEquippedWeaponPrefab;
                currentMaterial = saveLoadScript.lastEquippedMaterial;
                for (int i = 0; i < maxShips; i++)
                {
                    ships[i].shipBought = saveLoadScript.boughtShips[i];
                }
                for (int i = 0; i < maxWeapons; i++)
                {
                    weapons[i].weaponBought = saveLoadScript.boughtWeapons[i];
                }
                for (int i = 0; i < maxMaterials; i++)
                {
                    materials[i].materialBought = saveLoadScript.boughtMaterials[i];

                }
            }
            else
            {
                currentShip = 0;
                currentShip = 0;
                currentWeapon = 0;
                currentMaterial = 0;
                for (int i = 1; i < maxShips; i++)
                {
                    ships[i].shipBought = false;
                }
                for (int i = 1; i < maxWeapons; i++)
                {
                    weapons[i].weaponBought = false;
                }
                for (int i = 1; i < maxMaterials; i++)
                {
                    materials[i].materialBought = false;

                }
            }
        }
        else
        {
            Debug.LogWarning("SaveLoadScript has NOT been found");

        }
    }


    private void ShipCustomization()
    {
        GetComponentInChildren<MeshFilter>().mesh = ships[currentShip].shipData.vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().sharedMesh = ships[currentShip].shipData.vehicleColliderMesh;
    }

    public void NextShip()
    {
        currentShip++;
        if (currentShip >= maxShips) currentShip = 0;

        ShipCustomization();
        weaponClone.transform.parent.localPosition = ships[currentShip].shipData.WeaponPosition;

        if (garageManager)
        {
            garageManager.shipName.text = ships[currentShip].shipData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedVehicleMesh = currentShip;
            saveLoadScript.lastEquippedVehicleColliderMesh = currentShip;
        }


    }
    public void PreviousShip()
    {
        currentShip--;
        if (currentShip < 0) currentShip = maxShips - 1;

        ShipCustomization();
        weaponClone.transform.parent.localPosition = ships[currentShip].shipData.WeaponPosition;

        if (garageManager)
        {
            garageManager.shipName.text = ships[currentShip].shipData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedVehicleMesh = currentShip;
            saveLoadScript.lastEquippedVehicleColliderMesh = currentShip;
        }
    }


    private void WeaponCustomization()
    {
        weaponClone = Instantiate(weapons[currentWeapon].weaponData.vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
    }
    public void NextWeapon()
    {
        currentWeapon++;
        if (currentWeapon >= maxWeapons) currentWeapon = 0;

        Destroy(weaponClone);
        WeaponCustomization();

        if (garageManager)
        {
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedWeaponPrefab = currentWeapon;
        }


    }
    public void PreviousWeapon()
    {
        currentWeapon--;
        if (currentWeapon < 0) currentWeapon = maxWeapons - 1;

        Destroy(weaponClone);
        WeaponCustomization();

        if (garageManager)
        {
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedWeaponPrefab = currentWeapon;
        }
    }

    private void MaterialCustomization()
    {
        GetComponentInChildren<MeshRenderer>().material = materials[currentMaterial].materialData.material;
        weaponClone.GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = materials[currentMaterial].materialData.material;
    }

    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= maxMaterials) currentMaterial = 0;


        MaterialCustomization();

        if (garageManager)
        {
            garageManager.materialName.text = materials[currentMaterial].materialData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedMaterial = currentMaterial;
        }

    }

    public void PreviousMaterial()
    {
        currentMaterial--;
        if (currentMaterial < 0) currentMaterial = maxMaterials - 1;

        MaterialCustomization();

        if (garageManager)
        {
            garageManager.materialName.text = materials[currentMaterial].materialData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedMaterial = currentMaterial;
        }

    }



}
[System.Serializable]
public class ShipConfig
{
    public ShipData shipData;
    public bool shipBought;
}
[System.Serializable]
public class WeaponConfig
{
    public WeaponData weaponData;
    public bool weaponBought;
}
[System.Serializable]
public class MaterialConfig
{
    public MaterialData materialData;
    public bool materialBought;
}

