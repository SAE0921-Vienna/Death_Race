using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameManager gameManager;

    public List<Checkpoint> checkpointsInWorldList;

    public int checkpoints;
    public int currentCheckpoint;
    public int nextCheckpointIndex;

    private Transform checkpointParent;

    public Material normalCheckpointMAT;

    public Checkpoint nextcheckpoint;

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
            checkpointsInWorldList.Add(checkpoint);

        }




        checkpoints = checkpointsInWorldList.Count;
        gameManager.checkpoints = checkpoints;

        nextcheckpoint = checkpointsInWorldList[0];
        nextCheckpointIndex = 0;

    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        //Debug.Log(checkpoint.transform.name);
        if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            if (checkpoint.GetComponentInChildren<SkinnedMeshRenderer>())
            {
                //checkpoint.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                checkpoint.transform.GetChild(0).gameObject.SetActive(false);
                if ((checkpointsInWorldList.IndexOf(checkpoint) + 1) < checkpointsInWorldList.Count)
                {
                    nextcheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                    nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

            currentCheckpoint = checkpointsInWorldList.IndexOf(checkpoint);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
            gameManager.CheckCurrentCheckpoint();
            gameManager.spawnPlayerPosition = checkpoint.transform.position;
            gameManager.spawnPlayerRotation = checkpoint.transform.rotation;

            //Debug.Log("correct direction");
        }
        else
        {
            //Debug.Log("wrong direction or missed checkpoint");

        }

    }

    public void SetFirstCheckpointMAT()
    {
        Checkpoint nextcheckpoint = checkpointsInWorldList[1];
        //nextcheckpoint.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
    }


}
