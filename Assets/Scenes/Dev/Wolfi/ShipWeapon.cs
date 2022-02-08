using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public abstract class ShipWeapon : MonoBehaviour
{
    [SerializeField] 
    protected int ammoSize;
    [SerializeField] 
    protected float fireRate;
    [SerializeField]
    protected int projectileDamage;
    [SerializeField]
    protected GameObject prefabWeapon;
    [SerializeField]
    protected WeaponData currentWeapon;
    [SerializeField]
    protected LayerMask targetLayer;

    protected float nextFire = 0f;

    protected Vector3 shipWeaponPosition;
    [SerializeField]protected Transform shipWeaponTransform;

    [Range(0, 100)]
    [SerializeField] protected float rotationSpeed = 100;
    [SerializeField] protected Quaternion currentRotation;

    protected Quaternion _targetRotation;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        shipWeaponPosition = GetComponent<SpaceshipLoad>().CurrentShip.WeaponPosition;
        //shipWeaponTransform = transform.GetChild(1).GetChild(1).transform;
        SetEquippedWeapon();
    }
    private void Start()
    {
        shipWeaponTransform = transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).transform;
    }
    public int GetAmmo()
    {
        return ammoSize;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public abstract void Shoot();

    protected void SetEquippedWeapon()
    {
        ammoSize = currentWeapon.ammoSize;
        fireRate = currentWeapon.fireRate;
        projectileDamage = currentWeapon.damage;
        prefabWeapon = currentWeapon.vehicleWeaponPrefab;
    }
    protected abstract void RotateWeapon();

    protected GameObject HitTarget()
    {
        Ray ray = new Ray(shipWeaponTransform.position, shipWeaponTransform.forward);
        float distance = 1000f;
        bool hitTarget = Physics.Raycast(ray, out RaycastHit hit, distance, targetLayer);
        if (hitTarget)
        {
            Debug.Log(hit.collider.gameObject);
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
