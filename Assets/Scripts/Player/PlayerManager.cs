using System.Globalization;
using PlayerController;
using UnityEngine;
using UserInterface;
using Weapons;

public class PlayerManager : MonoBehaviour, IDamageable
{

    public VehicleController vehicleController;
    private UIManager uiManager;
    private GameManager gameManager;
    private PlayerShipWeapon playerShipWeapon;
    [Header("Health")]
    public int health = 100;
    public int healthLimit = 100;
    [Header("Nitro")]
    public float nitroSpeed = 50f;
    public float nitroAccelerationBoost = 0.05f;
    public float normalMaxSpeed;
    public float currentSpeed;
    public float mainCamPovBoost = 20f;
    [Header("Ammo")]
    public int ammo;
    public int ammoAdd;
    public int ammoLimit;
    public float bombTimer = 10f;
    public Vector3 bombScale = new Vector3(5, 5, 5);
    [Header("Healing Value")]
    public int healValue = 45;
    [Header("PowerUp Activates")]
    public bool shield;
    public bool nitro;
    public bool canShoot;
    public bool bomb;
    public bool isImmortal;
    public bool isOnRoadtrack;
    public bool isFacingCorrectDirection;

    [Header("Position")]
    public int currentlap;
    public int currentCheckpointIndex;
    public int nextCheckpointIndex;
    public Checkpoint nextCheckpoint;


    [Header("Power Up Timer")]
    public float timer;
    public float timerCooldown = 5f;

    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
        playerShipWeapon = GetComponent<PlayerShipWeapon>();

        #region  gameManager FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("GameManager NOT Found");

        }
        #endregion

        #region uiManager FindObjectOfType
        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.LogWarning("UIManager NOT Found");

        }

        #endregion

    }
    private void Start()
    {
        if (!playerShipWeapon)
        {
            Debug.LogWarning("PlayerWeapon NOT Found");
        }
        else
        {
            Debug.Log("Munition wurde geadded" + playerShipWeapon.GetAmmo());
            ammo = playerShipWeapon.GetAmmo();
            ammoAdd = playerShipWeapon.GetAmmo();
            ammoLimit = playerShipWeapon.GetAmmo() * 3;

        }
    }

    private void Update()
    {
        currentSpeed = Mathf.RoundToInt(vehicleController.currentSpeed * vehicleController.mMaxSpeed);
        if (uiManager)
        {
            if (uiManager.speedUnit != null) uiManager.speedUnit.text = currentSpeed.ToString(CultureInfo.InvariantCulture);
        }
        if (health <= 0) gameObject.SetActive(false);
        isOnRoadtrack = vehicleController.isOnRoadtrack;
    }

    public void GetDamage(int _damage)
    {
        health -= _damage;
        Debug.Log(this.gameObject.name + "bekommt Schaden: " + _damage);
    }
}
