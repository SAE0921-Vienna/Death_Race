using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Audio;

public abstract class ShipWeapon : MonoBehaviour, ISoundPlayer
{
    [SerializeField] 
    protected int ammoSize;
    [SerializeField] 
    protected float fireRate;
    [SerializeField]
    protected int projectileDamage;
    [SerializeField]
    protected GameObject projectilePrefab;
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
        currentWeapon = GetComponent<SpaceshipLoad>().CurrentWeapon;
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
        projectilePrefab = currentWeapon.laserPrefab;
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
            return hit.transform.root.gameObject;
        }
        else
        {
            var myGameObject = new GameObject();
            myGameObject.name = "TEST";
            return myGameObject;
        }
    }
    protected void InstantiateProjectile()
    {
        var tempobj = Instantiate(projectilePrefab, shipWeaponTransform.position, Quaternion.identity);
        var temprb = tempobj.GetComponent<Rigidbody>();
        temprb.AddForce(GetComponent<Rigidbody>().velocity * 20);
    }

    public void PlaySound()
    {
        AudioManager.PlaySound(currentWeapon.WeaponSound);
    }
}
