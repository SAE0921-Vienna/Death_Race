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
    public string maxCoinsUI = "9999999";

    public TextMeshProUGUI shipShopName;
    public TextMeshProUGUI shipStatsShop;
    public TextMeshProUGUI weaponStatsShop;
    public TextMeshProUGUI materialPrice;

    public TextMeshProUGUI shipStatsGarage;
    public TextMeshProUGUI weaponStatsGarage;

    public Transform notEnoughMoneyTransform;

    public GameObject audioManager;

    public bool _isFullscreen;
    public Transform fullScreenToggle;


    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    private void Awake()
    {
        Time.timeScale = 1;

        _isFullscreen = Screen.fullScreen;
        fullScreenToggle.GetComponent<Toggle>().isOn = _isFullscreen;
    }

    private void Start()
    {
        audioManager.transform.GetChild(0).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(1).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(2).GetComponent<VolumeSlider>().GetAudiosAtStart();
    }

    /// <summary>
    /// Close the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    /// <summary>
    /// Sets full screen size
    /// </summary>
    /// <param name="_isFullscreen"></param>
    public void SetFullScreen(bool _isFullscreen)
    {
        this._isFullscreen = _isFullscreen;
        Screen.fullScreen = _isFullscreen;
    }
}
