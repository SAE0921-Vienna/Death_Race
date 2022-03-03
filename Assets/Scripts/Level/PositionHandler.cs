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


    public int cpoint;
    public int lpoint;

    public int playerPosition;
    public int playerLaps;


    private void Start()
    {
        if (gameManager.ghostMode)
        {
            StartCoroutine("GetGhostObject", 2f);
        }

        checkpointArray = new int[racers.Count];
        lapArray = new int[racers.Count];

        PositionCalc();
        LapCalc();
    }

    private void Update()
    {
        PositionCalc();
        LapCalc();


        //müssen noch irgendwie überprüfen ob der spieler im selben checkpoint ist wie der/ein Gegner & die checkpoints

    }

    public void PositionCalc()
    {

        //Player
        checkpointArray[0] = playerManager.nextCheckpointIndex;

        //AI
        checkpointArray[1] = racers[1].GetComponent<BaseVehicleManager>().nextCheckpointIndex;
        //checkpointArray[2] = racers[2].GetComponent<BaseVehicleManager>().nextCheckpointIndex;
        //checkpointArray[3] = racers[3].GetComponent<BaseVehicleManager>().nextCheckpointIndex;
        //checkpointArray[4] = racers[4].GetComponent<BaseVehicleManager>().nextCheckpointIndex;

        Array.Sort(checkpointArray);
        Array.Reverse(checkpointArray);

        cpoint = Array.IndexOf(checkpointArray, playerManager.nextCheckpointIndex);


        switch (cpoint)
        {
            case 0:
                playerPosition = 1;
                break;
            case 1:
                playerPosition = 2;
                break;
            case 2:
                playerPosition = 3;
                break;
            case 3:
                playerPosition = 4;
                break;
            case 4:
                playerPosition = 5;
                break;
            default:
                break;
        }

    }

    public void LapCalc()
    {

        //Player
        lapArray[0] = playerManager.currentLapIndex;

        //AI
        lapArray[1] = racers[1].GetComponent<BaseVehicleManager>().currentLapIndex;
        //lapArray[2] = racers[2].GetComponent<BaseVehicleManager>().currentLapIndex;
        //lapArray[3] = racers[3].GetComponent<BaseVehicleManager>().currentLapIndex;
        //lapArray[4] = racers[4].GetComponent<BaseVehicleManager>().currentLapIndex;

        Array.Sort(lapArray);
        Array.Reverse(lapArray);

        lpoint = Array.IndexOf(lapArray, playerManager.currentLapIndex);


        switch (lpoint)
        {
            case 0:
                playerLaps = 1;
                break;
            case 1:
                playerLaps = 2;
                break;
            case 2:
                playerLaps = 3;
                break;
            case 3:
                playerLaps = 4;
                break;
            case 4:
                playerLaps = 5;
                break;
            default:
                break;
        }

    }


    IEnumerator GetGhostObject()
    {
        racers.Add(FindObjectOfType<LoadCustomAI>().gameObject);
        yield return null;
    }

    //public float DistanceToNextCheckpoint(Transform originPosition, Transform nextCheckpointPosition)
    //{
    //    Physics.Raycast(originPosition.position, nextCheckpointPosition.position, out var hit);

    //    return hit.distance;
    //}

    //public int GetLap(Transform racer)
    //{

    //    if (racer.GetComponent<BaseVehicleManager>())
    //    {
    //        return racer.GetComponent<BaseVehicleManager>().currentLapIndex;
    //    }
    //    else
    //    {
    //        return -1;
    //    }

    //}

    //public int GetCheckpoint(Transform racer)
    //{
    //    if (racer.GetComponent<BaseVehicleManager>())
    //    {
    //        return racer.GetComponent<BaseVehicleManager>().currentCheckpointIndex;
    //    }
    //    else
    //    {
    //        return -1;
    //    }
    //}

}






