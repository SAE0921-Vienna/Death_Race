using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Material", fileName = "Material")]
public class MaterialData : ScriptableObject
{
    public new string name;
    public int priceInShop;
    public Material material;

    /// <summary>
    /// Sets the text with the purchase value of the ship material.
    /// </summary>
    /// <returns></returns>
    public string GetPrice()
    {
        return @$"Price: {priceInShop}";

    }
}
