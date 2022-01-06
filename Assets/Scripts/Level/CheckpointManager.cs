using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckpointManager : MonoBehaviour
{
    private GameManager gameManager;

    public List<Checkpoint> checkpointsInWorldList;

    public int checkpoints;
    public int currentCheckpoint;
    public int nextCheckpointIndex;

    private Transform checkpointParent;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();


        checkpointParent = this.transform;

        checkpointsInWorldList = new List<Checkpoint>();

        //Order here is very important - in the hiearchy
        foreach (Transform checkpointsInWorld in checkpointParent)
        {
            //Debug.Log(checkpointsInWorld);
            Checkpoint checkpoint = checkpointsInWorld.GetComponent<Checkpoint>();
            //checkpoint.SetTrackCheckPoints(this);
            checkpointsInWorldList.Add(checkpoint);

        }

        checkpoints = checkpointsInWorldList.Count;

        nextCheckpointIndex = 0;

    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        //Debug.Log(checkpoint.transform.name);
        if(checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            currentCheckpoint = checkpointsInWorldList.IndexOf(checkpoint);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
            gameManager.CheckCurrentCheckpoint();
            gameManager.spawnPlayerPosition = checkpoint.transform.position;

            Debug.Log("correct");
        }
        else
        {

            Debug.Log("no");

        }

    }

}
