using UnityEngine;
using AI;
using Audio;

public class AIPowerUps : PowerUps
{
    AIFollowCurve aIFollowCurve;

    /// <summary>
    /// Get the important information.
    /// </summary>
    private void Awake()
    {
        _vehicleStats = GetComponent<BaseVehicleManager>();

        powerupParent = transform.GetChild(0);

        aIFollowCurve = GetComponent<AIFollowCurve>();

        normalSpeed = aIFollowCurve.MaxSpeed;

        nitroSpeedModifier = (aIFollowCurve.MaxSpeed / 100) * 20f;

    }

    private void Update()
    {
        _vehicleStats.timer -= Time.deltaTime;

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
            ResetPowerUps();
        }

    }

    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    public void ActivatePowerUp(PickUpScriptableObject powerUp)
    {
        Debug.Log(gameObject.name + " AIPOWERUP");
        powerUp.PowerUpAction(this.gameObject);
        powerUpList.Remove(powerUp);
        this.powerUp = null;
    }



}
