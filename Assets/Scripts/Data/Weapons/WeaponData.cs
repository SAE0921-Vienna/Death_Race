using UnityEngine;
using Weapons;


[CreateAssetMenu(menuName = "Weapon Data/Weapon", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;

    [Header("Visual")]
    public GameObject vehicleWeaponPrefab;

}


