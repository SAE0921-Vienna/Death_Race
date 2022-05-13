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
    public float[] distanceArray;
    public float[] totalcpoint;
    

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
            //distanceArray[i] = 

            float distance = Mathf.Pow(1.02f, -racers[i].GetComponent<BaseVehicleManager>().distance);

            totalcpoint[i] = (lapArray[i] * checkpointManager.checkpointCount) + checkpointArray[i] + distance;
        }
        Array.Sort(totalcpoint);
        Array.Reverse(totalcpoint);

        playerPosition = Array.IndexOf(totalcpoint, (lapArray[0] * checkpointManager.checkpointCount) + checkpointArray[0] + Mathf.Pow(1.02f, -racers[0].GetComponent<BaseVehicleManager>().distance)) ;
        ai1Position = Array.IndexOf(totalcpoint, (lapArray[1] * checkpointManager.checkpointCount) + checkpointArray[1] + Mathf.Pow(1.02f, -racers[1].GetComponent<BaseVehicleManager>().distance));
        if (!gameManager.ghostMode)
        {
            ai2Position = Array.IndexOf(totalcpoint, (lapArray[2] * checkpointManager.checkpointCount) + checkpointArray[2] + Mathf.Pow(1.02f, -racers[2].GetComponent<BaseVehicleManager>().distance));
            ai3Position = Array.IndexOf(totalcpoint, (lapArray[3] * checkpointManager.checkpointCount) + checkpointArray[3] + Mathf.Pow(1.02f, -racers[3].GetComponent<BaseVehicleManager>().distance));
        }


    }


    IEnumerator GetGhostObject()
    {
        racers.Add(FindObjectOfType<LoadCustomAI>().gameObject);
        yield return null;
    }


}






