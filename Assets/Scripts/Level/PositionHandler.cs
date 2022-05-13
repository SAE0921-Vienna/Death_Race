using System.Collections.Generic;
using UnityEngine;
using AI;
using System;
using System.Collections;

public class PositionHandler : MonoBehaviour
{
    public PlayerManager playerManager;
    public CheckpointManager checkpointManager;
    public GameManager gameManager;

    public List<GameObject> racers;


    public int[] checkpointArray;
    public int[] lapArray;
    public int[] totalcpoint;

    public int playerPosition;
    public int ai1Position;
    public int ai2Position;
    public int ai3Position;

    private void Start()
    {
        if (gameManager.ghostMode)
        {
            StartCoroutine("GetGhostObject", 2f);
        }

        checkpointArray = new int[racers.Count];
        lapArray = new int[racers.Count];

        CheckPlace();
    }

    private void Update()
    {


        CheckPlace();
        playerManager.currentPositionIndex = playerPosition + 1;
        racers[1].GetComponent<BaseVehicleManager>().currentPositionIndex = ai1Position + 1;
        racers[2].GetComponent<BaseVehicleManager>().currentPositionIndex = ai2Position + 1;
        racers[3].GetComponent<BaseVehicleManager>().currentPositionIndex = ai3Position + 1;


    }
    private void CheckPlace()
    {
        for (int i = 0; i < racers.Count; i++)
        {
            checkpointArray[i] = racers[i].GetComponent<BaseVehicleManager>().currentCheckpointIndex;
            lapArray[i] = racers[i].GetComponent<BaseVehicleManager>().currentLapIndex;
            totalcpoint[i] = (lapArray[i] * checkpointManager.checkpointCount) + checkpointArray[i];
        }
        Array.Sort(totalcpoint);
        Array.Reverse(totalcpoint);

        playerPosition = Array.IndexOf(totalcpoint, (lapArray[0] * checkpointManager.checkpointCount) + checkpointArray[0]);
        ai1Position = Array.IndexOf(totalcpoint, (lapArray[1] * checkpointManager.checkpointCount) + checkpointArray[1]);
        if (!gameManager.ghostMode)
        {
            ai2Position = Array.IndexOf(totalcpoint, (lapArray[2] * checkpointManager.checkpointCount) + checkpointArray[2]);
            ai3Position = Array.IndexOf(totalcpoint, (lapArray[3] * checkpointManager.checkpointCount) + checkpointArray[3]);
        }


    }


    IEnumerator GetGhostObject()
    {
        racers.Add(FindObjectOfType<LoadCustomAI>().gameObject);
        yield return null;
    }


}






