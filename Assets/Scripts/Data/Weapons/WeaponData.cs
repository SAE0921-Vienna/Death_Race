using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Weapon", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public int priceInShop;


    [Header("Visual")]
    public GameObject vehicleWeaponPrefab;

}


