using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{

    public TextMeshProUGUI shipName;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI materialName;

    public Button materialNext;
    public Button materialPrevious;
    public Button saveAndCloseGarage;

    public GameObject unavailablePanel;

    public Button buttonBuyShip;
    public Button buttonBuyWeapon;
    public Button buttonBuyMaterial;

    public TextMeshProUGUI milkyCoins;
    public TextMeshProUGUI starCoins;
    public string maxCoinsUI = "9999999";

    public TextMeshProUGUI shipStats;
    public TextMeshProUGUI weaponStats;
    public TextMeshProUGUI materialPrice;

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }


}
