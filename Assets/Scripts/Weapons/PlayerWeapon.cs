using UnityEngine;


namespace Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        private PowerUps _powerUps;
        private PlayerStats _playerStats;
        private Transform _weaponPosition;
        private IWeapon _weapon;


        private void Awake()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            _powerUps = GetComponent<PowerUps>();
            _playerStats = GetComponent<PlayerStats>();
            _weaponPosition = transform.GetChild(1);
        }

        private void Update()
        {
            Shoot();
        }

        /// <summary>
        /// Shoot one single bullet/laser
        /// </summary>
        private void Shoot()
        {
            if (!_playerStats.canShoot) return;
            if (Input.GetMouseButtonDown(0))
            {
                _playerStats.ammo -= 1;
                _weapon.Shoot();
            }
        }
    }
}