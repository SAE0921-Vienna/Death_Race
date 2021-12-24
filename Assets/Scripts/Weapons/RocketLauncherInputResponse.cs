using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class RocketLauncherInputResponse : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float rocketAccelaratonDelay;
        [SerializeField] private float rocketDespawnTimer;
        
        public void Shoot()
        {
            StartCoroutine(LaunchRocket());
        }

        private IEnumerator LaunchRocket()
        {
            GameObject projectile = Instantiate(rocketPrefab, instantiationLocation.position, Quaternion.identity);
            yield return new WaitForSeconds(rocketAccelaratonDelay);
            
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, projectileSpeed), ForceMode.Force);
            yield return new WaitForSeconds(rocketDespawnTimer);
            Destroy(projectile);
        }
    }
}