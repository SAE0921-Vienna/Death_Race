using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PlayerShipWeapon : ShipWeapon
{
    [SerializeField] private Camera _camera;
    Ray ray;
    protected PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        _camera = Camera.main;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        RotateWeapon();
        Shoot();
    }

    /// <summary>
    /// Executes the Player Shoot method
    /// </summary>
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
                tempobj.GetComponent<IDamageable>().GetDamage(projectileDamage);
            }
        }
    }
    /// <summary>
    /// Rotates the player's weapon in the direction of the mouse pointer
    /// </summary>
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
}
