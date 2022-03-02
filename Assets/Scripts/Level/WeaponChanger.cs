using AI;
using UserInterface;
using UnityEngine;
using System.Collections;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;
    public ShipWeapon shipWeapon;

    public GameObject[] changeEffectPrefab;
    [SerializeField] private Vector3 effectScale = new Vector3(4f, 4f, 4f);
    private WeaponData tempWeapon;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager == null)
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

            tempWeapon = spaceshipLoad.allWeapons[spaceshipLoad.currentWeapon];

            spaceshipLoad.currentWeapon = transform.GetSiblingIndex();

            spaceshipLoad.SetWeapon();

            shipWeapon.currentWeapon = spaceshipLoad.allWeapons[spaceshipLoad.currentWeapon];
            shipWeapon.SetEquippedWeapon();
            shipWeapon.shipWeaponTransform = spaceshipLoad.weaponClone.transform.GetChild(0).transform;


            if (tempWeapon != shipWeapon.currentWeapon)
            {
                StartCoroutine(SpawnEffect(other.transform.GetChild(1).transform));
            }


            if (!vehicleManager.unlimitedAmmo)
            {

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

    IEnumerator SpawnEffect(Transform spawnPosition)
    {
        GameObject effectClone = Instantiate(changeEffectPrefab[Random.Range(0, changeEffectPrefab.Length)], spawnPosition);
        effectClone.transform.position = new Vector3(shipWeapon.shipWeaponTransform.position.x, shipWeapon.shipWeaponTransform.position.y + .2f, shipWeapon.shipWeaponTransform.position.z);
        effectClone.transform.localScale = effectScale;

        yield return new WaitForSeconds(effectClone.GetComponent<ParticleSystem>().main.duration);

        Destroy(effectClone, 1.2f);
    }

}
