using System.Collections.Generic;
using AI;
using PlayerController;
using UnityEngine;
using UserInterface;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float nitroSpeedModifier = 200f;
    [SerializeField] private float nitroAccelerationModifier = 0.2f;
    [SerializeField] private float nitroFovModifier = 20f;

    public List<ScriptableObject> powerUpList;
    public PickUpScriptableObject powerUp;
    private UIManager uIManager;
    protected BaseVehicleManager _vehicleStats;
    private GameManager gameManager;
    protected Transform powerupParent;
    private int powerUpListLength = 1;

    [Header("Effects")]
    [SerializeField]
    private GameObject shieldEffect;
    [SerializeField]
    private GameObject nitroEffect;
    [SerializeField]
    private GameObject bombEffect;
    [SerializeField]
    private GameObject lowHealthEffect;
    [SerializeField]
    private GameObject healEffect;
    [SerializeField]
    private GameObject munitionEffect;

    private void Awake()
    {
        _vehicleStats = GetComponent<BaseVehicleManager>();

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("GameManager NOT Found");
        }

        GetUI();

        powerupParent = transform.GetChild(0);
    }


    private void Update()
    {
        PowerUpTimer();

        UpdateUI();

        UpdateInput();


    }

    private void GetUI()
    {
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager)
        {
            //UIManager Found
        }
        else
        {
            Debug.LogWarning("UIManager NOT Found");
        }
    }

    private void PowerUpTimer()
    {
        _vehicleStats.timer -= Time.deltaTime;

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
            ResetPowerUps();
        }

    }

    private void UpdateUI()
    {
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

    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && powerUp != null)
        {
            ActivatePowerUp(powerUp);
        }
    }


    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    private void ActivatePowerUp(PickUpScriptableObject powerUp)
    {
        powerUpList.Remove(powerUp);
        if (_vehicleStats.CompareTag("Player"))
        {
            uIManager.powerUpUI.color = new Color(0, 0, 0, 0);
        }
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
        if (_vehicleStats.CompareTag("Player"))
        {
            uIManager.powerUpUI.sprite = powerUp.icon;
            uIManager.powerUpUI.color = new Color(1, 1, 1, 1);
        }
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
            shieldEffect.SetActive(false);
        }
        if (_vehicleStats.hasNitro)
        {
            _vehicleStats.hasNitro = false;
            nitroEffect.SetActive(false);
            gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV;
            gameManager.overlayCam.fieldOfView = gameManager.vCamPOV;
            VehicleController vehicleContr = GetComponent<VehicleController>();
            vehicleContr.mMaxSpeed = GetComponent<SpaceshipLoad>().CurrentShip.maxSpeed;
            vehicleContr.mAccelerationConstant = GetComponent<SpaceshipLoad>().CurrentShip.accelerationSpeed;
        }
        if (_vehicleStats.ammo <= 0 && uIManager)
        {
            _vehicleStats.canShoot = false;
            _vehicleStats.ammo = 0;
            munitionEffect.SetActive(false);

            if (_vehicleStats.CompareTag("Player"))
            {
                uIManager.ammoAmountUI.gameObject.SetActive(false);
            }

        }
        if (_vehicleStats.hasBomb)
        {
            _vehicleStats.hasBomb = false;
        }
        if (_vehicleStats.hasHealed)
        {
            healEffect.SetActive(false);
            _vehicleStats.hasHealed = false;
        }
    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        shieldEffect.SetActive(true);
        _vehicleStats.isImmortal = true;
        _vehicleStats.hasShield = true;
    }

    public void NitroPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        nitroEffect.SetActive(true);
        if (_vehicleStats.CompareTag("Player"))
        {
            gameManager.vCam.m_Lens.FieldOfView = gameManager.vCamPOV + nitroFovModifier;
            gameManager.overlayCam.fieldOfView = gameManager.vCamPOV + nitroFovModifier;
        }
        //gameManager.vCam.m_Lens.FieldOfView = Mathf.Lerp(gameManager.vCamPOV, gameManager.vCamPOV + playerStats.mainCamPovBoost, Time.deltaTime);
        VehicleController vehicleContr = GetComponent<VehicleController>();
        vehicleContr.mMaxSpeed += nitroSpeedModifier;
        vehicleContr.mAccelerationConstant += nitroAccelerationModifier;

        _vehicleStats.hasNitro = true;
    }

    public void AmmoPowerUp()
    {
        if (_vehicleStats.CompareTag("Player"))
        {
            uIManager.ammoAmountUI.gameObject.SetActive(true);
        }
        //if (_vehicleStats.ammo < _vehicleStats.ammoLimit)
        //{
        //    _vehicleStats.ammo += _vehicleStats.ammoAdd;
        //    if (_vehicleStats.ammo > _vehicleStats.ammoLimit) _vehicleStats.ammo = _vehicleStats.ammoLimit;
        //}
        munitionEffect.SetActive(true);
        _vehicleStats.ammo += _vehicleStats.ammoAdd;
        if (_vehicleStats.CompareTag("Player"))
        {
            uIManager.ammoAmountUI.text = _vehicleStats.ammo.ToString();
        }
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
        healEffect.SetActive(true);
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        _vehicleStats.hasHealed = true;
        _vehicleStats.health += _vehicleStats.healValue;
        if (_vehicleStats.health >= _vehicleStats.healthLimit)
        {
            _vehicleStats.health = _vehicleStats.healthLimit;
        }
    }

    #endregion
}
