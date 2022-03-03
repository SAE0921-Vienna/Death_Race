using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;

public class PositionHandler : MonoBehaviour
{
    public PlayerManager playerManager;
    public CheckpointManager checkpointManager;
    public GameManager gameManager;

    public float playerPosition;


    private Transform racersParent;
    public List<GameObject> racers;


    public int[] checkpointArray;
    public int[] lapArray;


    private void Start()
    {

        racersParent = this.transform;

        for (int i = 0; i < racersParent.childCount; i++)
        {
            racers.Add(racersParent.GetChild(0).gameObject);
        }

        PositionTest();

    }

    private void Update()
    {
        ////player
        //DistanceToNextCheckpoint(racersParent.GetChild(0).transform, nextcheckpoint.transform);
        ////Debug.Log(DistanceToNextCheckpoint(racersParent.GetChild(0).transform, nextcheckpoint.transform));
        //GetLap(racersParent.GetChild(0));
        //GetCheckpoint(racersParent.GetChild(0));

        ////ai
        //DistanceToNextCheckpoint(racersParent.GetChild(1).transform, nextcheckpoint.transform);
        ////Debug.Log(DistanceToNextCheckpoint(racersParent.GetChild(1).transform, nextcheckpoint.transform));
        //GetLap(racersParent.GetChild(1));
        //GetCheckpoint(racersParent.GetChild(1));


        ////-------------------------------

        //PositionCalc();

        //only against one ai
        PositionTest();



    }

    public void PositionTest()
    {

        //Player
        checkpointArray[0] = racers[0].GetComponent<PlayerManager>().nextCheckpointIndex;
        //AI
        checkpointArray[1] = racers[1].GetComponent<AIManager>().nextCheckpointIndex;

        Array.Sort(checkpointArray);

        int x = Array.IndexOf(checkpointArray, playerPosition);

        switch (x)
        {
            case 0:
                racers[0].GetComponent<PlayerManager>().currentPositionIndex = 1;
                break;
            case 1:
                racers[0].GetComponent<PlayerManager>().currentPositionIndex = 2;
                break;
            case 2:
                racers[0].GetComponent<PlayerManager>().currentPositionIndex = 3;
                break;
            case 3:
                racers[0].GetComponent<PlayerManager>().currentPositionIndex = 4;
                break;
            case 4:
                racers[0].GetComponent<PlayerManager>().currentPositionIndex = 5;
                break;
            default:
                break;
        }
    }

    public void PositionCalc()
    {
        //playerPosition = DistanceToNextCheckpoint(racersParent.GetChild(0).transform, racersParent.GetChild(0).GetComponent<PlayerManager>().nextCheckpoint.transform);

        ////player
        //gameManager.positions[0] = DistanceToNextCheckpoint(racersParent.GetChild(0).transform, racersParent.GetChild(0).GetComponent<PlayerManager>().nextCheckpoint.transform);
        ////ai -s
        //gameManager.positions[1] = DistanceToNextCheckpoint(racersParent.GetChild(1).transform, racersParent.GetChild(1).GetComponent<AIManager>().nextCheckpoint.transform);

        //Array.Sort(gameManager.positions);

        //int x = Array.IndexOf(gameManager.positions, playerPosition);

        //switch (x)
        //{
        //    case 0:
        //        gameManager.playerPosition = 1;
        //        break;
        //    case 1:
        //        gameManager.playerPosition = 2;
        //        break;
        //    case 2:
        //        gameManager.playerPosition = 3;
        //        break;
        //    default:
        //        break;
        //}
    }

    public float DistanceToNextCheckpoint(Transform originPosition, Transform nextCheckpointPosition)
    {
        Physics.Raycast(originPosition.position, nextCheckpointPosition.position, out var hit);

        return hit.distance;
    }

    public int GetLap(Transform racer)
    {

        if (racer.GetComponent<BaseVehicleManager>())
        {
            return racer.GetComponent<BaseVehicleManager>().currentLapIndex;
        }
        else
        {
            return -1;
        }

    }

    public int GetCheckpoint(Transform racer)
    {
        if (racer.GetComponent<BaseVehicleManager>())
        {
            return racer.GetComponent<BaseVehicleManager>().currentCheckpointIndex;
        }
        else
        {
            return -1;
        }
    }

}

