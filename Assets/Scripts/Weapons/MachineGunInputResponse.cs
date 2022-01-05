using Audio;
using UnityEngine;

namespace Weapons
{
    public class MachineGunInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 100f;
        [SerializeField] private float barrelRotationSpeed;
        [SerializeField] private int ammoAdd = 200;

        [SerializeField] private GameObject barrel;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            instantiationLocation = GetComponent<Transform>();
        }

        public void Shoot()
        {
            var projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_camera.ScreenPointToRay(Input.mousePosition).direction * projectileSpeed, ForceMode.Impulse);
            barrel.transform.Rotate(0f, 0f, barrelRotationSpeed);
            
            
            PlaySound();
            Destroy(projectile, projectileLifeTime);
        }
        public int GetAmmo() => ammoAdd;
        public float GetFireRate() => projectileFireRate;
        public void PlaySound() => AudioManager.PlaySound(AudioManager.Sound.GunShotGeneric, instantiationLocation.position,.7f);
    }
}