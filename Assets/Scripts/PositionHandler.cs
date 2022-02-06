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

    public Checkpoint nextcheckpoint;

    private Transform racersParent;
    public List<GameObject> racers;

    private void Start()
    {
        nextcheckpoint = checkpointManager.nextcheckpoint;

        racersParent = this.transform;

        for (int i = 0; i < racersParent.childCount; i++)
        {
            racers.Add(racersParent.GetChild(0).gameObject);
        }
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

        PositionCalc();

    }



    public void PositionCalc()
    {
        playerPosition = DistanceToNextCheckpoint(racersParent.GetChild(0).transform, nextcheckpoint.transform);

        //player
        gameManager.positions[0] = DistanceToNextCheckpoint(racersParent.GetChild(0).transform, nextcheckpoint.transform);
        //ai -s
        gameManager.positions[1] = DistanceToNextCheckpoint(racersParent.GetChild(1).transform, nextcheckpoint.transform);
        gameManager.positions[2] = DistanceToNextCheckpoint(racersParent.GetChild(2).transform, nextcheckpoint.transform);

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
            return racer.GetComponent<AIManager>().currentLap;
        }
        if (racer.CompareTag("Player"))
        {
            return gameManager.currentLap;
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
            return racer.GetComponent<AIManager>().currentCheckpoint;
        }
        if (racer.CompareTag("Player"))
        {
            return gameManager.currentCheckpoint;
        }
        else
        {
            return -1;
        }
    }

}

