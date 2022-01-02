using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI speedUnit;
    public TextMeshProUGUI ammoAmountUI;
    public Image powerUpUI;
    public TextMeshProUGUI lapsAmountUI;
    public TextMeshProUGUI positionsUI;
    public TextMeshProUGUI roundTimerUI;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        lapsAmountUI.text = gameManager.currentLap + "/" + gameManager.laps + " laps";
        positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " position";
        roundTimerUI.text = gameManager.roundTimer.ToString();
    }

}
