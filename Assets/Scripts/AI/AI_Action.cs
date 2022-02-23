using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using UserInterface;
using Weapons;
using AI;

public class AI_Action : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private BaseVehicleManager baseVehicleManager;
    private PlayerWeapon playerWeapon;
    private VehicleController vehicleController;
    private ShipWeapon shipweapon;
    private AIPowerUps aiPowerUps;

    private void Awake()
    {
        baseVehicleManager = GetComponent<BaseVehicleManager>();
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

    }
    private void ShootingAI()
    {
        shipweapon.Shoot();
    }

    public void CheckPowerUp(PickUpScriptableObject powerUp)
    {
        switch (powerUp.powerUpType)
        {
            case PickUpScriptableObject.powerUps.Shield:
                //Function
                //if()
                aiPowerUps.ActivatePowerUp(powerUp);
                break;
            case PickUpScriptableObject.powerUps.Nitro:
                break;
            case PickUpScriptableObject.powerUps.Ammo:
                break;
            case PickUpScriptableObject.powerUps.Bomb:
                break;
            case PickUpScriptableObject.powerUps.Heal:
                break;
            case PickUpScriptableObject.powerUps.Random:
                break;
            default:
                break;
        }
    }
}
