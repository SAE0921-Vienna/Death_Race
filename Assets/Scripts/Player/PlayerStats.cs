using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    private VehicleController vehicleController;
    public TextMeshProUGUI speedUnit;

    public int health = 100;
    public float nitroSpeed = 50f;
    public float normalMaxSpeed;
    public float currentSpeed;
    public int ammo;
    public int ammoAdd = 25;
    public int ammoLimit = 100;
    public bool shield;
    public bool nitro;
    public bool canShoot;

    public float timer;
    public float timerCooldown = 5f;


    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
    }

    private void Update()
    {
        currentSpeed = Mathf.RoundToInt(vehicleController.currentSpeed * vehicleController.mMaxSpeed);
        speedUnit.text = currentSpeed.ToString();
    }

}
