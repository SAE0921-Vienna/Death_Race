using System.Collections;
using System.Collections.Generic;
using AI;
using Audio;
using PlayerController;
using UnityEngine;
using UserInterface;

public class PowerUps : MonoBehaviour
{

    #region Nitro Power Up Values
    [SerializeField] protected float nitroSpeedModifier = 200f;
    [SerializeField] protected float normalSpeed;
    [SerializeField] private float nitroAccelerationModifier = 0.2f;
    [SerializeField] private float nitroFovModifier = 110f;
    [SerializeField] private float nitroFovNormal;
    [SerializeField] private float nitroDeltaSpeed;
    private float t;
    #endregion

    #region References
    public List<ScriptableObject> powerUpList;
    public PickUpScriptableObject powerUp;
    private UIManager uIManager;
    protected BaseVehicleManager _vehicleStats;
    private GameManager gameManager;
    protected Transform powerupParent;
    private int powerUpListLength = 1;

    [Header("Effects")]
    [SerializeField]
    protected GameObject shieldEffect;
    [SerializeField]
    protected GameObject nitroEffect;
    [SerializeField]
    protected GameObject bombEffect;
    [SerializeField]
    protected GameObject lowHealthEffect;
    [SerializeField]
    protected GameObject healEffect;
    [SerializeField]
    protected GameObject munitionEffect;
    #endregion

    /// <summary>
    /// VehicleManager and GameManager will be initialized
    /// </summary>
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

        nitroFovNormal = gameManager.vCamPOV;
    }

    /// <summary>
    /// UI, Input and the PowerUpTimer are being checked
    /// </summary>
    private void Update()
    {
        PowerUpTimer();

        UpdateUI();

        UpdateInput();

        SetFov();


        if (_vehicleStats.health < 40 && !_vehicleStats.isAlive)
        {
            lowHealthEffect.SetActive(true);
        }
        else
        {
            lowHealthEffect.SetActive(false);
        }

    }

    /// <summary>
    /// Gets the UI manager
    /// </summary>
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

    /// <summary>
    /// PowerUpTimer is responsible for the reseting of the PowerUp/s
    /// </summary>
    private void PowerUpTimer()
    {
        _vehicleStats.timer -= Time.deltaTime;

        if (_vehicleStats.timer < 0)
        {
            _vehicleStats.timer = -1;
            ResetPowerUps();
        }
    }

    /// <summary>
    /// Updates the UI 
    /// </summary>
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

    /// <summary>
    /// Updates Input and Checks if the player has activated a powerup
    /// </summary>
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

            if (_vehicleStats.unlimitedHealth)
            {
                _vehicleStats.isImmortal = true;
            }
        }
        if (_vehicleStats.hasNitro)
        {
            _vehicleStats.hasNitro = false;
            nitroEffect.SetActive(false);
            if (_vehicleStats.CompareTag("AI"))
            {
                Debug.Log(_vehicleStats.gameObject.name);
                AIFollowCurve aIFollowCurve = GetComponent<AIFollowCurve>();
                Debug.Log(aIFollowCurve.MaxSpeed);
                aIFollowCurve.MaxSpeed = normalSpeed;
                Debug.Log(aIFollowCurve.MaxSpeed);

            }
            else
            {
                VehicleController vehicleContr = GetComponent<VehicleController>();
                vehicleContr.mMaxSpeed = GetComponent<SpaceshipLoad>().CurrentShip.maxSpeed;
                vehicleContr.mAccelerationConstant = GetComponent<SpaceshipLoad>().CurrentShip.accelerationSpeed;
            }
        }

        if (_vehicleStats.ammo <= 0 && uIManager)
        {
            _vehicleStats.canShoot = false;
            _vehicleStats.ammo = 0;

            if (_vehicleStats.CompareTag("Player"))
            {
                uIManager.ammoAmountUI.gameObject.SetActive(false);
            }

        }
        if (_vehicleStats.hasAmmoPowerUp)
        {
            _vehicleStats.hasAmmoPowerUp = false;
            munitionEffect.SetActive(false);
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

    /// <summary>
    /// Gives the vehicle a shield and makes it invincible
    /// </summary>
    public void ShieldPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        shieldEffect.SetActive(true);
        _vehicleStats.isImmortal = true;
        _vehicleStats.hasShield = true;

        AudioManager.PlaySound(AudioManager.Sound.ShieldUp);
    }

    /// <summary>
    /// Gives the vehicle a nitro booster
    /// </summary>
    public void NitroPowerUp()
    {
        _vehicleStats.timer = _vehicleStats.timerCooldown;
        nitroEffect.SetActive(true);
        if (_vehicleStats.CompareTag("AI"))
        {
            AIFollowCurve aIFollowCurve = GetComponent<AIFollowCurve>();
            aIFollowCurve.MaxSpeed += nitroSpeedModifier;
        }
        else
        {
            VehicleController vehicleContr = GetComponent<VehicleController>();
            vehicleContr.mMaxSpeed += nitroSpeedModifier;
            vehicleContr.mAccelerationConstant += nitroAccelerationModifier;
        }

        _vehicleStats.hasNitro = true;

        AudioManager.PlaySound(AudioManager.Sound.NitroSound);
    }

    private void SetFov()
    {
        if (_vehicleStats.hasNitro)
        {
            gameManager.vCam.m_Lens.FieldOfView =
                    Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovModifier, nitroDeltaSpeed);
            gameManager.vCamBack.m_Lens.FieldOfView =
                Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovModifier, nitroDeltaSpeed);
            gameManager.overlayCam.fieldOfView = Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovModifier, nitroDeltaSpeed);
        }
        else
        {
            gameManager.vCam.m_Lens.FieldOfView = Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovNormal, nitroDeltaSpeed);
            gameManager.vCamBack.m_Lens.FieldOfView =
                Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovNormal, nitroDeltaSpeed);
            gameManager.overlayCam.fieldOfView = Mathf.MoveTowards(gameManager.vCam.m_Lens.FieldOfView, nitroFovNormal, nitroDeltaSpeed);
        }
    }

    /// <summary>
    /// Adds Ammo to the Vehicle
    /// </summary>
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
        _vehicleStats.hasAmmoPowerUp = true;
        _vehicleStats.timer = munitionEffect.GetComponent<ParticleSystem>().main.duration;
        _vehicleStats.ammo += _vehicleStats.ammoAdd;
        _vehicleStats.canShoot = true;

        AudioManager.PlaySound(AudioManager.Sound.ReloadWeapon);



        if (_vehicleStats.CompareTag("Player"))
        {
            uIManager.ammoAmountUI.text = _vehicleStats.ammo.ToString();
        }
    }

    /// <summary>
    /// Drops a Bomb behind you with kinematic forces
    /// </summary>
    public void BombPowerUp()
    {
        GameObject bombClone = Instantiate(powerUp.powerUpPrefab, powerupParent.GetChild(2).transform.position, Quaternion.identity);
        bombClone.transform.localScale = bombClone.GetComponent<BombTrigger>().BombScale;
        bombClone.GetComponent<SphereCollider>().enabled = true;
        bombClone.GetComponent<BombTrigger>().bombHasBeenActivated = true;
        _vehicleStats.hasBomb = true;

        AudioManager.PlaySound(AudioManager.Sound.PlaceBomb);
    }

    /// <summary>
    /// Heals you for a "health" amount
    /// </summary>
    public void HealPowerUp()
    {
        healEffect.SetActive(true);
        _vehicleStats.timer = healEffect.GetComponent<ParticleSystem>().main.duration;
        _vehicleStats.hasHealed = true;
        _vehicleStats.health += _vehicleStats.healValue;
        if (_vehicleStats.health >= _vehicleStats.healthLimit)
        {
            _vehicleStats.health = _vehicleStats.healthLimit;
        }
        AudioManager.PlaySound(AudioManager.Sound.HealShip);
    }

    #endregion
}
