using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Material", fileName = "Material")]
public class MaterialData : ScriptableObject
{
    public new string name;
    public int priceInShop;
    public Material material;

}
