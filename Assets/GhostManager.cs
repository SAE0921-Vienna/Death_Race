using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public GameManager gameManager;
    public Ghost ghostManager;

    private void Awake()
    {
        ghostManager.StartRecordingGhost();

        ghostManager.loadFromFile();
        //ghostManager.playGhostRecording();

    }

    private void Update()
    {
        if(gameManager.currentLap >= gameManager.laps)
        {
            ghostManager.StopRecordingGhost();
            ghostManager.SaveGhostToFile();
        }
    }

}
