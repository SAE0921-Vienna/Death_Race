using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UserInterface;
using TMPro;
using AI;

public class GameOver : MonoBehaviour
{
    GameManager gameManager;
    UIManager UIManager;
    [SerializeField] private SaveLoadScript saveLoadScript;
    public PositionHandler positionHandler;

    public TextMeshProUGUI playerPositionUI;
    public TextMeshProUGUI playerMoneyUI;
    public TextMeshProUGUI playerGameTime;

    public TextMeshProUGUI ai1position;
    public TextMeshProUGUI ai2position;
    public TextMeshProUGUI ai3position;

    public List<string> aiNames;

    private string earnedMoney;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        UIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        AddMoneyvsAI();

        UIManager.gameObject.SetActive(false);

        playerPositionUI.text = $"You Finished: {CheckPosition(positionHandler.racers[0].GetComponent<BaseVehicleManager>())} ";

        playerMoneyUI.text = $"You Earned: {earnedMoney}";

        ai1position.text = $"BOT {SetRandomName()}: {CheckPosition(positionHandler.racers[1].GetComponent<BaseVehicleManager>())} ";
        ai2position.text = $"BOT {SetRandomName()}: {CheckPosition(positionHandler.racers[2].GetComponent<BaseVehicleManager>())} ";
        ai3position.text = $"BOT {SetRandomName()}: {CheckPosition(positionHandler.racers[3].GetComponent<BaseVehicleManager>())} ";

        playerGameTime.text = $"Time: {gameManager.roundTimerAsString}";

    }

    public string CheckPosition(BaseVehicleManager vehicleManager)
    {
        switch (vehicleManager.currentPositionIndex)
        {
            case 1:
                return vehicleManager.currentPositionIndex + "st";
            case 2:
                return vehicleManager.currentPositionIndex + "nd";
            case 3:
                return vehicleManager.currentPositionIndex + "rd";
            case 4:
                return vehicleManager.currentPositionIndex + "th";
            default:
                return "Error";
        }
    }

    public string SetRandomName()
    {
        string tempString = aiNames[Random.Range(0, aiNames.Count)];
        aiNames.Remove(tempString);

        return tempString;
    }

    public void AddMoneyvsAI()
    {
        int tempAddedMoney = 0;
        int tempPlayerPosIndex = positionHandler.racers[0].GetComponent<BaseVehicleManager>().currentPositionIndex;

        switch (tempPlayerPosIndex)
        {
            case 1:
                tempAddedMoney += 1750;
                break;
            case 2:
                tempAddedMoney += 1250;
                break;
            case 3:
                tempAddedMoney += 750;
                break;
            case 4:
                tempAddedMoney += 450;
                break;
            default:
                break;
        }

        earnedMoney = tempAddedMoney.ToString();
        saveLoadScript.milkyCoins += tempAddedMoney;
        saveLoadScript.SaveMoneyData();
    }
}
