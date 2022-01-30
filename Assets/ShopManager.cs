using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public SpaceShipConfigurator spaceShipConfigurator;

    public SaveLoadScript saveLoadScript;


    public List<ShipConfig> shipsShop;
    public List<WeaponConfig> weaponsShop;
    public List<MaterialConfig> materialsShop;

    public GameObject[] buttonSwitch;
    public GameObject[] placeholderSwitch;
    private GameObject weaponClone;
    public int currentIndexSwitch = 0;
    public int currentShipPrefab = 0;
    public int currentWeaponPrefab = 0;
    public int currentMaterial = 0;
    public int currentSW = 0; //Ship - Weapon

    public Material hologramMAT;

    public TextMeshProUGUI shipName;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI materialName;
    public TextMeshProUGUI swName;


    public float timer;
    public float timerCooldown = 5f;
    public bool automaticRotation;


    private void Awake()
    {
        GetListsGManager(); // for now for testing
        automaticRotation = false;
        timer = timerCooldown;

    }

    private void Update()
    {
        if (automaticRotation)
        {
            AutomaticSWRotation();
        }

    }

    public void AutomaticSWRotation()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (currentSW == 0)
            {
                currentShipPrefab++;

                Destroy(weaponClone);
                if (currentShipPrefab >= shipsShop.Count)
                {
                    currentShipPrefab = 0;
                }
                transform.GetChild(2).GetComponent<MeshFilter>().mesh = shipsShop[currentShipPrefab].shipData.vehicleMesh;
                transform.GetChild(2).GetComponent<MeshRenderer>().material = materialsShop[currentMaterial].materialData.material;
                swName.text = "Ship";

                transform.GetChild(2).localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if (currentSW == 1)
            {
                currentWeaponPrefab++;

                transform.GetChild(2).GetComponent<MeshFilter>().mesh = null;
                if (currentWeaponPrefab >= weaponsShop.Count)
                {
                    currentWeaponPrefab = 0;
                }
                Destroy(weaponClone);
                weaponClone = Instantiate(weaponsShop[currentWeaponPrefab].weaponData.vehicleWeaponPrefab, transform.GetChild(2).transform, false);
                weaponClone.GetComponentInChildren<WeaponRotator>().enabled = false;
                weaponClone.GetComponent<MeshRenderer>().material = materialsShop[currentMaterial].materialData.material;
                weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = materialsShop[currentMaterial].materialData.material;
                weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = materialsShop[currentMaterial].materialData.material;
                swName.text = "Weapon";
                transform.GetChild(2).localScale = new Vector3(0.3f, 0.3f, 0.3f);


            }



            timer = timerCooldown;
        }
    }

    public void SwitchSW()
    {
        currentSW++;
        if (currentSW > 1)
        {
            currentSW = 0;
        }
        timer = 0;
    }


    public void GetListsGManager()
    {

        for (int i = 0; i < spaceShipConfigurator.ships.Count; i++)
        {
            shipsShop.Add(spaceShipConfigurator.ships[i]);
        }
        for (int i = 0; i < spaceShipConfigurator.weapons.Count; i++)
        {
            weaponsShop.Add(spaceShipConfigurator.weapons[i]);
        }
        for (int i = 0; i < spaceShipConfigurator.materials.Count; i++)
        {
            materialsShop.Add(spaceShipConfigurator.materials[i]);
        }


    }

    public void SwitchPrefabs()
    {

        switch (currentIndexSwitch)
        {
            case 0:
                ChangeShipShop();
                break;
            case 1:
                Destroy(weaponClone);
                ChangeWeaponShop();
                break;
            case 2:
                currentSW = 0;
                automaticRotation = true;
                break;
            default:
                Debug.LogWarning("Error in the Shop - Switch Prefab");
                break;
        }

    }



    private void ChangeShipShop()
    {
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = shipsShop[currentShipPrefab].shipData.vehicleMesh;
        shipName.text = shipsShop[currentShipPrefab].shipData.name;
    }

    public void NextShipShop()
    {
        currentShipPrefab++;
        if (currentShipPrefab >= shipsShop.Count)
        {
            currentShipPrefab = 0;
        }

        ChangeShipShop();
    }

    public void PreviousShipShop()
    {
        currentShipPrefab--;
        if (currentShipPrefab < 0)
        {
            currentShipPrefab = shipsShop.Count - 1;
        }

        ChangeShipShop();
    }

    private void ChangeWeaponShop()
    {
        weaponClone = Instantiate(weaponsShop[currentWeaponPrefab].weaponData.vehicleWeaponPrefab, transform.GetChild(1).transform, false);
        weaponClone.GetComponentInChildren<WeaponRotator>().enabled = false;
        weaponClone.GetComponent<MeshRenderer>().material = hologramMAT;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;
        weaponName.text = weaponsShop[currentWeaponPrefab].weaponData.name;

    }

    public void NextWeaponShop()
    {
        Destroy(weaponClone);
        currentWeaponPrefab++;
        if (currentWeaponPrefab >= weaponsShop.Count)
        {
            currentWeaponPrefab = 0;
        }

        ChangeWeaponShop();
    }
    public void PreviousWeaponShop()
    {
        Destroy(weaponClone);
        currentWeaponPrefab--;
        if (currentWeaponPrefab < 0)
        {
            currentWeaponPrefab = weaponsShop.Count - 1;
        }

        ChangeWeaponShop();
    }


    public void NextMaterialShop()
    {
        currentMaterial++;
   
        if (currentMaterial >= materialsShop.Count)
        {
            currentMaterial = 0;
        }
        timer = 0;
        materialName.text = materialsShop[currentMaterial].materialData.name;
    }

    public void PreviousMaterialShop()
    {
        currentMaterial--;
       
        if (currentMaterial < 0)
        {
            currentMaterial = materialsShop.Count - 1;
        }
        timer = 0;
        materialName.text = materialsShop[currentMaterial].materialData.name;

    }

    public void SwitchRight()
    {
        buttonSwitch[currentIndexSwitch].SetActive(false);
        placeholderSwitch[currentIndexSwitch].SetActive(false);
        currentIndexSwitch++;

        if (currentIndexSwitch >= buttonSwitch.Length)
        {
            currentIndexSwitch = 0;
        }

        buttonSwitch[currentIndexSwitch].SetActive(true);
        placeholderSwitch[currentIndexSwitch].SetActive(true);



        currentShipPrefab = 0;
        currentWeaponPrefab = 0;
        SwitchPrefabs();
    }

    public void SwitchLeft()
    {
        buttonSwitch[currentIndexSwitch].SetActive(false);
        placeholderSwitch[currentIndexSwitch].SetActive(false);
        currentIndexSwitch--;

        if (currentIndexSwitch < 0)
        {
            currentIndexSwitch = buttonSwitch.Length - 1;
        }

        buttonSwitch[currentIndexSwitch].SetActive(true);
        placeholderSwitch[currentIndexSwitch].SetActive(true);


        currentShipPrefab = 0;
        currentWeaponPrefab = 0;
        SwitchPrefabs();
    }
}
