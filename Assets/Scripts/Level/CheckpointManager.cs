using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpointsInWorldList;

    public int checkpoints;

    private Transform checkpointParent;

    private void Awake()
    {
        checkpoints = checkpointsInWorldList.Count;
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

    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        //Debug.Log(checkpoint.transform.name);
        Debug.Log(checkpointsInWorldList.IndexOf(checkpoint));

    }

}
