using System.Collections;
using Audio;
using UnityEngine;

namespace Weapons
{
    public class RocketLauncherInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float rocketAccelaratonDelay;
        [SerializeField] private float rocketOffset = 10f;
        [SerializeField] private float rocketDespawnTimer;
        [SerializeField] private Ray projectileRayDirection;
        [SerializeField] private float projectileFireRate = 0.5f;
        [SerializeField] private int ammoAdd = 15;

        public int GetAmmo() => ammoAdd;
        public float GetFireRate() => projectileFireRate;
        
        public void Shoot()
        {
            StartCoroutine(LaunchRocket()); 
            instantiationLocation = GetComponent<Transform>();
            
            PlaySound();
        }

        private IEnumerator LaunchRocket()
        {
            if (Camera.main != null) projectileRayDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

            var projectile = Instantiate(rocketPrefab, instantiationLocation.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(projectileRayDirection.direction * projectileSpeed, ForceMode.Acceleration);
            yield return null;
        }
        
        public void PlaySound()
        {
            AudioManager.PlaySound(AudioManager.Sound.RocketLauncherLaunch, instantiationLocation.position);
        }
    }
}