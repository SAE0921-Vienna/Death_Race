using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class SpaceShipConfigurator : MonoBehaviour
{
    #region Editor Menu
    [ContextMenuItem(name: "Unlock All", function: "UnlockAll")]
    [ContextMenuItem(name: "Reset All", function: "ResetAll")]
    #endregion

    #region UI and SaveManager
    public GarageManager garageManager;
    [SerializeField]
    private SaveLoadScript saveLoadScript;
    #endregion

    #region Ships
    [SerializeField]
    private List<ShipData> allShips;
    public List<ShipConfig> ships;
    public int currentShip = 0;
    public int maxShips;
    #endregion

    #region Weapons
    [SerializeField]
    private List<WeaponData> allWeapons;
    public List<WeaponConfig> weapons;
    public int currentWeapon = 0;
    public int maxWeapons;
    private GameObject weaponClone;

    [HideInInspector]
    public IWeapon vehicleWeaponScript;
    #endregion

    #region Materials
    [Header("Skins")]
    public List<MaterialData> allMaterials;
    public List<MaterialConfig> materials;
    public MaterialData unavailableMaterial;
    public int currentMaterial = 0;
    public int maxMaterials;
    #endregion

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider maxSpeedSlider;
    [SerializeField]
    private Slider acceleration;
    [SerializeField]
    private Slider turnSpeed;
    [SerializeField]
    private Slider angularDrag;

    [SerializeField]
    private Slider damageSlider;
    [SerializeField]
    private Slider ammoSlider;
    [SerializeField]
    private Slider firerateSlider;



    /// <summary>
    /// Gets all the ships, weapons and materials in the game from the saved data
    /// </summary>
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

    /// <summary>
    /// Checks for a save data in the saveloadscript - gets all the information from the savedata or loads a new game
    /// </summary>
    public void CheckSaveLoadScript()
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

    /// <summary>
    /// Changes the ship (mesh, material and UIs)
    /// </summary>
    private void ChangeShip()
    {
        GetComponentInChildren<MeshFilter>().mesh = ships[currentShip].shipData.vehicleMesh;
        GetComponentInChildren<MeshCollider>().sharedMesh = ships[currentShip].shipData.vehicleColliderMesh;

        garageManager.shipStatsGarage.text = ships[currentShip].shipData.GetShipStats();
        SetShipStatSliders();

        if (ships[currentShip].shipBought)
        {
            if (ships[currentShip].shipBought && weapons[currentWeapon].weaponBought)
            {
                ChangeWeaponMaterial(currentMaterial);
                garageManager.saveAndCloseGarage.interactable = true;
                garageManager.unavailablePanel.SetActive(false);

            }
            ChangeShipMaterial(currentMaterial);


        }
        else
        {

            ChangeShipMaterial(unavailableMaterial);
            ChangeWeaponMaterial(unavailableMaterial);
            garageManager.saveAndCloseGarage.interactable = false;
            garageManager.unavailablePanel.SetActive(true);

        }


    }

    /// <summary>
    /// Changes the ship to the next ship
    /// </summary>
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

    /// <summary>
    /// Changes the ship to the previous ship
    /// </summary>
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

    /// <summary>
    /// Changes the weapon (prefab, material and UIs)
    /// </summary>
    private void ChangeWeapon()
    {
        weaponClone = Instantiate(weapons[currentWeapon].weaponData.vehicleWeaponPrefab, transform.GetChild(1).GetChild(1).transform, false);
        //weaponClone.GetComponentInChildren<WeaponRotator>().enabled = false;

        garageManager.weaponStatsGarage.text = weapons[currentWeapon].weaponData.GetWeaponStats();
        SetWeaponStatSliders();

        if (weapons[currentWeapon].weaponBought && ships[currentShip].shipBought)
        {
            ChangeWeaponMaterial(currentMaterial);
            garageManager.saveAndCloseGarage.interactable = true;
            garageManager.unavailablePanel.SetActive(false);

        }
        else
        {
            ChangeWeaponMaterial(unavailableMaterial);
            garageManager.saveAndCloseGarage.interactable = false;
            garageManager.unavailablePanel.SetActive(true);

        }

    }

    /// <summary>
    /// Changes the weapon to the next weapon
    /// </summary>
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

    /// <summary>
    /// Changes the weapon to the previous weapon
    /// </summary>
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

    /// <summary>
    /// Changes the material to the next material for the ship and the weapon in the garage
    /// </summary>
    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= maxMaterials) currentMaterial = 0;

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

    /// <summary>
    /// Changes the material to the previous material for the ship and the weapon in the garage
    /// </summary>
    public void PreviousMaterial()
    {
        currentMaterial--;
        if (currentMaterial < 0) currentMaterial = maxMaterials - 1;

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


    public void SetShipStatSliders()
    {
        healthSlider.value = ships[currentShip].shipData.health;
        maxSpeedSlider.value = ships[currentShip].shipData.maxSpeed;
        acceleration.value = ships[currentShip].shipData.accelerationSpeed;
        turnSpeed.value = ships[currentShip].shipData.turnSpeed;
        angularDrag.value = ships[currentShip].shipData.speedBasedAngularDrag;
    }

    public void SetWeaponStatSliders()
    {
        damageSlider.value = weapons[currentWeapon].weaponData.damage;
        ammoSlider.value = weapons[currentWeapon].weaponData.ammoSize;
        firerateSlider.value = weapons[currentWeapon].weaponData.fireRate;
    }



    /// <summary>
    /// Unlocks All Ships, Weapons and Materials in the game
    /// </summary>
    [ContextMenu(itemName: "Unlock All")]
    public void UnlockAll()
    {
        currentShip = 0;
        currentShip = 0;
        currentWeapon = 0;
        currentMaterial = 0;

        saveLoadScript.lastEquippedVehicleMesh = 0;
        saveLoadScript.lastEquippedVehicleColliderMesh = 0;
        saveLoadScript.lastEquippedWeaponPrefab = 0;
        saveLoadScript.lastEquippedMaterial = 0;

        for (int i = 1; i < maxShips; i++)
        {
            ships[i].shipBought = true;
        }
        for (int i = 1; i < maxWeapons; i++)
        {
            weapons[i].weaponBought = true;
        }
        for (int i = 1; i < maxMaterials; i++)
        {
            materials[i].materialBought = true;
        }

        saveLoadScript.SaveSaveData();
        CheckSaveLoadScript();

        ChangeShip();
        Destroy(weaponClone);
        ChangeWeapon();

        if (garageManager)
        {
            garageManager.shipName.text = ships[currentShip].shipData.name;
            garageManager.materialName.text = materials[currentMaterial].materialData.name;
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;

            garageManager.unavailablePanel.SetActive(false);

        }

    }

    /// <summary>
    /// Resets the game (all Ships, Weapons and Materials to zero)
    /// </summary>
    [ContextMenu(itemName: "Reset All")]
    public void ResetAll()
    {
        currentShip = 0;
        currentShip = 0;
        currentWeapon = 0;
        currentMaterial = 0;

        saveLoadScript.lastEquippedVehicleMesh = 0;
        saveLoadScript.lastEquippedVehicleColliderMesh = 0;
        saveLoadScript.lastEquippedWeaponPrefab = 0;
        saveLoadScript.lastEquippedMaterial = 0;

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
        saveLoadScript.SaveSaveData();
        CheckSaveLoadScript();

        ChangeShip();
        Destroy(weaponClone);
        ChangeWeapon();

        if (garageManager)
        {
            garageManager.shipName.text = ships[currentShip].shipData.name;
            garageManager.materialName.text = materials[currentMaterial].materialData.name;
            garageManager.weaponName.text = weapons[currentWeapon].weaponData.name;

            garageManager.unavailablePanel.SetActive(false);

        }

    }

}


/// <summary>
/// A Ship class with information from a scriptableobject (shipdata) and a bool if it has been bought
/// </summary>
[System.Serializable]
public class ShipConfig
{
    public ShipData shipData;
    public bool shipBought;
}


/// <summary>
///  A Weapon class with information from a scriptableobject (weapondata) and a bool if it has been bought
/// </summary>
[System.Serializable]
public class WeaponConfig
{
    public WeaponData weaponData;
    public bool weaponBought;
}


/// <summary>
///  A Material class with information from a scriptableobject (materialdata) and a bool if it has been bought
/// </summary>
[System.Serializable]
public class MaterialConfig
{
    public MaterialData materialData;
    public bool materialBought;
}

