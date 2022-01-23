using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{

    public TextMeshProUGUI shipName;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI materialName;


    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }



}
