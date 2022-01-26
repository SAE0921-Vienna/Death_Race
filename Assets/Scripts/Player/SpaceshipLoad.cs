using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SpaceshipLoad : MonoBehaviour
{
    [SerializeField]
    private List<ShipData> allShips;
    [SerializeField]
    private List<WeaponData> allWeapons;
    [SerializeField]
    private List<MaterialData> allMaterials;


    [Header("Info")]
    [SerializeField]
    private SaveLoadScript saveLoadScript;

    [SerializeField]
    private int currentShip;
    [SerializeField]
    private int currentWeapon;
    [SerializeField]
    private int currentMaterial;

    [HideInInspector]
    public IWeapon vehicleWeaponScript;

    private void Awake()
    {
        if (saveLoadScript)
        {
            saveLoadScript.LoadSaveData();
            currentShip = saveLoadScript.lastEquippedVehicleMesh;
            currentShip = saveLoadScript.lastEquippedVehicleColliderMesh;
            currentWeapon = saveLoadScript.lastEquippedWeaponPrefab;
            currentMaterial = saveLoadScript.lastEquippedMaterial;


            GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
            GameObject.Find("SpaceShip").GetComponent<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

            GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;

            GameObject weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, GameObject.Find("WeaponPosition").transform, false);
            if (weaponClone.GetComponent<IWeapon>() == null) return;
            vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
            weaponClone.GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;

            weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;


        }
        else
        {
            Debug.LogWarning("SaveLoadScript NOT found");
        }
    }


}
