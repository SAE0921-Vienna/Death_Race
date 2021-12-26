using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        private PowerUps powerUps;
        private PlayerStats playerStats;
        private Transform weaponPosition;
        private IWeapon _weapon;


        private void Awake()
        {
            _weapon = GetComponentInChildren<IWeapon>();

            powerUps = GetComponent<PowerUps>();
            playerStats = GetComponent<PlayerStats>();

            weaponPosition = transform.GetChild(1);


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
                    _weapon.Shoot();

                }



            }
        }



    }
}