using Audio;
using UnityEngine;

namespace Weapons
{
    public class LaserWeaponInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileDefaultSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 1f;
        [SerializeField] private int ammoAdd = 20;
        private Camera _camera;

        private PlayerManager player;

        private void Awake()
        {
            _camera = Camera.main;
            //instantiationLocation = GetComponent<Transform>();
            player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();

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
            if (player.currentSpeed >= projectileDefaultSpeed)
            {
                projectileSpeed = player.currentSpeed + projectileDefaultSpeed;
            }
            else
            {
                projectileSpeed = projectileDefaultSpeed;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            GameObject projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(ray.direction * projectileSpeed, ForceMode.Impulse);

            PlaySound();
            Destroy(projectile, projectileLifeTime);
        }


        public void PlaySound()
        {
            AudioManager.PlaySound(AudioManager.Sound.LaserSound, instantiationLocation.position, .5f);
        }


    }
}