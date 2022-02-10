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
        ghost.loadFromFile();
        saveLoadScript.LoadHighScoreData();

        ghostVehicleMaterialIndex = saveLoadScript.lastGhostVehicleIndex;
        ghostVehicleMaterialIndex = saveLoadScript.lastGhostMaterialIndex;

        uimanager.highscoreUI.text = "BEST TIME: " + saveLoadScript.highScore.ToString();



    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && gameManager.currentLap == 0)
        {
            if (ghost.hasData)
            {
                ghost.playGhostRecording();
            }
            else
            {
                ghostVehicleMeshIndex = saveLoadScript.lastEquippedVehicleMesh;
                ghostVehicleMaterialIndex = saveLoadScript.lastEquippedMaterial;
            }
            ghost.StartRecording();
            ghost.StartRecordingGhost();

        }

    }

    public void StopRecording()
    {
        ghost.StopRecordingGhost();
        if (saveLoadScript.highScore > gameManager.roundTimer || !ghost.hasData)
        {
            ghost.SaveGhostToFile();
            saveLoadScript.highScore = gameManager.roundTimer;
            saveLoadScript.SaveHighScore(gameManager.roundTimer, ghostVehicleMeshIndex, ghostVehicleMaterialIndex);
            
        }

    }


}
