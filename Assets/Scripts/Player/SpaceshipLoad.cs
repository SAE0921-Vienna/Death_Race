using System.Collections.Generic;
using PlayerController;
using UnityEngine;

using Weapons;

public class SpaceshipLoad : MonoBehaviour
{
    [SerializeField]
    protected List<ShipData> allShips;
    public ShipData CurrentShip { get { return allShips[currentShip];} }
    [SerializeField]
    protected List<WeaponData> allWeapons;
    public WeaponData CurrentWeapon { get { return allWeapons[currentWeapon]; } }
    [SerializeField]
    protected List<MaterialData> allMaterials;


    [Header("Info")]
    [SerializeField]
    protected SaveLoadScript saveLoadScript;

    [SerializeField]
    protected int currentShip;
    [SerializeField]
    protected int currentWeapon;
    [SerializeField]
    public int currentMaterial;

    private VehicleController _vehicleController;


    private void Awake()
    {
        LoadShip();
    }

    private void LoadShip()
    {
        if (saveLoadScript)
        {
            saveLoadScript.LoadSaveData();
            currentShip = saveLoadScript.lastEquippedVehicleMesh;
            currentShip = saveLoadScript.lastEquippedVehicleColliderMesh;
            currentWeapon = saveLoadScript.lastEquippedWeaponPrefab;
            currentMaterial = saveLoadScript.lastEquippedMaterial;


            GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
            //GetComponentInChildren<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

            GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;

            GameObject weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab,
                transform.GetChild(1).GetChild(1).transform, false);
            weaponClone.GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material =
                allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material =
                allMaterials[currentMaterial].material;

            weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;

            SetVehicleStats();
        }
        
        else
        {
            Debug.LogWarning("SaveLoadScript NOT found");
        }
    }

    private void SetVehicleStats()
    {
        _vehicleController = GetComponent<VehicleController>();
        _vehicleController.mMaxSpeed = allShips[currentShip].maxSpeed;
        _vehicleController.mAccelerationConstant = allShips[currentShip].accelerationSpeed;
        _vehicleController.steeringSpeed = allShips[currentShip].turnSpeed;
        _vehicleController.speedDependentAngularDragMagnitude = allShips[currentShip].speedBasedAngularDrag;
    }
}
