using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyRecorder;

public class GhostManager : MonoBehaviour
{

    public GameManager gameManager;


    #region Recorder

    public Recorder recorder;
    //void Start()
    //{

    //    recorder.autoLoad();
    //    recorder.Load();
    //    if (recorder.hasData)
    //    {
    //        recorder.Play();
    //    }

    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.tag == "Player" && gameManager.currentLap == 0)
    //    {
    //        recorder.recorderState = RecorderState.Recording;

    //    }
    //    if (other.tag == "Player" && gameManager.currentLap >= gameManager.laps)
    //    {
    //        recorder.recorderState = RecorderState.Idle;

    //        recorder.autoSave();
    //        recorder.Save();

    //    }
    //}

    #endregion

    public Ghost ghost;

    private void Start()
    {
        ghost.loadFromFile();
        if (ghost.hasData)
        {
            ghost.playGhostRecording();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && gameManager.currentLap == 0)
        {
            ghost.StartRecording();
            ghost.StartRecordingGhost();

        }
        if (other.tag == "Player" && gameManager.currentLap >= gameManager.laps)
        {
            ghost.StopRecordingGhost();
            ghost.SaveGhostToFile();

        }
    }

 
}
