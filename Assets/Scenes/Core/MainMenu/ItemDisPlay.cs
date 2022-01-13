using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    #region Variables
    [Header("Item Display")]
    [SerializeField] private ShopItems Item;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI Wallet;
    [SerializeField] private GameObject BoughtText;
    [SerializeField] private Image Thumbnail;
    #endregion Variables

    /// <summary>
    /// Das Item (ScriptableObject) wird nun auf dem Display im Shop angezeigt
    /// </summary>
    void Awake()
    {
        PriceText.text = "" + Item.MilkyCoinPrice;
        NameText.text = Item.Name;
        DescriptionText.text = Item.Description;
        Thumbnail.sprite = Item.ItemThumbnail;
    }
    /// <summary>
    /// Hier wird überprüft, ob das Item bereits im Shop gekauft wurde
    /// </summary>
    private void Update()
    {
        Wallet.text = "Wallet: " + PlayerPrefs.GetInt("PlayerMoney");
        switch (Item.Type)
        {
            case ETypes.SHIP:
                if (PlayerPrefs.GetInt("JacketIsInStock") == 1)
                {
                    BoughtText.SetActive(true);
                }
                else
                {
                    BoughtText.SetActive(false);
                }
                break;
            case ETypes.WEAPON:
                if (PlayerPrefs.GetInt("ShoesAreInStock") == 1)
                {
                    BoughtText.SetActive(true);
                }
                else
                {
                    BoughtText.SetActive(false);
                }
                break;
            case ETypes.SHIPSKIN:
                if (PlayerPrefs.GetInt("BagIsInStock") == 1)
                {
                    BoughtText.SetActive(true);
                }
                else
                {
                    BoughtText.SetActive(false);
                }
                break;
        }
    }
}