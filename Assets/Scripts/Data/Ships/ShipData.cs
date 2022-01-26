using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Ship", fileName = "Ship")]
public class ShipData : ScriptableObject
{
    public new string name;


    [Header("Visual")]
    public Mesh vehicleMesh;
    public Mesh vehicleColliderMesh;

    [Header("Stats")]
    public int health = 100;
    public int nitroSpeed = 300;
    public int maxSpeed = 2000;

    [Header("Stats")]
    public Vector3 WeaponPosition;


}
