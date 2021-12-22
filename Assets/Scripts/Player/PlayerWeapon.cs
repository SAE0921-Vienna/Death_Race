using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private PowerUps powerUps;
    private PlayerStats playerStats;
    private Transform weaponPosition;


    private void Awake()
    {
        powerUps = GetComponent<PowerUps>();
        playerStats = GetComponent<PlayerStats>();

        weaponPosition = transform.GetChild(1);

        weaponPosition.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();

    }

    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// Shoot one single bullet/laser
    /// </summary>
    public void Shoot()
    {

        if (playerStats.canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Shoot
                playerStats.ammo -= 1;
                weaponPosition.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            }



        }
    }



}
