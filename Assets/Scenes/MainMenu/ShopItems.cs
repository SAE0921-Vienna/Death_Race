using UnityEngine;

public enum ETypes 
{
    NONE,
    SHIP,
    SHIPSKIN,
    WEAPON
}

[CreateAssetMenu(fileName ="New ShopItem", menuName = "Scriptables/ShopItems")]
public class ShopItems : ScriptableObject
{
    #region Variables
    [Header("Item Settings")]
    public string Name;
    public ETypes Type;
    public int ID;
    public string Description;
    public GameObject ship;
    public Material shipColor;
    public GameObject shipweapon;
    
    public int MilkyCoinPrice;
    public int StarCoinPrice;

    public bool IsInStock;
    private bool IsBuyable;

    public Sprite ItemThumbnail;


    #endregion Variables

    public void BuyItem()
    {
        switch (Type)
        {
            case ETypes.SHIP:
                Buyable();
                if (IsBuyable == true)
                {
                    PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") - MilkyCoinPrice);
                }
                    break;
            case ETypes.SHIPSKIN:
                Buyable();
                
                break;
            case ETypes.WEAPON:
                Buyable();
                
                break;
        }
        PlayerPrefs.Save(); //Saving PlayerManager
    }
    
    private void Buyable()
    {
        if(IsInStock)
        {
            if(PlayerPrefs.GetInt("PlayerMoney") >= MilkyCoinPrice || PlayerPrefs.GetInt("PlayerMoney") >= StarCoinPrice)
            {
                IsBuyable = true;
            }
            else
            {
                IsBuyable = false;
            }
        }
    }

    public void AddItem()
    {
        switch (Type)
        {
            case ETypes.SHIP:

                break;
            case ETypes.SHIPSKIN:

                break;
            case ETypes.WEAPON:

                break;
        }
    }
}
