using UnityEngine;
using Weapons;


[CreateAssetMenu(menuName = "Customization Data/Customization", fileName = "Customization")]
public class CustomizationData : ScriptableObject
{

    public MaterialName[]  vehicleMaterials;
    //public Material[] vehicleMaterials;

    [System.Serializable]
    public class MaterialName
    {
        public string name;
        public Material material;

    }

    //public MaterialType materialType;

    //public enum MaterialType
    //{
    //    Base,
    //    Angel,
    //    Soldier,
    //    Lava,
    //    Water,
    //    Agent,
    //    Unavailable
    //}



}
