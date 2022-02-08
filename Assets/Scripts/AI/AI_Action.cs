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

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        fieldOfView = GetComponent<FieldOfView>();
        vehicleController = GetComponent<VehicleController>();
        playerWeapon = GetComponent<PlayerWeapon>();
    }
    void Update()
    {
        if (fieldOfView.nearestObject)
        {
            Debug.Log("AI Shooting");
            ShootingAI();
        }
    }
    private void ShootingAI()
    {
        //playerWeapon.Shoot();
    }
}
