using Audio;
using UnityEngine;

namespace Weapons
{
    public class LaserWeaponInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 1f;
        [SerializeField] private int ammoAdd = 20;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            instantiationLocation = GetComponent<Transform>();

        }


        public int GetAmmo()
        {
            return ammoAdd;
        }
        public float GetFireRate()
        {
            return projectileFireRate;
        }

        public void Shoot()
        {
         
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                GameObject projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(ray.direction * projectileSpeed, ForceMode.Impulse);

                Destroy(projectile, projectileLifeTime);

                AudioManager.PlaySound(AudioManager.Sound.LaserSound, 0.1f);
            
        }


        public void PlaySound()
        {
            AudioManager.PlaySound(AudioManager.Sound.LaserSound, 0.1f);
        }


    }
}