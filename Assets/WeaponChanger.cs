using AI;
using UserInterface;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;
    public ShipWeapon shipWeapon;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        if(uIManager == null)
        {
            Debug.Log("UIManager NOT found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(transform.GetSiblingIndex());
        if (other.CompareTag("Player"))
        {
            SpaceshipLoad spaceshipLoad = other.GetComponentInParent<SpaceshipLoad>();
            shipWeapon = other.GetComponentInParent<ShipWeapon>();
            BaseVehicleManager vehicleManager = other.GetComponentInParent<BaseVehicleManager>();

            spaceshipLoad.currentWeapon = transform.GetSiblingIndex();

            spaceshipLoad.SetWeapon();

            shipWeapon.currentWeapon = spaceshipLoad.allWeapons[spaceshipLoad.currentWeapon];
            shipWeapon.SetEquippedWeapon();
            shipWeapon.shipWeaponTransform = spaceshipLoad.weaponClone.transform.GetChild(0).transform;

            vehicleManager.ammo = shipWeapon.GetAmmo();
            vehicleManager.ammoAdd = shipWeapon.GetAmmo();

            if (vehicleManager.CompareTag("Player"))
            {
                uIManager.ammoAmountUI.gameObject.SetActive(true);
            }
            vehicleManager.canShoot = true;

        }


    }
}
