using UnityEngine;
using Weapons;


[CreateAssetMenu(menuName = "Scriptables/Material", fileName = "Material")]
public class MaterialData : ScriptableObject
{
    public new string name;
    public Material material;

}
