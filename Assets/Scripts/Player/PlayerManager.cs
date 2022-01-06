using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Weapons;

public class PlayerManager : MonoBehaviour
{

    private VehicleController vehicleController;
    private UIManager uiManager;
    private PlayerWeapon playerWeapon;
    [Header("Health")]
    public int health = 100;
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
    [Header("SlowTime")]
    public float slowTimeValue = 0.5f;
    [Header("PowerUp Activates")]
    public bool shield;
    public bool nitro;
    public bool canShoot;
    public bool bomb;
    public bool timeSlowed;
    public bool isImmortal;

    [Header("Power Up Timer")]
    public float timer;
    public float timerCooldown = 5f;


    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
        uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        playerWeapon = GetComponent<PlayerWeapon>();

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

    }

}
