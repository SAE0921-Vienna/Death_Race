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
    public RectTransform wrongDirectionUI;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        lapsAmountUI.text = gameManager.currentLap + "/" + gameManager.laps + " LAPS";
        //positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " POSITION";
        //switch, st, nd, rd, th,...
        positionsUI.text = gameManager.playerPosition +"st";
        roundTimerUI.text = gameManager.roundTimer.ToString();
    }

}
