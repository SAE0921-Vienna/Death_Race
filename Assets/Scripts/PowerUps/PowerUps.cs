using System.Collections.Generic;
using PlayerController;
using UnityEngine;
using UserInterface;


public class PowerUps : MonoBehaviour
{
    public List<ScriptableObject> powerUpList;
    private PickUpScriptableObject powerUp;
    private UIManager uIManager;
    //private PlayerWeapon playerWeapon;
    private PlayerManager playerStats;
    private GameManager gameManager;
    private Transform powerupParent;
    private int powerUpListLength = 1;



    private void Awake()
    {
        playerStats = GetComponent<PlayerManager>();

        #region GameManager  FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("GameManager NOT Found");

        }
        #endregion

        #region uiManager  FindObjectOfType
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager)
        {
            //UIManager Found
        }
        else
        {
            Debug.LogWarning("UIManager NOT Found");
        }
        #endregion


        powerupParent = transform.GetChild(0);
        playerStats.normalMaxSpeed = GetComponent<VehicleController>().mMaxSpeed;
    }


    private void Update()
    {
        playerStats.timer -= Time.deltaTime;
        if (uIManager)
        {
            if (uIManager.ammoAmountUI != null)
            {
                uIManager.ammoAmountUI.text = playerStats.ammo.ToString();
            }
            if (playerStats.ammo <= 0)
            {
                playerStats.ammo = 0;
                uIManager.ammoAmountUI.text = playerStats.ammo.ToString();
            }
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
        uIManager.powerUpUI.color = new Color(0, 0, 0, 0);
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
        uIManager.powerUpUI.sprite = powerUp.icon;
        uIManager.powerUpUI.color = new Color(1, 1, 1, 1);
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
            gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV;
            VehicleController vehicleContr = GetComponent<VehicleController>();
            vehicleContr.mMaxSpeed -= playerStats.nitroSpeed;
            vehicleContr.mAccelerationConstant -= playerStats.nitroAccelerationBoost;
        }
        if (playerStats.ammo <= 0 && uIManager)
        {
            playerStats.canShoot = false;
            playerStats.ammo = 0;
            uIManager.ammoAmountUI.gameObject.SetActive(false);

        }
        if (playerStats.bomb)
        {
            playerStats.bomb = false;
        }



    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        powerupParent.GetChild(0).gameObject.SetActive(true);
        playerStats.isImmortal = true;
        playerStats.shield = true;
    }

    public void NitroPowerUp()
    {
        playerStats.timer = playerStats.timerCooldown;
        powerupParent.GetChild(1).gameObject.SetActive(true);
        gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV + playerStats.mainCamPovBoost;
        //gameManager.vCam.m_Lens.FieldOfView = Mathf.Lerp(gameManager.vCamPOV, gameManager.vCamPOV + playerStats.mainCamPovBoost, Time.deltaTime);
        VehicleController vehicleContr = GetComponent<VehicleController>();
        vehicleContr.mMaxSpeed += playerStats.nitroSpeed;
        vehicleContr.mAccelerationConstant += playerStats.nitroAccelerationBoost;

        playerStats.nitro = true;
    }

    public void AmmoPowerUp()
    {
        uIManager.ammoAmountUI.gameObject.SetActive(true);
        if (playerStats.ammo < playerStats.ammoLimit)
        {
            playerStats.ammo += playerStats.ammoAdd;
            if (playerStats.ammo > playerStats.ammoLimit) playerStats.ammo = playerStats.ammoLimit;
        }
        uIManager.ammoAmountUI.text = playerStats.ammo.ToString();
        playerStats.canShoot = true;
    }

    public void BombPowerUp()
    {
        GameObject bombClone = Instantiate(powerUp.powerUpPrefab, powerupParent.GetChild(2).transform.position, Quaternion.identity);
        bombClone.transform.localScale = playerStats.bombScale;
        bombClone.GetComponent<SphereCollider>().enabled = true;
        //bombClone.GetComponent<Rigidbody>().useGravity = true;
        bombClone.GetComponent<BombTrigger>().hasBeenActivated = true;
        playerStats.bomb = true;
    }

    public void HealPowerUp()
    {
        playerStats.health += playerStats.healValue;
        if (playerStats.health >= playerStats.healthLimit)
        {
            playerStats.health = playerStats.healthLimit;
        }
    }

    #endregion
}
