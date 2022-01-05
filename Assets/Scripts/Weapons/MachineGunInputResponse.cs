using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons
{

    public class MachineGunInputResponse : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private Transform instantiationLocation;
        [SerializeField] private float projectileSpeed = 200f;
        [SerializeField] private float projectileLifeTime = 5f;
        [SerializeField] private float projectileFireRate = 100f;
        [SerializeField] private int ammoAdd = 200;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            instantiationLocation = GetComponent<Transform>();
        }

        public void Shoot()
        {
            
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                GameObject projectile = Instantiate(laserPrefab, instantiationLocation.position, instantiationLocation.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(ray.direction * projectileSpeed, ForceMode.Impulse);

                Destroy(projectile, projectileLifeTime);
            




        }

        public int GetAmmo()
        {
            return ammoAdd;
        }
        public float GetFireRate()
        {
            return projectileFireRate;
        }
    }
}