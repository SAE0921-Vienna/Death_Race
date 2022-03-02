using UnityEngine;
using AI;

public class AI_Action : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private BaseVehicleManager baseVehicleManager;
    private ShipWeapon shipweapon;
    private AIPowerUps aiPowerUps;

    /// <summary>
    /// Get the important information.
    /// </summary>
    private void Awake()
    {
        baseVehicleManager = GetComponent<BaseVehicleManager>();
        fieldOfView = GetComponent<FieldOfView>();
        shipweapon = GetComponent<ShipWeapon>();
        aiPowerUps = GetComponent<AIPowerUps>();
    }
    void Update()
    {
        if (fieldOfView.nearestObject)
        {
            ShootingAI();
            Debug.Log(gameObject.name + " schieﬂt auf " + fieldOfView.nearestObject.gameObject.name);
        }

    }
    /// <summary>
    /// Executes the Shoot method.
    /// </summary>
    private void ShootingAI()
    {
        shipweapon.Shoot();
    }

    /// <summary>
    /// Checks which PowerUps have been picked up and applies small behaviors.
    /// </summary>
    public void CheckPowerUp(PickUpScriptableObject powerUp)
    {
        switch (powerUp.powerUpType)
        {
            case PickUpScriptableObject.powerUps.Shield:
                if(baseVehicleManager.health < 70)
                aiPowerUps.ActivatePowerUp(powerUp);
                break;
            case PickUpScriptableObject.powerUps.Nitro:
                aiPowerUps.ActivatePowerUp(powerUp);
                break;
            case PickUpScriptableObject.powerUps.Ammo:
                aiPowerUps.ActivatePowerUp(powerUp);
                break;
            case PickUpScriptableObject.powerUps.Bomb:
                aiPowerUps.ActivatePowerUp(powerUp);
                break;
            case PickUpScriptableObject.powerUps.Heal:
                if (baseVehicleManager.health < 70)
                    aiPowerUps.ActivatePowerUp(powerUp);
                break;
            default:
                Debug.LogWarning("Oops, something went wrong with AI PowerUps");
                break;
        }
    }
}
