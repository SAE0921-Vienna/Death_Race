using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class AIShipWeapon : ShipWeapon
{
    private FieldOfView fieldOfView;
    [SerializeField]
    private Vector3 weaponToTarget;
    [SerializeField]
    private float changePositionTimer;
    [SerializeField]
    private bool checkTimer;

    private AIManager aIManager;

    protected override void Awake()
    {
        aIManager = GetComponent<AIManager>();
        fieldOfView = GetComponentInParent<FieldOfView>();
        weaponToTarget = new Vector3(0f, 0f, 0f);
        checkTimer = false;
        changePositionTimer = 5f;
        base.Awake();
    }
    private void Update()
    {
        RotateWeapon();

        if (checkTimer)
            Timer();
    }

    public override void Shoot()
    {
        if (!aIManager.canShoot) return;
        
        if (Time.time > nextFire)
        {
            PlaySound();
            Debug.Log("NEXTFIRE" + nextFire);
            nextFire = Time.time + 1 / fireRate;

            ammoSize -= 1;
            InstantiateProjectile();

            if (HitTarget() != null && HitTarget().GetComponent<IDamageable>() != null)
            {
                HitTarget().GetComponent<IDamageable>().GetDamage(projectileDamage);
                Debug.Log("Ich Gengner SCHADEN");
            }
            else
            {
                Debug.Log("MAMAMAAAAAA");
            }
        }
    }

    protected override void RotateWeapon()
    {
        if (fieldOfView.nearestObject != null)
        {

            weaponToTarget.x = fieldOfView.nearestObject.transform.position.x - shipWeaponTransform.position.x;
            weaponToTarget.y = fieldOfView.nearestObject.transform.position.y - shipWeaponTransform.position.y;
            weaponToTarget.z = fieldOfView.nearestObject.transform.position.z - shipWeaponTransform.position.z;

            _targetRotation = Quaternion.LookRotation(weaponToTarget);
            currentRotation = shipWeaponTransform.rotation;

            float angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

            shipWeaponTransform.rotation = angularDifference > 0
                ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
                : _targetRotation;

            changePositionTimer = 5f;
        }
        else
        {
            if (!checkTimer)
                checkTimer = true;

            if (changePositionTimer <= 0 && checkTimer)
            {
                shipWeaponTransform.localRotation = Quaternion.identity;

                checkTimer = false;
                changePositionTimer = 5f;
            }
        }
    }
    private void Timer()
    {
        changePositionTimer -= Time.deltaTime;
    }
    protected override void InstantiateProjectile()
    {
        Debug.Log(shipWeaponTransform.localEulerAngles);
        GameObject projectile = Instantiate(projectilePrefab, shipWeaponTransform.position, shipWeaponTransform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(shipWeaponTransform.localEulerAngles * projectileSpeed, ForceMode.Impulse);
    }
}
