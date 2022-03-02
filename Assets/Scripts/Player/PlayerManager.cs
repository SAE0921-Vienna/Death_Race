using System.Globalization;
using AI;
using UnityEngine;
using UserInterface;
using UnityEngine.UI;
using PlayerController;

public class PlayerManager : BaseVehicleManager
{
    private UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();
        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.LogWarning("UIManager NOT Found");
        }
        if (!_gameManager.ghostMode)
        {
            uiManager.healthSlider.maxValue = healthLimit;
            uiManager.healthSlider.value = health;
        }


    }

    protected override void Update()
    {
        base.Update();
        UpdateUIValues();

    }

    private void UpdateUIValues()
    {
        if (!uiManager) return;
        if (uiManager.speedUnit != null)
        {
            uiManager.speedUnit.text = currentSpeed.ToString(CultureInfo.InvariantCulture);
        }
        uiManager.healthSlider.value = health;


    }

    /// <summary>
    /// Activates cheat and gives the player unlimited ammo
    /// </summary>
    public void ToggleUnlimitedAmmo()
    {
        unlimitedAmmo = !unlimitedAmmo;
        if (unlimitedAmmo)
        {
            if (uiManager.ammoToggle)
            {
                uiManager.ammoToggle.GetComponent<Toggle>().isOn = unlimitedAmmo;
                uiManager.ammoToggle.GetChild(1).GetComponent<Image>().enabled = unlimitedAmmo;
            }

            ammo = 999999999;
            uiManager.ammoAmountUI.gameObject.SetActive(unlimitedAmmo);
            canShoot = unlimitedAmmo;
        }
        else
        {
            if (uiManager.ammoToggle)
            {
                uiManager.ammoToggle.GetComponent<Toggle>().isOn = unlimitedAmmo;
                uiManager.ammoToggle.GetChild(1).GetComponent<Image>().enabled = unlimitedAmmo;
            }

            ammo = 0;
            AddAmmoOnStart();
            uiManager.ammoAmountUI.gameObject.SetActive(true);
            canShoot = true;

        }
    }

    /// <summary>
    /// Activates cheat and gives the player unlimited health
    /// </summary>
    public void ToggleUnlimitedHealth()
    {

        unlimitedHealth = !unlimitedHealth;
        if (unlimitedHealth)
        {
            if (uiManager.healthToggle)
            {
                uiManager.healthToggle.GetComponent<Toggle>().isOn = unlimitedHealth;
                uiManager.healthToggle.GetChild(1).GetComponent<Image>().enabled = unlimitedHealth;
            }

            isImmortal = unlimitedHealth;
        }
        else
        {
            if (uiManager.healthToggle)
            {
                uiManager.healthToggle.GetComponent<Toggle>().isOn = unlimitedHealth;
                uiManager.healthToggle.GetChild(1).GetComponent<Image>().enabled = unlimitedHealth;
            }

            isImmortal = unlimitedHealth;

        }
    }

    /// <summary>
    /// Activates cheat and gives the player no speed limit
    /// </summary>
    public void ToggleNoSpeedLimit()
    {
        noSpeedLimit = !noSpeedLimit;
        if (noSpeedLimit)
        {
            if (uiManager.noSpeedLimitToggle)
            {
                uiManager.noSpeedLimitToggle.GetComponent<Toggle>().isOn = noSpeedLimit;
                uiManager.noSpeedLimitToggle.GetChild(1).GetComponent<Image>().enabled = noSpeedLimit;
            }

            var _vehicleController = GetComponent<VehicleController>();
            _vehicleController.mMaxSpeed = 9999f;
            _vehicleController.mAccelerationConstant = 0.3f;
            _vehicleController.steeringSpeed = 12f;
            _vehicleController.speedDependentAngularDragMagnitude = 5f;
        }
        else
        {
            if (uiManager.noSpeedLimitToggle)
            {
                uiManager.noSpeedLimitToggle.GetComponent<Toggle>().isOn = noSpeedLimit;
                uiManager.noSpeedLimitToggle.GetChild(1).GetComponent<Image>().enabled = noSpeedLimit;
            }

            var spaceshipLoad = GetComponent<SpaceshipLoad>();
            spaceshipLoad.SetVehicleStats();
        }
    }

    //public void CheckLapCount()
    //{
    //    if (currentLapIndex > _gameManager.laps && !_gameManager.ghostMode)
    //    {
    //        currentLapIndex = _gameManager.laps;
    //        _gameManager.raceFinished = true;
    //        _gameManager.gameOverCanvas.gameObject.SetActive(true);
    //        
    //        //for (int i = 0; i < finishLineManager.checkpointParent.childCount; i++)
    //        //{
    //        //    Checkpoint checkpoint = finishLineManager.checkpointParent.GetChild(i).GetComponent<Checkpoint>();
    //        //    if (checkpoint.transform.GetChild(0))
    //        //    {
    //        //        checkpoint.transform.GetChild(0).gameObject.SetActive(false);
    //        //    }
    //        //}
    //    }
    //    if (currentLapIndex < 0)
    //    {
    //        currentLapIndex = 0;
    //    }
    //}
}
