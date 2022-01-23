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
    public CustomizationData customizationData;
    public int currentMaterial = 0;
    public int maxMaterials;



    private void Awake()
    {

        maxShips = allShips.Count;
        maxWeapons = allWeapons.Count;
        maxMaterials = customizationData.vehicleMaterials.Length - 1;

        for (int i = 0; i < maxShips; i++)
        {
            ships[i].ship = allShips[i];
        }
        for (int i = 0; i < maxWeapons; i++)
        {
            weapons[i].weapon = allWeapons[i];
        }

        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().convex = allShips[currentShip].vehicleColliderMesh;
        garageManager.shipName.text = allShips[currentShip].name;


        GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;


        weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.weaponName.text = allWeapons[currentWeapon].name;

    }

    public void NextShip()
    {
        currentShip++;
        if (currentShip >= maxShips) currentShip = 0;

        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().convex = allShips[currentShip].vehicleColliderMesh;
        garageManager.shipName.text = allShips[currentShip].name;


    }
    public void PreviousShip()
    {
        currentShip--;
        if (currentShip < 0) currentShip = maxShips - 1;

        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GameObject.Find("SpaceShip").GetComponent<MeshCollider>().convex = allShips[currentShip].vehicleColliderMesh;
        garageManager.shipName.text = allShips[currentShip].name;


    }

    public void NextWeapon()
    {
        currentWeapon++;
        if (currentWeapon >= maxWeapons) currentWeapon = 0;

        Destroy(weaponClone);
        weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.weaponName.text = allWeapons[currentWeapon].name;

    }
    public void PreviousWeapon()
    {
        currentWeapon--;
        if (currentWeapon < 0) currentWeapon = maxWeapons - 1;

        Destroy(weaponClone);
        weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.weaponName.text = allWeapons[currentWeapon].name;

    }

    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= maxMaterials) currentMaterial = 0;

        GetComponentInChildren<MeshRenderer>().material =  customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;


    }

    public void PreviousMaterial()
    {
        currentMaterial--;
        if (currentMaterial < 0) currentMaterial = maxMaterials - 1;

        GetComponentInChildren<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = customizationData.vehicleMaterials[currentMaterial].material;
        garageManager.materialName.text = customizationData.vehicleMaterials[currentMaterial].name;

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
