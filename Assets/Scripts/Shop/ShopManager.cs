using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    [ContextMenuItem(name: "Reset Money", function: "ResetMoney")]
    [ContextMenuItem(name: "Infinite Money", function: "InfiniteMoney")]

    public SpaceShipConfigurator spaceShipConfigurator;
    public GarageManager garageManager;

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

    private void Awake()
    {
        GetElementsGManager(); // for now for testing
        automaticRotation = false;
        timer = timerCooldown;

        garageManager.shipShopName.text = shipsShop[currentShipPrefab].shipData.name;
        garageManager.shipStatsShop.text = shipsShop[currentShipPrefab].shipData.GetShipPrice() + "\n" + shipsShop[currentShipPrefab].shipData.GetShipStats();
        SetShipStatSliders();

        if (shipsShop[currentShipPrefab].shipBought)
        {
            garageManager.buttonBuyShip.interactable = false;
            garageManager.buttonBuyShip.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
        else
        {
            garageManager.buttonBuyShip.interactable = true;
            garageManager.buttonBuyShip.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }

        garageManager.materialPrice.text = materialsShop[currentMaterial].materialData.GetPrice();

        if (!saveLoadScript.hasMoneyData)
        {
            saveLoadScript.SaveMoneyData();
        }

        ChangeMoneyUI();

    }

    private void Update()
    {
        if (automaticRotation)
        {
            AutomaticSWRotation();
        }

    }

    public void ChangeMoneyUI()
    {
        if (saveLoadScript && garageManager)
        {
            saveLoadScript.LoadMoneyData();
            garageManager.milkyCoins.text = saveLoadScript.milkyCoins.ToString();
            if (saveLoadScript.milkyCoins > int.Parse(garageManager.milkyCoins.text))
            {
                garageManager.milkyCoins.text = garageManager.maxCoinsUI;
            }

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
                //weaponClone.GetComponentInChildren<WeaponRotator>().enabled = false;
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


    public void SetShipStatSliders()
    {
        healthSlider.value = shipsShop[currentShipPrefab].shipData.health;
        maxSpeedSlider.value = shipsShop[currentShipPrefab].shipData.maxSpeed;
        acceleration.value = shipsShop[currentShipPrefab].shipData.accelerationSpeed;
        turnSpeed.value = shipsShop[currentShipPrefab].shipData.turnSpeed;
        angularDrag.value = shipsShop[currentShipPrefab].shipData.speedBasedAngularDrag;
    }

    public void SetWeaponStatSliders()
    {
        damageSlider.value = weaponsShop[currentWeaponPrefab].weaponData.damage;
        ammoSlider.value = weaponsShop[currentWeaponPrefab].weaponData.ammoSize;
        firerateSlider.value = weaponsShop[currentWeaponPrefab].weaponData.fireRate;
    }



    public void GetElementsGManager()
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

    public void BuyShip()
    {
        if (saveLoadScript)
        {
            saveLoadScript.milkyCoins -= shipsShop[currentShipPrefab].shipData.priceInShop;
            saveLoadScript.boughtShips[currentShipPrefab] = true;
            shipsShop[currentShipPrefab].shipBought = true;
            saveLoadScript.SaveSaveData();
            saveLoadScript.SaveMoneyData();
            ChangeMoneyUI();
            garageManager.buttonBuyShip.interactable = false;
            garageManager.buttonBuyShip.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
    }

    public void BuyWeapon()
    {
        if (saveLoadScript)
        {
            saveLoadScript.milkyCoins -= weaponsShop[currentWeaponPrefab].weaponData.priceInShop;
            saveLoadScript.boughtWeapons[currentWeaponPrefab] = true;
            weaponsShop[currentWeaponPrefab].weaponBought = true;
            saveLoadScript.SaveSaveData();
            saveLoadScript.SaveMoneyData();
            ChangeMoneyUI();
            garageManager.buttonBuyWeapon.interactable = false;
            garageManager.buttonBuyWeapon.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
    }

    public void BuyMaterial()
    {
        if (saveLoadScript)
        {
            saveLoadScript.milkyCoins -= materialsShop[currentMaterial].materialData.priceInShop;
            saveLoadScript.boughtMaterials[currentMaterial] = true;
            materialsShop[currentMaterial].materialBought = true;
            saveLoadScript.SaveSaveData();
            saveLoadScript.SaveMoneyData();
            ChangeMoneyUI();
            garageManager.buttonBuyMaterial.interactable = false;
            garageManager.buttonBuyMaterial.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
    }


    public void SwitchPrefabs()
    {

        switch (currentIndexSwitch)
        {
            case 0:
                ChangeShipShop();
                automaticRotation = false;
                break;
            case 1:
                Destroy(weaponClone);
                ChangeWeaponShop();
                automaticRotation = false;
                break;
            case 2:
                //currentSW = 0;
                automaticRotation = true;
                timer = 0;
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

        garageManager.shipStatsShop.text = shipsShop[currentShipPrefab].shipData.GetShipPrice() + "\n" + shipsShop[currentShipPrefab].shipData.GetShipStats();
        SetShipStatSliders();

        if (shipsShop[currentShipPrefab].shipBought)
        {
            garageManager.buttonBuyShip.interactable = false;
            garageManager.buttonBuyShip.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";


        }
        else
        {
            garageManager.buttonBuyShip.interactable = true;
            garageManager.buttonBuyShip.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
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
        //weaponClone.GetComponentInChildren<WeaponRotator>().enabled = false;
        weaponClone.GetComponent<MeshRenderer>().material = hologramMAT;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;
        if(weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material != null)
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;
        weaponName.text = weaponsShop[currentWeaponPrefab].weaponData.name;

        garageManager.weaponStatsShop.text = weaponsShop[currentWeaponPrefab].weaponData.GetWeaponPrice() + "\n" + weaponsShop[currentWeaponPrefab].weaponData.GetWeaponStats();
        SetWeaponStatSliders();


        if (weaponsShop[currentWeaponPrefab].weaponBought)
        {
            garageManager.buttonBuyWeapon.interactable = false;
            garageManager.buttonBuyWeapon.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";


        }
        else
        {
            garageManager.buttonBuyWeapon.interactable = true;
            garageManager.buttonBuyWeapon.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }

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
        garageManager.materialPrice.text = materialsShop[currentMaterial].materialData.GetPrice();


        if (materialsShop[currentMaterial].materialBought)
        {
            garageManager.buttonBuyMaterial.interactable = false;
            garageManager.buttonBuyMaterial.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
        else
        {
            garageManager.buttonBuyMaterial.interactable = true;
            garageManager.buttonBuyMaterial.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
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
        garageManager.materialPrice.text = materialsShop[currentMaterial].materialData.GetPrice();


        if (materialsShop[currentMaterial].materialBought)
        {
            garageManager.buttonBuyMaterial.interactable = false;
            garageManager.buttonBuyMaterial.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
        }
        else
        {
            garageManager.buttonBuyMaterial.interactable = true;
            garageManager.buttonBuyMaterial.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
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


    /// <summary>
    /// Resets all the money
    /// </summary>
    [ContextMenu(itemName: "Reset Money")]
    public void ResetMoney()
    {
        saveLoadScript.milkyCoins = 0;
        saveLoadScript.SaveMoneyData();
        ChangeMoneyUI();

    }

    /// <summary>
    /// Gives the player infinite money
    /// </summary>
    [ContextMenu(itemName: "Infinite Money")]
    public void InfiniteMoney()
    {
        saveLoadScript.milkyCoins = saveLoadScript.maxMilkyCoins;
        saveLoadScript.SaveMoneyData();
        ChangeMoneyUI();
    }
}
