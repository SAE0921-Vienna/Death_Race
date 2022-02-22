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
    private AIPowerUps aiPowerUps;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        fieldOfView = GetComponent<FieldOfView>();
        vehicleController = GetComponent<VehicleController>();
        playerWeapon = GetComponent<PlayerWeapon>();
        shipweapon = GetComponent<ShipWeapon>();
        aiPowerUps = GetComponent<AIPowerUps>();
    }
    void Update()
    {
        if (fieldOfView.nearestObject)
        {
            ShootingAI();
            Debug.Log(this.gameObject.name + " shootet auf " + fieldOfView.nearestObject.gameObject.name);
        }
        if (aiPowerUps.powerUp != null)
        {
            CheckPowerUp();
            aiPowerUps.ActivatePowerUp(aiPowerUps.powerUp);
        }
    }
    private void ShootingAI()
    {
        shipweapon.Shoot();
    }

    private void CheckPowerUp()
    {
        switch (aiPowerUps.powerUp//Enum)
        {
            //case aiPowerUps.powerUp.
        }
    }
}
