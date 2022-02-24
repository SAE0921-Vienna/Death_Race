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
                if (ghost.hasData && !ghost.isRecording)
                {
                    ghost.playGhostRecording();
                }
                else
                {
                    ghostVehicleMeshIndex = saveLoadScript.lastEquippedVehicleMesh;
                    ghostVehicleMaterialIndex = saveLoadScript.lastEquippedMaterial;
                }

                if (!ghost.isRecording)
                {
                    ghost.StartRecording();
                    ghost.StartRecordingGhost();
                }


            }
        }
    }

    public void StopRecording()
    {
        ghost.StopRecordingGhost();

        if (saveLoadScript.bestTime > gameManager.roundTimer || !ghost.hasData)
        {
            ghost.SaveGhostToFile();
            saveLoadScript.bestTime = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(saveLoadScript.bestTime, gameManager.currentMinAsString, gameManager.currentSecAsString, gameManager.currentMiliAsString, ghostVehicleMeshIndex, ghostVehicleMaterialIndex);
        }

        ghost.loadFromFile();
        saveLoadScript.LoadHighScoreData();
        ghost.playGhostRecording();


        uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.currentMinAsString + ":" + saveLoadScript.currentSecAsString + ":" + saveLoadScript.currentMiliAsString;

    }


}
