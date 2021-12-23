using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps : MonoBehaviour
{
    public List<ScriptableObject> powerUpList;
    private PickUpScriptableObject powerUp;
    //private PlayerWeapon playerWeapon;
    private PlayerStats playerStats;
    private Transform powerupParent;
    private int powerUpListLength = 1;

    public Image powerUpUI;
    public TextMeshProUGUI ammoAmountUI;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        powerupParent = transform.GetChild(0);

        playerStats.normalMaxSpeed = GetComponent<VehicleController>().mMaxSpeed;

    }


    private void Update()
    {
        playerStats.timer -= Time.deltaTime;

        if (ammoAmountUI != null)
        {
            ammoAmountUI.text = playerStats.ammo.ToString();
        }

        if (playerStats.timer < 0)
        {
            playerStats.timer = -1;
            ResetPowerUps();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && powerUp != null)
        {
            ActivatePowerUp(powerUp);
        }

    }

    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    public void ActivatePowerUp(PickUpScriptableObject powerUp)
    {

        powerUpList.Remove(powerUp);
        powerUpUI.color = new Color(0, 0, 0, 0);
        powerUp.PowerUpAction(this.gameObject);
        this.powerUp = null;
    }


    /// <summary>
    /// Adds the powerUp to the list
    /// </summary>
    /// <param name="powerUp"></param>
    public void AddToPowerUpList(PickUpScriptableObject powerUp)
    {
        if (powerUpList.Count >= powerUpListLength)
        {
            powerUpList.Clear();
        }
        powerUpList.Add(powerUp);
        powerUpUI.sprite = powerUp.icon;
        powerUpUI.color = new Color(1, 1, 1, 1);
        this.powerUp = powerUp;

    }

    /// <summary>
    /// Resets the powerups after some time
    /// </summary>
    public void ResetPowerUps()
    {

        if (playerStats.shield)
        {
            playerStats.shield = false;
            powerupParent.GetChild(0).gameObject.SetActive(false);

        }
        if (playerStats.nitro)
        {
            playerStats.nitro = false;
            powerupParent.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
            GetComponent<VehicleController>().mMaxSpeed = playerStats.normalMaxSpeed;
        }
        if (playerStats.canShoot && playerStats.ammo <= 0)
        {
            playerStats.canShoot = false;
            ammoAmountUI.gameObject.SetActive(false);

        }
    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        powerupParent.GetChild(0).gameObject.SetActive(true);
        playerStats.shield = true;
    }

    public void NitroPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        powerupParent.GetChild(1).gameObject.SetActive(true);
        //transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        GetComponent<VehicleController>().mMaxSpeed += playerStats.nitroSpeed;
        playerStats.nitro = true;
    }

    public void AmmoPowerUp()
    {
        ammoAmountUI.gameObject.SetActive(true);
        if (playerStats.ammo < playerStats.ammoLimit)
        {
            playerStats.ammo += playerStats.ammoAdd;
            if (playerStats.ammo > playerStats.ammoLimit) playerStats.ammo = playerStats.ammoLimit;
        }
        ammoAmountUI.text = playerStats.ammo.ToString();
        playerStats.canShoot = true;
    }

    #endregion
}
