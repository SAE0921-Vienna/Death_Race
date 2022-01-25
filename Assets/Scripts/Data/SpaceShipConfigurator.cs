using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SpaceShipConfigurator : MonoBehaviour
{
    //[SerializeField] private ShipData shipData;
    //[SerializeField] private WeaponData weaponData;

    public GarageManager garageManager;
    [SerializeField]
    private SaveLoadScript saveLoadScript;

    [SerializeField]
    private List<ShipData> allShips;
    public List<ShipConfig> ships;
    public int currentShip = 0;
    public int boughtShips;
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
    public CustomizationData customizationData;
    public int currentMaterial = 0;
    public int maxMaterials;



    private void Awake()
    {

        maxShips = allShips.Count;
        maxWeapons = allWeapons.Count;
        maxMaterials = customizationData.vehicleMaterials.Length - 1;




        if (saveLoadScript)
        {
            saveLoadScript.LoadSaveData();
            currentShip = saveLoadScript.lastEquippedVehicleMesh;
            currentShip = saveLoadScript.lastEquippedVehicleColliderMesh;

            currentWeapon = saveLoadScript.lastEquippedWeaponPrefab;

            currentMaterial = saveLoadScript.lastEquippedMaterial;

        }

        for (int i = 0; i < maxShips; i++)
        {
            ships[i].ship = allShips[i];
            //ships[i].shipBought = saveLoadScript.boughtShips[i];
        }
        for (int i = 0; i < maxWeapons; i++)
        {
            weapons[i].weapon = allWeapons[i];
        }


        //GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        //GameObject.Find("SpaceShip").GetComponent<MeshCollider>().convex = allShips[currentShip].vehicleColliderMesh;

        ShipCustomization();

        GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;

        WeaponCustomization();
        weaponClone.transform.parent.localPosition = ships[currentShip].ship.WeaponPosition;



        if (garageManager)
        {
            garageManager.shipName.text = allShips[currentShip].name;
            garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;
            garageManager.weaponName.text = allWeapons[currentWeapon].name;
        }

    }


    private void ShipCustomization()
    {
        GetComponentInChildren<MeshFilter>().mesh = ships[currentShip].ship.vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().sharedMesh = ships[currentShip].ship.vehicleColliderMesh;
    }

    public void NextShip()
    {
        currentShip++;
        if (currentShip >= maxShips) currentShip = 0;

        ShipCustomization();
        weaponClone.transform.parent.localPosition = ships[currentShip].ship.WeaponPosition;

        if (garageManager)
        {
            garageManager.shipName.text = allShips[currentShip].name;
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
        weaponClone.transform.parent.localPosition = ships[currentShip].ship.WeaponPosition;

        if (garageManager)
        {
            garageManager.shipName.text = allShips[currentShip].name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedVehicleMesh = currentShip;
            saveLoadScript.lastEquippedVehicleColliderMesh = currentShip;
        }
    }


    private void WeaponCustomization()
    {
        weaponClone = Instantiate(weapons[currentWeapon].weapon.vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
    }
    public void NextWeapon()
    {
        currentWeapon++;
        if (currentWeapon >= maxWeapons) currentWeapon = 0;

        Destroy(weaponClone);
        WeaponCustomization();

        if (garageManager)
        {
            garageManager.weaponName.text = allWeapons[currentWeapon].name;
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
            garageManager.weaponName.text = allWeapons[currentWeapon].name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedWeaponPrefab = currentWeapon;
        }
    }

    private void MaterialCustomization()
    {
        GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
    }

    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= maxMaterials) currentMaterial = 0;


        MaterialCustomization();

        if (garageManager)
        {
            garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;
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
            garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;
        }
        if (saveLoadScript)
        {
            saveLoadScript.lastEquippedMaterial = currentMaterial;
        }

    }



    [System.Serializable]
    public class ShipConfig
    {
        public ShipData ship;
        public bool shipBought;
    }

    [System.Serializable]
    public class WeaponConfig
    {
        public WeaponData weapon;
        public bool weaponBought;
    }

    [System.Serializable]
    public class MaterialConfig
    {
        public Material[] materials;
        public bool materialBought;
    }



}
