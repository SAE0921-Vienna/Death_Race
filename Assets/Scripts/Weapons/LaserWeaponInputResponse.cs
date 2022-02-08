using Audio;
using UnityEngine;

namespace Weapons
{
    public class LaserWeaponInputResponse : MonoBehaviour, ISoundPlayer //IWeapon
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileDefaultSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 1f;
        [SerializeField] private int ammoAdd;
        
        private Camera _camera;
        private PlayerManager _player;

        private void Awake()
        {

            _camera = Camera.main;
            _player = GetComponent<PlayerManager>(); //HEIßt eigentlich ShipManager
            if (!_player)
            {
                Debug.LogWarning("PlayerManager has NOT been found");
            }
        }
        
        public int GetAmmo() => ammoAdd;
        public float GetFireRate() => projectileFireRate;
        public void PlaySound() => AudioManager.PlaySound(AudioManager.Sound.LaserSound, instantiationLocation.position, .5f);

        //public void Shoot()
        //{
        //    if (_player.currentSpeed >= projectileDefaultSpeed)
        //    {
        //        projectileSpeed = _player.currentSpeed + projectileDefaultSpeed;
        //    }
        //    else
        //    {
        //        projectileSpeed = projectileDefaultSpeed;
        //    }

        //    var ray = _camera.ScreenPointToRay(Input.mousePosition);
            
        //    var projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
        //    projectile.GetComponent<Rigidbody>().AddForce(ray.direction * projectileSpeed, ForceMode.Impulse);

        //    PlaySound();
        //    Destroy(projectile, projectileLifeTime);
        //}
    }
}