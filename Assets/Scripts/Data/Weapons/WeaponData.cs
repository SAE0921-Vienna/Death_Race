using UnityEngine;
using static Audio.AudioManager;

[CreateAssetMenu(menuName = "Scriptables/Weapon", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public int priceInShop;


    [Header("Visual")]
    public GameObject vehicleWeaponPrefab;

    [Header("Projectile Settings")]
    public GameObject laserPrefab;
    public Sound WeaponSound;

    [Header("Stats")]
    [Range(3, 60)]
    public int ammoSize;
    [Range(1, 20)]
    public float fireRate;
    [Range(2, 75)]
    public int damage;

    /// <summary>
    /// Sets the text with the purchase value of the weapon.
    /// </summary>
    /// <returns></returns>
    public string GetWeaponPrice()
    {
        return @$"Price: {priceInShop}";
    }

    /// <summary>
    /// Sets the text for the weapon stats.
    /// </summary>
    /// <returns></returns>
    public string GetWeaponStats()
    {
        return @$"Damage: {damage}

Ammo: {ammoSize}

FireRate: {fireRate}

";

    }

}


