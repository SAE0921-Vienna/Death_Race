using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UserInterface;

public class GhostManager : MonoBehaviour
{
    #region References
    public GameManager gameManager;
    public SaveLoadScript saveLoadScript;
    public UIManager uimanager;

    public Ghost ghost;
    #endregion

    /// <summary>
    /// Checks if its in ghostmode
    /// </summary>
    private void Start()
    {
        if (gameManager.ghostMode)
        {

            ghost.loadFromFile();
            saveLoadScript.LoadHighScoreData();

            if (!ghost.hasData)
            {
                uimanager.highscoreUI.text = "BEST TIME: ??:??:??";

            }
            else
            {
                uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;
            }


            saveLoadScript.LoadMoneyData();
            uimanager.moneyUI.text = "Milky Coins: " + saveLoadScript.milkyCoins;
        }

 
    }

    /// <summary>
    /// Starts Recording Ghost
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.ghostMode)
        {
            if (other.tag == "Player")
            {
                StartRecording();

                if (ghost.hasData)
                {
                    ghost.playGhostRecording();

                    //ResetTimer();

                }
            }
        }
    }

    /// <summary>
    /// Stops the Ghost Recording and Saves the best time
    /// </summary>
    public void StopRecording()
    {
        ghost.StopRecordingGhost();

        if (saveLoadScript.bestTime > gameManager.roundTimer)
        {
            ghost.SaveGhostToFile();
            saveLoadScript.bestTime = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(gameManager.roundTimer, gameManager.currentMinAsString, gameManager.currentSecAsString, gameManager.currentMiliAsString, saveLoadScript.lastEquippedVehicleMesh, saveLoadScript.lastEquippedMaterial);
            //Debug.Log("Saved Best Time: " + gameManager.roundTimer);
        }

        ghost.loadFromFile();
        saveLoadScript.LoadHighScoreData();
        //Debug.Log("Best Time: " + gameManager.roundTimer);

        //ghost.playGhostRecording();
        ghost.ChangeGhostVehicle();

        ResetTimer();

        uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;

    }

    /// <summary>
    /// Saves the first Ghost Recording (if there is no data)
    /// </summary>
    public void SaveRecordingFirstTime()
    {
        if (!ghost.hasData)
        {
            ghost.StopRecordingGhost();

            ghost.SaveGhostToFile();
            saveLoadScript.bestTime = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(gameManager.roundTimer, gameManager.currentMinAsString, gameManager.currentSecAsString, gameManager.currentMiliAsString, saveLoadScript.lastEquippedVehicleMesh, saveLoadScript.lastEquippedMaterial);
            //Debug.Log("First Time: " + gameManager.roundTimer);

            ghost.loadFromFile();
            saveLoadScript.LoadHighScoreData();
            ghost.playGhostRecording();

            ResetTimer();

            uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;
        }
        else
        {
            StopRecording();
        }
    }

    /// <summary>
    /// Starts the Ghost Recording
    /// </summary>
    public void StartRecording()
    {
        if (!ghost.isRecording)
        {
            ghost.StartRecording();
            ghost.StartRecordingGhost();

        }
    }

    /// <summary>
    /// Resets the Timer for the next lap
    /// </summary>
    public void ResetTimer()
    {
        gameManager.roundTimer = 0;
        gameManager.currentMilliSec = 0;
        gameManager.currentMin = 0;
        gameManager.currentSec = 0;
    }

    /// <summary>
    /// The player gets an amount of money won by racing against their own ghost
    /// </summary>
    public void AddMoneyGhostMode()
    {
        int tempAddedMoney = 0;

        if (gameManager.roundTimer <= 80f)
        {
            //1000
            tempAddedMoney += 1000;
        }
        else if (gameManager.roundTimer > 80f && gameManager.roundTimer <= 85f)
        {
            //500
            tempAddedMoney += 500;
        }
        else if (gameManager.roundTimer > 85f && gameManager.roundTimer <= 90f)
        {
            //300
            tempAddedMoney += 300;
        }
        else if (gameManager.roundTimer > 90f && gameManager.roundTimer <= 95f)
        {
            //200
            tempAddedMoney += 200;
        }
        else if (gameManager.roundTimer > 95f && gameManager.roundTimer <= 100f)
        {
            //100
            tempAddedMoney += 100;
        }
        else
        {
            //50
            tempAddedMoney += 50;
        }
        if ((saveLoadScript.bestTime > gameManager.roundTimer  && gameManager.roundTimer <= 100f) || !saveLoadScript.hasBestTimeData)
        {
            tempAddedMoney += 500;
        }


        saveLoadScript.milkyCoins += tempAddedMoney;
        StartCoroutine(ShowAddedMoney(tempAddedMoney));
        saveLoadScript.SaveMoneyData();
        uimanager.moneyUI.text = "Milky Coins: " + saveLoadScript.milkyCoins;
    }

    /// <summary>
    /// Displays the added money in the UI
    /// </summary>
    /// <param name="tempAddedMoney"></param>
    /// <returns></returns>
    IEnumerator ShowAddedMoney(int tempAddedMoney)
    {
        uimanager.tempAddedMoneyUI.gameObject.SetActive(true);
        uimanager.tempAddedMoneyUI.text = "+" + tempAddedMoney;

        yield return new WaitForSeconds(2f);

        uimanager.tempAddedMoneyUI.gameObject.SetActive(false);

    }

}
