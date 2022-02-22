using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class AIPowerUps : PowerUps
{
    private  void Awake()
    {
        _vehicleStats = GetComponent<BaseVehicleManager>();
    }

    private void Update()
    {
        _vehicleStats.timer -= Time.deltaTime;

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
            ResetPowerUps();
        }

        //if (powerUp != null)
        //{
        //    ActivatePowerUp(powerUp);
        //}

      
    }


}
