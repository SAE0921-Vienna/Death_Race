using UnityEngine;
using Audio;
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
    public int ammoSize;
    public float fireRate;
    public int damage;

    public string GetStats()
    {
        return @$"Price: {priceInShop}
Ammo: {ammoSize}
FireRate: {fireRate}
Damage: {damage}
";

    }

}


