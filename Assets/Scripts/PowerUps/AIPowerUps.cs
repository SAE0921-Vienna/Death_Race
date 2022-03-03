using UnityEngine;
using AI;

public class AIPowerUps : PowerUps
{
    AI_Action aiaction;

    /// <summary>
    /// Get the important information.
    /// </summary>
    private void Awake()
    {
        _vehicleStats = GetComponent<BaseVehicleManager>();
        aiaction = GetComponent<AI_Action>();

        powerupParent = transform.GetChild(0);
    }

    private void Update()
    {
        _vehicleStats.timer -= Time.deltaTime;

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
            ResetPowerUps();
        }
        if (powerUp != null)
        {
            //ActivatePowerUp(powerUp);
            aiaction.CheckPowerUp(powerUp);
        }
    }

    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    public void ActivatePowerUp(PickUpScriptableObject powerUp)
    {
        //aiaction.CheckPowerUp(powerUp);

        Debug.Log(gameObject.name + " AIPOWERUP");
        powerUp.PowerUpAction(this.gameObject);
        powerUpList.Remove(this.powerUp);
        this.powerUp = null;
    }


}
