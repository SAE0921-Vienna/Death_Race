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


    private void Start()
    {

        racersParent = this.transform;

        for (int i = 0; i < racersParent.childCount; i++)
        {
            racers.Add(racersParent.GetChild(0).gameObject);
        }

        //PositionCalc();
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


        if (checkpointArray[0] > checkpointArray[1])
        {
            playerManager.currentPositionIndex = 1;
        }
        if (checkpointArray[1] > checkpointArray[0])
        {
            playerManager.currentPositionIndex = 2;
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

        if (racer.CompareTag("AI"))
        {
            return racer.GetComponent<AIManager>().currentLapIndex;
        }
        if (racer.CompareTag("Player"))
        {
            return racer.GetComponent<PlayerManager>().currentLapIndex;
        }
        else
        {
            return -1;
        }

    }

    public int GetCheckpoint(Transform racer)
    {
        if (racer.CompareTag("AI"))
        {
            return racer.GetComponent<AIManager>().currentCheckpointIndex;
        }
        if (racer.CompareTag("Player"))
        {
            return racer.GetComponent<PlayerManager>().currentCheckpointIndex;
        }
        else
        {
            return -1;
        }
    }

}

