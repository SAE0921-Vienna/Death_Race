using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UserInterface;

public class GhostManager : MonoBehaviour
{

    public GameManager gameManager;
    public UIManager uimanager;
    public bool hasData;


    public Ghost ghost;

    private void Start()
    {

        //uimanager.highscoreUI.text = highScoreTime.ToString();


        ghost.loadFromFile();
      

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && gameManager.currentLap == 0)
        {
            ghost.StartRecording();
            ghost.StartRecordingGhost();
            if (ghost.hasData)
            {
                ghost.playGhostRecording();
            }

        }
        if (other.tag == "Player" && gameManager.currentLap >= gameManager.laps)
        {
            ghost.StopRecordingGhost();
            //highScoreTime = (int)gameManager.roundTimer;
            //if (highScoreTime < savedHighScoreTime)
            //{
            //    ghost.SaveGhostToFile();
            //}

        }
    }



}
