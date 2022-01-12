using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private Garagemanager garagemanager;
    [SerializeField] private GameObject ships;
    [SerializeField] private GameObject ship;
    [SerializeField] private Material shipColor;
    [SerializeField] private GameObject shipweapon;

    private GameObject lastship;
    private Material lastshipColor;
    private GameObject lastshipweapon;

    private void Awake()
    {
        if(ship == null)
        {
            ship = garagemanager.shipsConfig[0].ship;
            shipColor = garagemanager.shipsConfig[0].shipColor[0].shipColor;
            shipweapon = garagemanager.shipWeapon[0].shipWeapon;
        }
    }
}
