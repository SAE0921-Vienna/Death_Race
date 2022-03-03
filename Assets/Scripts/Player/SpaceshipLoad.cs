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
    public GameObject vfxClone;


    private void Awake()
    {
        LoadShip();
        SetVFXPrefab();
    }

    /// <summary>
    /// Load the ship.
    /// </summary>
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
        }
        else
        {
            Debug.LogWarning("SaveLoadScript NOT found");
        }
    }

    /// <summary>
    /// Sets the vehicle's material.
    /// </summary>
    public void SetVehicleMesh()
    {
        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;
    }

    /// <summary>
    /// Sets the weapon
    /// </summary>
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

    /// <summary>
    /// Sets the vehicle stats.
    /// </summary>
    public void SetVehicleStats()
    {
        var _vehicleController = GetComponent<VehicleController>();
        if (_vehicleController == null) return;
        _vehicleController.mMaxSpeed = allShips[currentShip].maxSpeed;
        _vehicleController.mAccelerationConstant = allShips[currentShip].accelerationSpeed;
        _vehicleController.steeringSpeed = allShips[currentShip].turnSpeed;
        _vehicleController.speedDependentAngularDragMagnitude = allShips[currentShip].speedBasedAngularDrag;
    }

    /// <summary>
    /// Sets the weapon stats.
    /// </summary>
    public void SetWeaponStats()
    {
        _baseVehicleManager = GetComponent<BaseVehicleManager>();
        _baseVehicleManager.ammo = allWeapons[currentWeapon].ammoSize;
        _baseVehicleManager.ammoAdd = _baseVehicleManager.ammo;
    }

    /// <summary>
    /// Sets the VFX prefab.
    /// </summary>
    public void SetVFXPrefab()
    {
        if(vfxClone!= null)
        {
            Destroy(vfxClone);
        }

        if (GetComponentInChildren<SpaceshipRotator>())
            vfxClone = Instantiate(CurrentShip.vfxPrefab, GetComponentInChildren<SpaceshipRotator>().transform);
    }

    /// <summary>
    /// Sets the weapon position.
    /// </summary>
    public void SetWeaponPosition()
    {
        weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;
    }

}
