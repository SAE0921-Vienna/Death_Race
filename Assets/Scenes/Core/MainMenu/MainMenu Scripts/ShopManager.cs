using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int MilkyCoins;
    public int StarCoins;

    private Garagemanager garagemanager;
    public Material hologramMAT;

    [Header("Ship")]
    public GameObject hologramShipsPlaceholder;
    [SerializeField]
    private Vector3 hologramShipScale = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("Weapon")]
    public GameObject hologramWeaponsPlaceholder;
    [SerializeField]
    private Vector3 hologramWeaponsScale = new Vector3(0.4f, 0.4f, 0.4f);




    private void Awake()
    {
        garagemanager = FindObjectOfType<Garagemanager>();
        if (!garagemanager)
        {
            Debug.LogWarning("GarageManager was NOT found");
        }

        //Ship
        for (int i = 0; i <= garagemanager.shipsConfig.Count - 1; i++)
        {
            GameObject clone = Instantiate(garagemanager.shipsConfig[i].ship, hologramShipsPlaceholder.transform.position, hologramShipsPlaceholder.transform.rotation);
            clone.transform.parent = hologramShipsPlaceholder.transform;
            clone.transform.localScale = hologramShipScale;
            clone.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;
            Destroy(clone.transform.GetChild(1).gameObject);
        }

        //Weapon
        for (int j = 0; j <= garagemanager.shipWeapon.Count - 1; j++)
        {
            GameObject clone = Instantiate(garagemanager.shipWeapon[j].shipWeapon, hologramWeaponsPlaceholder.transform.position, hologramWeaponsPlaceholder.transform.rotation);
            clone.transform.parent = hologramWeaponsPlaceholder.transform;
            clone.transform.localScale = hologramWeaponsScale;

            clone.transform.GetComponent<MeshRenderer>().material = hologramMAT;

            if (clone.transform.GetChild(0).childCount > 0)
            {
                clone.transform.GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;

                clone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = hologramMAT;

            }
        }

    }



}
