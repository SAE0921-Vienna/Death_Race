using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public List<ScriptableObject> powerUpList;
    private PickUpScriptableObject powerUp;
    private UIManager uiManager;
    private PlayerManager playerStats;
    private Transform powerupParent;
    private int powerUpListLength = 1;


    private void Awake()
    {
        playerStats = GetComponent<PlayerManager>();
        uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        powerupParent = transform.GetChild(0);

        playerStats.normalMaxSpeed = GetComponent<VehicleController>().mMaxSpeed;
    }


    private void Update()
    {
        playerStats.timer -= Time.deltaTime;

        if (uiManager.ammoAmountUI != null)
        {
            uiManager.ammoAmountUI.text = playerStats.ammo.ToString();
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
        uiManager.powerUpUI.color = new Color(0, 0, 0, 0);
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
        uiManager.powerUpUI.sprite = powerUp.icon;
        uiManager.powerUpUI.color = new Color(1, 1, 1, 1);
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
            playerStats.isImmortal = false;
            powerupParent.GetChild(0).gameObject.SetActive(false);


        }
        if (playerStats.nitro)
        {
            playerStats.nitro = false;
            powerupParent.GetChild(1).gameObject.SetActive(false);
            GetComponent<VehicleController>().mMaxSpeed = playerStats.normalMaxSpeed;
        }
        if (playerStats.canShoot && playerStats.ammo <= 0)
        {
            playerStats.canShoot = false;
            uiManager.ammoAmountUI.gameObject.SetActive(false);

        }
        if (playerStats.bomb)
        {
            playerStats.bomb = false;
        }
        if (playerStats.timeSlowed)
        {
            playerStats.timeSlowed = false;
        }
    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        playerStats.isImmortal = true;
        powerupParent.GetChild(0).gameObject.SetActive(true);
        playerStats.shield = true;
    }

    public void NitroPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        powerupParent.GetChild(1).gameObject.SetActive(true);
        GetComponent<VehicleController>().mMaxSpeed += playerStats.nitroSpeed;
        playerStats.nitro = true;
    }

    public void AmmoPowerUp()
    {
        uiManager.ammoAmountUI.gameObject.SetActive(true);
        if (playerStats.ammo < playerStats.ammoLimit)
        {
            playerStats.ammo += playerStats.ammoAdd;
            if (playerStats.ammo > playerStats.ammoLimit) playerStats.ammo = playerStats.ammoLimit;
        }
        uiManager.ammoAmountUI.text = playerStats.ammo.ToString();
        playerStats.canShoot = true;
    }

    public void BombPowerUp()
    {
        GameObject bombClone = Instantiate(powerUp.powerUpPrefab, powerupParent.GetChild(2).transform.position, Quaternion.identity);
        bombClone.transform.localScale = playerStats.bombScale;
        bombClone.AddComponent<Rigidbody>();
        bombClone.AddComponent<SphereCollider>();
        bombClone.GetComponent<BombTrigger>().hasBeenActivated = true;
        playerStats.bomb = true;
    }

    //public void SlowPowerUp()
    //{
    //    playerStats.timer = playerStats.timerCooldown;
    //    Time.timeScale = playerStats.slowTimeValue;
    //    playerStats.timeSlowed = true;

    //}

    #endregion
}
