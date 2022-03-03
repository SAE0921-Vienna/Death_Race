using UnityEngine;
using Audio;
using AI;

public abstract class ShipWeapon : MonoBehaviour, ISoundPlayer
{
    [Header("Weapon")]
    public WeaponData currentWeapon;
    [SerializeField] protected LayerMask targetLayer;
    protected Vector3 shipWeaponPosition;
    public Transform shipWeaponTransform;
    [Range(0, 100)]
    [SerializeField] protected float rotationSpeed = 100;
    [SerializeField] protected Quaternion currentRotation;
    protected Quaternion _targetRotation;

    [Header("Projectil")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int ammoSize;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int projectileDamage;
    [SerializeField] protected float projectileSpeed = 10000f;
    [SerializeField] protected float projectileDefaultSpeed = 200f;
    [SerializeField] protected float projectileLifeTime = 6f;
    protected float nextFire = 0f;

    protected BaseVehicleManager vehicleManager;
    protected RaycastHit hit;

    protected virtual void Start()
    {
        currentWeapon = GetComponent<SpaceshipLoad>().CurrentWeapon;
        shipWeaponPosition = GetComponent<SpaceshipLoad>().CurrentShip.WeaponPosition;
        vehicleManager = GetComponent<BaseVehicleManager>();

        SetEquippedWeapon();

        shipWeaponTransform = transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).transform;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetAmmo()
    {
        return ammoSize;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetFireRate()
    {
        return fireRate;
    }

    /// <summary>
    /// Calls the Shoot method
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Set the selected weapon stats
    /// </summary>
    public void SetEquippedWeapon()
    {
        ammoSize = currentWeapon.ammoSize;
        fireRate = currentWeapon.fireRate;
        projectileDamage = currentWeapon.damage;
        projectilePrefab = currentWeapon.laserPrefab;
    }

    /// <summary>
    /// Rotates the weapon in the aiming direction
    /// </summary>
    protected abstract void RotateWeapon();

    /// <summary>
    /// Gets the GameObject parent
    /// </summary>
    /// <returns></returns>
    protected GameObject HitTarget()
    {
        Ray ray = new Ray(shipWeaponTransform.position, shipWeaponTransform.forward);
        float distance = 1000f;
        bool hitTarget = Physics.Raycast(ray, out hit, distance, targetLayer);

        if (hitTarget)
        {
            return hit.transform.root.gameObject;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Instantiates the weapon projectile with a force
    /// </summary>
    protected virtual void InstantiateProjectile()
    {
        float tempProjectileLifeTime = projectileLifeTime;
        if (vehicleManager.currentSpeed > 0)
        {
            projectileSpeed = (vehicleManager.currentSpeed * 30) + projectileDefaultSpeed;
        }
        else
        {
            projectileSpeed = projectileDefaultSpeed;
        }
        //PlaySound();
        GameObject projectile = Instantiate(projectilePrefab, shipWeaponTransform.position, shipWeaponTransform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(shipWeaponTransform.forward * projectileSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

        if (currentWeapon.name.Equals("Raketa Avtamata"))
        {
            projectile.GetComponent<SphereCollider>().enabled = true;
            projectileLifeTime *= 1.5f;
        }
        else
            projectileLifeTime = tempProjectileLifeTime; //For Sandbox

        if (projectile != null)
        Destroy(projectile, projectileLifeTime);
    }

    /// <summary>
    /// Plays the gunshot sound
    /// </summary>
    public void PlaySound()
    {
        //AudioManager.PlaySound(currentWeapon.WeaponSound);
    }
}
