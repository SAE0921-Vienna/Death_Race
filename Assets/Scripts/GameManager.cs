using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishLine;

    public int laps = 3;
    public int currentLap;
    public int positions = 5;
    public int playerPosition;
    public int checkpoints;
    public int currentCheckpoint;


    public float roundTimer;

    public bool isFacingCorrectWay;

    private void Update()
    {
        roundTimer += Time.deltaTime;

        CheckLaps();
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

    public void CheckPosition()
    {
        //Check Position
    }
}
