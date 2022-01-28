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
    public MaterialData unavailableMaterial;
    public int currentMaterial = 0;
    public int maxMaterials;

    private void Awake()
    {
        maxShips = allShips.Count;
        maxWeapons = allWeapons.Count;
        maxMaterials = allMaterials.Count;

        CheckSaveLoadScript();

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

        ChangeWeapon();
        weaponClone.transform.parent.localPosition = ships[currentShip].shipData.WeaponPosition;

        ChangeShip();

        ChangeShipMaterial(currentMaterial);

        

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


    private void ChangeShip()
    {
        GetComponentInChildren<MeshFilter>().mesh = ships[currentShip].shipData.vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().sharedMesh = ships[currentShip].shipData.vehicleColliderMesh;


        if (ships[currentShip].shipBought)
        {
            if (ships[currentShip].shipBought && weapons[currentWeapon].weaponBought)
            {
                ChangeWeaponMaterial(currentMaterial);
            }
            ChangeShipMaterial(currentMaterial);
            //garageManager.materialNext.interactable = true;
            //garageManager.materialPrevious.interactable = true;
            garageManager.saveAndCloseGarage.interactable = true;

        }
        else
        {

            ChangeShipMaterial(unavailableMaterial);
            ChangeWeaponMaterial(unavailableMaterial);
            //garageManager.materialNext.interactable = false;
            //garageManager.materialPrevious.interactable = false;
            garageManager.saveAndCloseGarage.interactable = false;

        }
      

    }

    public void NextShip()
    {
        currentShip++;
        if (currentShip >= maxShips) currentShip = 0;

        ChangeShip();
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

        ChangeShip();
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


    private void ChangeWeapon()
    {
        weaponClone = Instantiate(weapons[currentWeapon].weaponData.vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();

        if (weapons[currentWeapon].weaponBought && ships[currentShip].shipBought)
        {
            ChangeWeaponMaterial(currentMaterial);
            //garageManager.materialNext.interactable = true;
            //garageManager.materialPrevious.interactable = true;
            garageManager.saveAndCloseGarage.interactable = true;
        }
        else
        {
            ChangeWeaponMaterial(unavailableMaterial);
            //garageManager.materialNext.interactable = false;
            //garageManager.materialPrevious.interactable = false;
            garageManager.saveAndCloseGarage.interactable = false;
        }

    }


    public void NextWeapon()
    {
        currentWeapon++;
        if (currentWeapon >= maxWeapons) currentWeapon = 0;

        Destroy(weaponClone);

        ChangeWeapon();

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
        ChangeWeapon();

        if (garageManager)
        {
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedWeaponPrefab = currentWeapon;
        }
    }

    private void ChangeMaterialAll(int materialIndex)
    {

        ChangeShipMaterial(materialIndex);

        ChangeWeaponMaterial(materialIndex);
    }

    /// <summary>
    /// Change the Ships Material with materialIndex
    /// </summary>
    /// <param name="materialIndex"></param>
    private void ChangeShipMaterial(int materialIndex)
    {
        GetComponentInChildren<MeshRenderer>().material = materials[materialIndex].materialData.material;
    }

    /// <summary>
    /// Change the Ships Material with material
    /// </summary>
    /// <param name="material"></param>
    private void ChangeShipMaterial(MaterialData material)
    {
        GetComponentInChildren<MeshRenderer>().material = material.material;
    }

    /// <summary>
    /// Change the Weapons Material with materialIndex
    /// </summary>
    /// <param name="materialIndex"></param>
    private void ChangeWeaponMaterial(int materialIndex)
    {
        weaponClone.GetComponent<MeshRenderer>().material = materials[materialIndex].materialData.material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[materialIndex].materialData.material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = materials[materialIndex].materialData.material;
    }

    /// <summary>
    /// Change the Weapons Material with a material
    /// </summary>
    /// <param name="material"></param>
    private void ChangeWeaponMaterial(MaterialData material)
    {
        weaponClone.GetComponent<MeshRenderer>().material = material.material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = material.material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = material.material;
    }



    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= maxMaterials) currentMaterial = 0;

        //if (materials[currentMaterial].materialBought)
        //{
        //    ChangeMaterialAll(currentMaterial);

        //    if (garageManager)
        //    {
        //        garageManager.materialName.text = materials[currentMaterial].materialData.name;
        //    }
        //    if (saveLoadScript)
        //    {
        //        saveLoadScript.lastEquippedMaterial = currentMaterial;
        //    }
        //}
        //else
        //{
        //    while (!materials[currentMaterial].materialBought)
        //    {
        //        NextMaterial();
        //    }

        //    //currentMaterial--;
        //    //if (currentMaterial < 0) currentMaterial = maxMaterials - 1;
        //}

        if (materials[currentMaterial].materialBought && ships[currentShip].shipBought)
        {
            ChangeShipMaterial(currentMaterial);

            if (garageManager)
            {
                garageManager.materialName.text = materials[currentMaterial].materialData.name;
            }
            if (saveLoadScript)
            {
                saveLoadScript.lastEquippedMaterial = currentMaterial;
            }
        }
        else
        {
            while (!materials[currentMaterial].materialBought)
            {
                NextMaterial();
            }


        }

        if (materials[currentMaterial].materialBought && weapons[currentWeapon].weaponBought && ships[currentShip].shipBought)
        {
            ChangeWeaponMaterial(currentMaterial);

            if (garageManager)
            {
                garageManager.materialName.text = materials[currentMaterial].materialData.name;
            }
            if (saveLoadScript)
            {
                saveLoadScript.lastEquippedMaterial = currentMaterial;
            }
        }
        else
        {
            while (!materials[currentMaterial].materialBought)
            {
                NextMaterial();
            }


        }

    }

    public void PreviousMaterial()
    {
        currentMaterial--;
        if (currentMaterial < 0) currentMaterial = maxMaterials - 1;

        //if (materials[currentMaterial].materialBought)
        //{
        //    ChangeMaterialAll(currentMaterial);

        //    if (garageManager)
        //    {
        //        garageManager.materialName.text = materials[currentMaterial].materialData.name;
        //    }
        //    if (saveLoadScript)
        //    {
        //        saveLoadScript.lastEquippedMaterial = currentMaterial;
        //    }
        //}
        //else
        //{
        //    while (!materials[currentMaterial].materialBought)
        //    {
        //        PreviousMaterial();
        //    }
        //    //currentMaterial++;
        //    //if (currentMaterial >= maxMaterials) currentMaterial = 0;

        //}
        if (materials[currentMaterial].materialBought && ships[currentShip].shipBought)
        {
            ChangeShipMaterial(currentMaterial);

            if (garageManager)
            {
                garageManager.materialName.text = materials[currentMaterial].materialData.name;
            }
            if (saveLoadScript)
            {
                saveLoadScript.lastEquippedMaterial = currentMaterial;
            }
        }
        else
        {
            while (!materials[currentMaterial].materialBought)
            {
                PreviousMaterial();
            }


        }

        if (materials[currentMaterial].materialBought && weapons[currentWeapon].weaponBought && ships[currentShip].shipBought)
        {
            ChangeWeaponMaterial(currentMaterial);

            if (garageManager)
            {
                garageManager.materialName.text = materials[currentMaterial].materialData.name;
            }
            if (saveLoadScript)
            {
                saveLoadScript.lastEquippedMaterial = currentMaterial;
            }
        }
        else
        {
            while (!materials[currentMaterial].materialBought)
            {
                PreviousMaterial();
            }


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

