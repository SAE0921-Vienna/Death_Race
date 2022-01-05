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
            projectileRayDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

            var projectile = Instantiate(rocketPrefab, instantiationLocation.position, Quaternion.identity);
            Vector3 offset = new Vector3(projectile.transform.position.x, projectile.transform.position.y + rocketOffset, projectile.transform.position.z);
            projectile.transform.position = offset;
            yield return new WaitForSeconds(rocketAccelaratonDelay);

            projectile.GetComponent<Rigidbody>().AddForce(projectileRayDirection.direction * projectileSpeed, ForceMode.Impulse);
            
            //projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, projectileSpeed), ForceMode.Force);
            yield return new WaitForSeconds(rocketDespawnTimer);
            
            Destroy(projectile);
        }


        public void PlaySound()
        {
            AudioManager.PlaySound(AudioManager.Sound.RocketLauncherLaunch, instantiationLocation.position);
        }
    }
}