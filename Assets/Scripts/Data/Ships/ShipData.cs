using UnityEngine;


[CreateAssetMenu(menuName = "Ship Data/Ship", fileName = "Ship")]
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




}
