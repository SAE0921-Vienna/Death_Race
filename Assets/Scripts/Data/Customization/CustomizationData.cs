using UnityEngine;
using Weapons;


[CreateAssetMenu(menuName = "Customization Data/Customization", fileName = "Customization")]
public class CustomizationData : ScriptableObject
{

    public Material[] vehicleMaterials;
    public MaterialType materialType;

    public enum MaterialType
    {
        Base,
        Angel,
        Soldier,
        Lava,
        Water,
        Agent

    }






}
