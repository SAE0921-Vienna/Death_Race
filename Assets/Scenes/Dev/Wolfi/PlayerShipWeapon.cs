using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PlayerShipWeapon : ShipWeapon
{
    [SerializeField] private Camera _camera;
    Ray ray;
    protected PlayerManager playerManager;

    protected override void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        base.Awake();
    }

    void Update()
    {
        //Debug.DrawRay(shipWeaponTransform.position, shipWeaponTransform.forward*400, Color.green);
        RotateWeapon();
        Shoot();
    }
    public override void Shoot()
    {
        if (!playerManager.canShoot) return;

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            if (playerManager.ammo <= 0)
            {
                playerManager.canShoot = false;
                Debug.Log("No Ammo");
                return;
            }
            else
                playerManager.canShoot = true;

            nextFire = Time.time + 1 / fireRate;

            playerManager.ammo -= 1;
            InstantiateProjectile();
            var tempobj = HitTarget();

            if (tempobj != null && tempobj.GetComponent<IDamageable>() != null)
            {
                if(!tempobj.GetComponent<AIManager>().isImmortal) //Gilt nur für 1 Spieler gegen X AI's
                tempobj.GetComponent<IDamageable>().GetDamage(projectileDamage);
            }
            else
            {
                Debug.Log("MAMAMAAAAAA");
            }
            //Debug.Log(HitTarget().name);
        }
    }

    protected override void RotateWeapon()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            Debug.DrawLine(transform.position, hit.point);

        _targetRotation = Quaternion.LookRotation(ray.direction);
        currentRotation = shipWeaponTransform.rotation;

        float angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

        shipWeaponTransform.rotation = angularDifference > 0
            ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
            : _targetRotation;
    }
    protected override void InstantiateProjectile()
    {
        if (playerManager.currentSpeed > 0)
        {
            projectileSpeed = (playerManager.currentSpeed * 150) + projectileDefaultSpeed;
        }
        else
        {
            projectileSpeed = projectileDefaultSpeed;
        }
        //PlaySound();
        GameObject projectile = Instantiate(projectilePrefab, shipWeaponTransform.position, shipWeaponTransform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(shipWeaponTransform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);

        Destroy(projectile, projectileLifeTime);
    }
}
