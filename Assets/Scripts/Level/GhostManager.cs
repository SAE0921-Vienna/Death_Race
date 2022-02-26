using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UserInterface;

public class GhostManager : MonoBehaviour
{
    public GameManager gameManager;
    public SaveLoadScript saveLoadScript;
    public UIManager uimanager;

    public int ghostVehicleMeshIndex;
    public int ghostVehicleMaterialIndex;

    public Ghost ghost;

    private void Start()
    {


        if (gameManager.ghostMode)
        {

            ghost.loadFromFile();
            saveLoadScript.LoadHighScoreData();

            ghostVehicleMaterialIndex = saveLoadScript.lastGhostVehicleIndex;
            ghostVehicleMaterialIndex = saveLoadScript.lastGhostMaterialIndex;

            if (!ghost.hasData)
            {
                uimanager.highscoreUI.text = "BEST TIME: ??:??:??";

            }
            else
            {
                uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;
            }
        }
    }

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

                //ghostVehicleMeshIndex = saveLoadScript.lastEquippedVehicleMesh;
                //ghostVehicleMaterialIndex = saveLoadScript.lastEquippedMaterial;

            }
        }
    }

    public void StopRecording()
    {
        ghost.StopRecordingGhost();

        if (saveLoadScript.bestTime > gameManager.roundTimer)
        {
            ghost.SaveGhostToFile();
            saveLoadScript.bestTime = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(gameManager.roundTimer, gameManager.currentMinAsString, gameManager.currentSecAsString, gameManager.currentMiliAsString, ghostVehicleMeshIndex, ghostVehicleMaterialIndex);
            //Debug.Log("Saved Best Time: " + gameManager.roundTimer);
        }

        ghost.loadFromFile();
        saveLoadScript.LoadHighScoreData();
        //Debug.Log("Best Time: " + gameManager.roundTimer);

        //ghost.playGhostRecording();

        ResetTimer();

        uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;

    }

    public void SaveRecordingFirstTime()
    {
        if (!ghost.hasData)
        {
            ghost.StopRecordingGhost();

            ghost.SaveGhostToFile();
            saveLoadScript.bestTime = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(gameManager.roundTimer, gameManager.currentMinAsString, gameManager.currentSecAsString, gameManager.currentMiliAsString, ghostVehicleMeshIndex, ghostVehicleMaterialIndex);
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


    public void StartRecording()
    {
        if (!ghost.isRecording)
        {
            ghost.StartRecording();
            ghost.StartRecordingGhost();

        }
    }

    public void ResetTimer()
    {
        gameManager.roundTimer = 0;
        gameManager.currentMilliSec = 0;
        gameManager.currentMin = 0;
        gameManager.currentSec = 0;
    }

    public void AddMoney()
    {
        int bonusMoney = 500;

        if (gameManager.roundTimer <= 80f)
        {
            //1000
            saveLoadScript.milkyCoins += 1000;
        }
        else if(gameManager.roundTimer > 80f && gameManager.roundTimer <= 85f) 
        {
            //500
            saveLoadScript.milkyCoins += 500;
        }
        else if(gameManager.roundTimer > 85f && gameManager.roundTimer <= 90f) 
        {
            //300
            saveLoadScript.milkyCoins += 500;
        }
        else if(gameManager.roundTimer > 90f && gameManager.roundTimer <= 95f)
        {
            //200
        }
        else if(gameManager.roundTimer > 95f && gameManager.roundTimer <= 100f) 
        {
            //100
        }
        else 
        {
            //50
        }
        if (saveLoadScript.bestTime > gameManager.roundTimer && gameManager.roundTimer <= 100f)
        {
            //bonusMoney
        }
        saveLoadScript.SaveMoneyData();
    }

}
