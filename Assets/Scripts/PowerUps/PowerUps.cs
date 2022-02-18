using System.Collections.Generic;
using AI;
using PlayerController;
using UnityEngine;
using UserInterface;


public class PowerUps : MonoBehaviour
{
    [SerializeField] private float nitroSpeedModifier;
    [SerializeField] private float nitroAccelerationModifier;
    [SerializeField] private float nitroFovModifier;

    public List<ScriptableObject> powerUpList;
    public PickUpScriptableObject powerUp;
    private UIManager uIManager;
    //private PlayerWeapon playerWeapon;
    private BaseVehicleManager _vehicleStats;
    private GameManager gameManager;
    private Transform powerupParent;
    private PlayerShipWeapon playerShipWeapon;
    private int powerUpListLength = 1;

    private void Awake()
    {
        _vehicleStats = GetComponent<BaseVehicleManager>();

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("GameManager NOT Found");
        }
        
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager)
        {
            //UIManager Found
        }
        else
        {
            Debug.LogWarning("UIManager NOT Found");
        }
        powerupParent = transform.GetChild(0);
    }


    private void Update()
    {
        _vehicleStats.timer -= Time.deltaTime;
        if (uIManager)
        {
            if (uIManager.ammoAmountUI != null)
            {
                uIManager.ammoAmountUI.text = _vehicleStats.ammo.ToString();
            }
            if (_vehicleStats.ammo <= 0)
            {
                _vehicleStats.ammo = 0;
                uIManager.ammoAmountUI.text = _vehicleStats.ammo.ToString();
            }
        }

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
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

        if (_vehicleStats.hasShield)
        {
            _vehicleStats.hasShield = false;
            _vehicleStats.isImmortal = false;
            powerupParent.GetChild(0).gameObject.SetActive(false);
        }
        if (_vehicleStats.hasNitro)
        {
            _vehicleStats.hasNitro = false;
            powerupParent.GetChild(1).gameObject.SetActive(false);
            gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV;
            VehicleController vehicleContr = GetComponent<VehicleController>();
            vehicleContr.mMaxSpeed -= nitroSpeedModifier;
            vehicleContr.mAccelerationConstant -= nitroAccelerationModifier;
        }
        if (_vehicleStats.ammo <= 0 && uIManager)
        {
            _vehicleStats.canShoot = false;
            _vehicleStats.ammo = 0;
            uIManager.ammoAmountUI.gameObject.SetActive(false);

        }
        if (_vehicleStats.hasBomb)
        {
            _vehicleStats.hasBomb = false;
        }
    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        powerupParent.GetChild(0).gameObject.SetActive(true);
        _vehicleStats.isImmortal = true;
        _vehicleStats.hasShield = true;
    }

    public void NitroPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        powerupParent.GetChild(1).gameObject.SetActive(true);
        gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV + nitroFovModifier;
        //gameManager.vCam.m_Lens.FieldOfView = Mathf.Lerp(gameManager.vCamPOV, gameManager.vCamPOV + playerStats.mainCamPovBoost, Time.deltaTime);
        VehicleController vehicleContr = GetComponent<VehicleController>();
        vehicleContr.mMaxSpeed += nitroSpeedModifier;
        vehicleContr.mAccelerationConstant += nitroAccelerationModifier;

        _vehicleStats.hasNitro = true;
    }

    public void AmmoPowerUp()
    {
        uIManager.ammoAmountUI.gameObject.SetActive(true);
        if (_vehicleStats.ammo < _vehicleStats.ammoLimit)
        {
            _vehicleStats.ammo += _vehicleStats.ammoAdd;
            if (_vehicleStats.ammo > _vehicleStats.ammoLimit) _vehicleStats.ammo = _vehicleStats.ammoLimit;
        }
        uIManager.ammoAmountUI.text = _vehicleStats.ammo.ToString();
        _vehicleStats.canShoot = true;
    }

    public void BombPowerUp()
    {
        GameObject bombClone = Instantiate(powerUp.powerUpPrefab, powerupParent.GetChild(2).transform.position, Quaternion.identity);
        bombClone.transform.localScale = _vehicleStats.bombScale;
        bombClone.GetComponent<SphereCollider>().enabled = true;
        //bombClone.GetComponent<Rigidbody>().useGravity = true;
        bombClone.GetComponent<BombTrigger>().hasBeenActivated = true;
        _vehicleStats.hasBomb = true;
    }

    public void HealPowerUp()
    {
        _vehicleStats.health += _vehicleStats.healValue;
        if (_vehicleStats.health >= _vehicleStats.healthLimit)
        {
            _vehicleStats.health = _vehicleStats.healthLimit;
        }
    }

    #endregion
}
