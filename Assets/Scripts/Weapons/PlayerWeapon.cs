using UnityEngine;


namespace Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        private PowerUps _powerUps;
        private PlayerManager _playerStats;
        private Transform _weaponPosition;
        private IWeapon _weapon;
        public float nextFire = 0f;
        public float fireRate;
        public int ammoAdd;

        private void Start()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            _powerUps = GetComponent<PowerUps>();
            _playerStats = GetComponent<PlayerManager>();
            _weaponPosition = transform.GetChild(1);

            if (_weapon != null)
            {
                fireRate = _weapon.GetFireRate();
                ammoAdd = _weapon.GetAmmo();
            }
            else
            {
                Debug.LogWarning("PlayerWeapon has NOT been found");
            }


        }

        private void Update()
        {
            Shoot();
        }

        /// <summary>
        /// Shoot one single bullet/laser
        /// </summary>
        protected void Shoot()
        {
            if (!_playerStats.canShoot) return;

            if (Input.GetMouseButton(0) && Time.time > nextFire)
            {
                nextFire = Time.time + 1 / fireRate;

                _playerStats.ammo -= 1;
                if (_weapon == null) return;
                _weapon.Shoot();
            }
        }
    }
}