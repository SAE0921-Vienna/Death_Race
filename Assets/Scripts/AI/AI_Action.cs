using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using UserInterface;
using Weapons;

public class AI_Action : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private PlayerManager playerManager;
    private PlayerWeapon playerWeapon;
    private VehicleController vehicleController;
    private ShipWeapon shipweapon;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        fieldOfView = GetComponent<FieldOfView>();
        vehicleController = GetComponent<VehicleController>();
        playerWeapon = GetComponent<PlayerWeapon>();
        shipweapon = GetComponent<ShipWeapon>();
    }
    void Update()
    {
        if (fieldOfView.nearestObject)
        {
            ShootingAI();
            Debug.Log("BLYAT");
        }
    }
    private void ShootingAI()
    {
        shipweapon.Shoot();
        Debug.Log("SUUKA");
    }
}
