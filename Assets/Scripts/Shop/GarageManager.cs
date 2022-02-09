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

    public GameObject audioManager;
    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    private void Start()
    {
        audioManager.transform.GetChild(0).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(1).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(2).GetComponent<VolumeSlider>().GetAudiosAtStart();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }


}
