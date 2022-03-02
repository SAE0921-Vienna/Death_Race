using System.Globalization;
using AI;
using UnityEngine;
using UserInterface;
using UnityEngine.UI;

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
