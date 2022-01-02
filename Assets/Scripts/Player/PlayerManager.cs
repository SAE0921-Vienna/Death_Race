using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private VehicleController vehicleController;
    private UIManager uiManager;
    [Header("Health")]
    public int health = 100;
    [Header("Nitro")]
    public float nitroSpeed = 50f;
    public float normalMaxSpeed;
    public float currentSpeed;
    [Header("Ammo")]
    public int ammo;
    public int ammoAdd = 25;
    public int ammoLimit = 100;
    [Header("Bomb")]
    public float bombTimer = 8f;
    public Vector3 bombScale = new Vector3(5, 5, 5);
    [Header("SlowTime")]
    public float slowTimeValue = 0.5f;
    [Header("Activates")]
    public bool isImmortal;
    public bool shield;
    public bool nitro;
    public bool canShoot;
    public bool bomb;
    public bool timeSlowed;

    [Header("Power Up Timer")]
    public float timer;
    public float timerCooldown = 5f;


    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
        uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    private void Update()
    {
        currentSpeed = Mathf.RoundToInt(vehicleController.currentSpeed * vehicleController.mMaxSpeed);
        if (uiManager.speedUnit != null) uiManager.speedUnit.text = currentSpeed.ToString();
      
    }

}
