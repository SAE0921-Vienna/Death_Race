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



    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    


}
