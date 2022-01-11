using Audio;
using UnityEngine;

namespace Weapons
{
    public class MachineGunInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileDefaultSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 100f;
        [SerializeField] private float barrelRotationSpeed;
        [SerializeField] private int ammoAdd = 200;

        [SerializeField] private GameObject barrel;
        private Camera _camera;

        private PlayerManager player;

        private void Awake()
        {
            _camera = Camera.main;
            //instantiationLocation = GetComponent<Transform>();
            #region playerManager FindObjectOfType
            player = FindObjectOfType<PlayerManager>();
            if (player)
            {
                //Player Found
            }
            else
            {
                Debug.LogWarning("PlayerManager NOT found");
            }
            #endregion
        }

        public void Shoot()
        {
            if( player.currentSpeed >= projectileDefaultSpeed)
            {
                projectileSpeed = player.currentSpeed + projectileDefaultSpeed;
            }
            else
            {
                projectileSpeed = projectileDefaultSpeed;
            }

            var projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_camera.ScreenPointToRay(Input.mousePosition).direction * projectileSpeed, ForceMode.Impulse);
            barrel.transform.Rotate(0f, 0f, barrelRotationSpeed);


            PlaySound();
            Destroy(projectile, projectileLifeTime);
        }
        public int GetAmmo() => ammoAdd;
        public float GetFireRate() => projectileFireRate;
        public void PlaySound() => AudioManager.PlaySound(AudioManager.Sound.GunShotGeneric, instantiationLocation.position, .7f);
    }
}