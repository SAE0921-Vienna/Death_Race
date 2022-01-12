using System.Collections;
using System.Collections.Generic;
using PlayerController;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Weapons;

public class PlayerManager : MonoBehaviour
{

    public VehicleController vehicleController;
    private UIManager uiManager;
    private GameManager gameManager;
    private PlayerWeapon playerWeapon;
    [Header("Health")]
    public int health = 100;
    public int healthLimit = 100;
    [Header("Nitro")]
    public float nitroSpeed = 50f;
    public float normalMaxSpeed;
    public float currentSpeed;
    [Header("Ammo")]
    public int ammo;
    //public int ammoAdd = 25;
    public int ammoAdd;
    //public int ammoLimit = 100;
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

    [Header("Power Up Timer")]
    public float timer;
    public float timerCooldown = 5f;


    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
        playerWeapon = GetComponent<PlayerWeapon>();

        #region  gameManager FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
        {
            //gameManager Found
        }
        else
        {
            Debug.LogWarning("GameManager NOT Found");
        }
        #endregion

        #region uiManager FindObjectOfType
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager)
        {
            //UIManager Found
        }
        else
        {
            Debug.LogWarning("UIManager NOT Found");
        }
        #endregion



    }
    private void Start()
    {
        ammoAdd = playerWeapon.ammoAdd;
        ammoLimit = playerWeapon.ammoAdd;
    }

    private void Update()
    {
        currentSpeed = Mathf.RoundToInt(vehicleController.currentSpeed * vehicleController.mMaxSpeed);
        if (uiManager.speedUnit != null) uiManager.speedUnit.text = currentSpeed.ToString();
        if (health <= 0) gameObject.SetActive(false);
        isOnRoadtrack = vehicleController.isOnRoadtrack;

        Debug.DrawLine(transform.position, FacingInfo().point);


    }

    public RaycastHit FacingInfo()
    {
        Physics.Raycast(transform.position, transform.forward, out var hit);

        return hit;
    }

}
