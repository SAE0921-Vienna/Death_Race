using Audio;
using UnityEngine;

namespace Weapons
{
    public class LaserWeaponInputResponse : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        
        public void Shoot()
        {
            GameObject projectile = Instantiate(laserPrefab, instantiationLocation.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, projectileSpeed));
            
            AudioManager.PlaySound(AudioManager.Sound.LaserSound);
        }
    }
}