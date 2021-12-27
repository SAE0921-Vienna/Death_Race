using Audio;
using UnityEngine;

namespace Weapons
{
    public class LaserWeaponInputResponse : MonoBehaviour, IWeapon, ISoundPlayer
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileLifeTime = 5f;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            instantiationLocation = GetComponent<Transform>();
        }


        public void Shoot()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.DrawLine(transform.position, hit.point);
            //}

            GameObject projectile = Instantiate(laserPrefab, instantiationLocation.position, Quaternion.identity);
            //projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, projectileSpeed));
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