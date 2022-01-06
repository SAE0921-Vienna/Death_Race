using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishLine;

    private CheckpointManager checkpointManager;

    public int laps = 3;
    public int currentLap;
    public int positions = 5;
    public int playerPosition;
    public int checkpoints;
    public int currentCheckpoint;

    public float roundTimer;

    public bool isFacingCorrectWay;

    private void Awake()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>().GetComponent<CheckpointManager>();
        checkpoints = checkpointManager.checkpoints;
    }

    private void Update()
    {
        roundTimer = Time.time;
        roundTimer = (float)System.Math.Round(roundTimer, 0);

        CheckLaps();
        CheckCurrentCheckpoint();
    }


    public void CheckLaps()
    {
        if (currentLap >= laps)
        {
            currentLap = laps;
        }
        if (currentLap < 0)
        {
            currentLap = 0;
        }

    }

    public void CheckCurrentCheckpoint()
    {


    }

    public void CheckPosition()
    {
        //Check Position
    }
}
