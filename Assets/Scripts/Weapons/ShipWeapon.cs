using UnityEngine;
using Audio;
using AI;

public abstract class ShipWeapon : MonoBehaviour, ISoundPlayer
{
    //Weapon
    [Header("Weapon")]
    public WeaponData currentWeapon;
    [SerializeField] protected LayerMask targetLayer;
    protected Vector3 shipWeaponPosition;
    public Transform shipWeaponTransform;
    [Range(0, 100)]
    [SerializeField] protected float rotationSpeed = 100;
    [SerializeField] protected Quaternion currentRotation;
    protected Quaternion _targetRotation;

    // Projectil
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

    // Start is called before the first frame update
    //protected virtual void Awake()
    //{

    //}
    protected virtual void Start()
    {

        currentWeapon = GetComponent<SpaceshipLoad>().CurrentWeapon;
        shipWeaponPosition = GetComponent<SpaceshipLoad>().CurrentShip.WeaponPosition;
        vehicleManager = GetComponent<BaseVehicleManager>();

        SetEquippedWeapon();

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

    public void SetEquippedWeapon()
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
        bool hitTarget = Physics.Raycast(ray, out hit, distance, targetLayer);

        if (hitTarget)
        {
            //Debug.Log(hit.transform.root.gameObject.name);
            return hit.transform.root.gameObject;
        }
        else
        {
            //var myGameObject = new GameObject();
            //myGameObject.name = "TEST";
            return null;
        }
    }

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

    public void PlaySound()
    {
        //AudioManager.PlaySound(currentWeapon.WeaponSound);
    }
}
