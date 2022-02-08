using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipWeapon : ShipWeapon
{
    [SerializeField]private Camera _camera;
    protected PlayerManager playerManager;

    protected override void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        base.Awake();
    }

    void Update()
    {
        //Debug.DrawRay(shipWeaponTransform.position, shipWeaponTransform.forward*400, Color.green);
        Shoot();
    }
    public override void Shoot()
    {
        if (!playerManager.canShoot) return;

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            PlaySound();
            InstantiateProjectile();
            nextFire = Time.time + 1 / fireRate;

            ammoSize -= 1;
            //var tempobj = HitTarget();
            if (HitTarget() != null && HitTarget().GetComponent<IDamageable>() != null)
            {

                HitTarget().GetComponent<IDamageable>().GetDamage(projectileDamage);
                //tempobj.GetComponent<IDamageable>().GetDamage(projectileDamage);
            }
            else
            {
                Debug.Log("MAMAMAAAAAA");
            }
            Debug.Log(HitTarget().name);
        }
        //Projectile instanzieren?
    }

    protected override void RotateWeapon()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
            Debug.DrawLine(transform.position, hit.point);

        _targetRotation = Quaternion.LookRotation(ray.direction);
        currentRotation = shipWeaponTransform.rotation;

        var angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

        shipWeaponTransform.rotation = angularDifference > 0
            ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
            : _targetRotation;
    }
}
