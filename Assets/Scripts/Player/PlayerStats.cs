using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public bool shield;
    public bool nitro;
    public float nitroSpeed = 50f;
    public float speed;



    public float timer;
    public float timerCooldown = 5f;

    private void Awake()
    {
        speed = GetComponent<VehicleController>().speed;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = -1;
            ResetPowerUps();
        }
    }

    public void ResetPowerUps()
    {
        if (shield)
        {
            shield = false;
            transform.GetChild(0).gameObject.SetActive(false);

        }
        if (nitro)
        {
            nitro = false;
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<VehicleController>().speed = speed;
        }
    }


    public void ShieldPowerUp()
    {
        timer = timerCooldown;
        transform.GetChild(0).gameObject.SetActive(true);
        shield = true;
    }

    public void NitroPowerUp()
    {
        timer = timerCooldown;
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<VehicleController>().speed += nitroSpeed;
        nitro = true;

    }

}
