using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garagemanager : MonoBehaviour
{
    public GameObject ships;
    public Material availableObject;
    public Material unavailableObject;
    private int weaponListCounter = 0;
    private int shipListCounter = 0;

    private GameObject lastship;
    private Material lastshipColor;
    private GameObject lastshipweapon;

    public List<ShipConfig> shipsConfig;
    public List<ShipConfigUniversalColor> ShipuniversalSkin;
    public List<ShipCofigWeapon> shipWeapon;
    
    public void WeaponList(int _leftright) //Child(1) == WeaponPosition 
    {
        if (_leftright == 0)
        {
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(false);
            weaponListCounter--;
            ListCheck("Weapon");
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(true);
        }
        else
        {
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(false);
            weaponListCounter++;
            ListCheck("Weapon");
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(true);
        }
    }
    public void ShipList(int _leftright)
    {
        if (_leftright == 0)
        {
            ships.transform.GetChild(shipListCounter).gameObject.SetActive(false);
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(false);
            shipListCounter--;
            ListCheck("Ship");
            ships.transform.GetChild(shipListCounter).gameObject.SetActive(true);
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(true);
        }
        else
        {
            ships.transform.GetChild(shipListCounter).gameObject.SetActive(false);
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(false);
            shipListCounter++;
            ListCheck("Ship");
            ships.transform.GetChild(shipListCounter).gameObject.SetActive(true);
            ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).gameObject.SetActive(true);
        }
    }
    private void ListCheck(string _name)
    {
        if (_name == "Weapon")
        {
            if (weaponListCounter > ships.transform.GetChild(0).GetChild(1).childCount - 1)
            {
                weaponListCounter = 0;
            }
            else if (weaponListCounter < 0)
            {
                weaponListCounter = ships.transform.GetChild(0).GetChild(1).childCount - 1;
            }
            WeaponBoughtCheck();
        }
        else if (_name == "Ship")
        {
            if (shipListCounter > ships.transform.childCount - 1)
            {
                shipListCounter = 0;
            }
            else if (shipListCounter < 0)
            {
                shipListCounter = ships.transform.childCount - 1;
            }
            if (shipsConfig[shipListCounter].shipBought == false)
            {
                ships.transform.GetChild(shipListCounter).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = unavailableObject;
                ChangeMaterial(unavailableObject);
            }
            else
            {
                ships.transform.GetChild(shipListCounter).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = availableObject;
                WeaponBoughtCheck();
            }
        }
    }
    private void WeaponBoughtCheck()
    {
        if (shipWeapon[weaponListCounter].weaponBought == true && shipsConfig[shipListCounter].shipBought == true)
        {
            ChangeMaterial(availableObject);
        }
        else
        {
            ChangeMaterial(unavailableObject);
        }
    }
    private void ChangeMaterial(Material _material)
    {
        ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).GetComponent<MeshRenderer>().material = _material;
        if (ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).childCount > 0)
        {
            for (int j = 0; j < ships.transform.GetChild(0).GetChild(1).GetChild(weaponListCounter).childCount; j++)
            {
                ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).GetChild(j).GetComponent<MeshRenderer>().material = _material;
                if (ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).GetChild(j).childCount > 0)
                {
                    for (int k = 0; k < ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).childCount; k++)
                    {
                        ships.transform.GetChild(shipListCounter).GetChild(1).GetChild(weaponListCounter).GetChild(j).GetChild(k).GetComponent<MeshRenderer>().material = _material;
                    }
                }
            }
        }
    }
}
[System.Serializable]
public class ShipConfig
{
    public GameObject ship;
    public bool shipBought;
    public List<ShipConfigColor> shipColor;
}
[System.Serializable]
public class ShipConfigColor
{
    public Material shipColor;
    public bool skinBought;

}
[System.Serializable]
public class ShipCofigWeapon
{
    public GameObject shipWeapon;
    public bool weaponBought;
}
[System.Serializable]
public class ShipConfigUniversalColor
{
    public Material shipColor;
    public bool skinBought;
}

