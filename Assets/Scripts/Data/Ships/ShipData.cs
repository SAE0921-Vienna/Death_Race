using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Ship", fileName = "Ship")]
public class ShipData : ScriptableObject
{
    public new string name;
    public int priceInShop;

    [Header("Visual")]
    public Mesh vehicleMesh;
    public Mesh vehicleColliderMesh;
    public GameObject vfxPrefab;

    [Header("Stats")]
    [Range(75, 125)]
    public int health;

    public int nitroSpeed;
    [Range(950f, 1250f)]
    public float maxSpeed;
    [Range(0.15f,0.3f)]
    public float accelerationSpeed;
    [Range(5f, 12f)]
    public float turnSpeed;
    [Range(2f, 5f)]
    public float speedBasedAngularDrag;

    [Header("WeaponPosition")]
    public Vector3 WeaponPosition;


    /// <summary>
    /// Sets the text with the purchase value of the ship.
    /// </summary>
    /// <returns></returns>
    public string GetShipPrice()
    {
       return @$"Price: {priceInShop}";
    }

    /// <summary>
    /// Sets the text for the ship stats.
    /// </summary>
    /// <returns></returns>
    public string GetShipStats()
    {
        return @$"Health: {health}

Max Speed: {maxSpeed}

Acceleration: {accelerationSpeed}

Turn Speed: {turnSpeed}

Angular Drag: {speedBasedAngularDrag}

";

    }
}
