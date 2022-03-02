using System.Collections.Generic;
using PlayerController;
using UnityEngine;
using AI;

public class SpaceshipLoad : MonoBehaviour
{
    [SerializeField]
    public List<ShipData> allShips;
    public ShipData CurrentShip { get { return allShips[currentShip]; } }
    [SerializeField]
    public List<WeaponData> allWeapons;
    public WeaponData CurrentWeapon { get { return allWeapons[currentWeapon]; } }
    [SerializeField]
    protected List<MaterialData> allMaterials;


    [Header("Info")]
    [SerializeField]
    protected SaveLoadScript saveLoadScript;

    [SerializeField]
    public int currentShip;
    [SerializeField]
    public int currentWeapon;
    [SerializeField]
    public int currentMaterial;

    private BaseVehicleManager _baseVehicleManager;

    public GameObject weaponClone;


    private void Awake()
    {
        LoadShip();
        SetVFXPrefab();
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


            SetVehicleMesh();

            SetWeapon();

            weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;

            SetVehicleStats();
            SetWeaponStats();
        }

        else
        {
            Debug.LogWarning("SaveLoadScript NOT found");
        }
    }

    public void SetVehicleMesh()
    {
        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;
    }

    public void SetWeapon()
    {
        if(weaponClone != null)
        {
            Destroy(weaponClone);
        }

        weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab,
              transform.GetChild(1).GetChild(1).transform, false);
        weaponClone.GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material =
            allMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material =
      allMaterials[currentMaterial].material;
    }


    public void SetVehicleStats()
    {
        var _vehicleController = GetComponent<VehicleController>();
        _vehicleController.mMaxSpeed = allShips[currentShip].maxSpeed;
        _vehicleController.mAccelerationConstant = allShips[currentShip].accelerationSpeed;
        _vehicleController.steeringSpeed = allShips[currentShip].turnSpeed;
        _vehicleController.speedDependentAngularDragMagnitude = allShips[currentShip].speedBasedAngularDrag;
    }

    public void SetWeaponStats()
    {
        _baseVehicleManager = GetComponent<BaseVehicleManager>();
        _baseVehicleManager.ammo = allWeapons[currentWeapon].ammoSize;
        _baseVehicleManager.ammoAdd = _baseVehicleManager.ammo;
    }

    public void SetVFXPrefab()
    {
        if (GetComponentInChildren<SpaceshipRotator>())
            Instantiate(CurrentShip.vfxPrefab, GetComponentInChildren<SpaceshipRotator>().transform);
    }

    public void SetWeaponPosition()
    {
        weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;
    }

}
