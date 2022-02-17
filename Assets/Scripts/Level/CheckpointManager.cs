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
    [SerializeField]
    private GameObject checkpointEffectPrefab;

    public Material normalCheckpointMAT;

    public Checkpoint nextcheckpoint;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        checkpointParent = transform;
        checkpointsInWorldList = new List<Checkpoint>();

        //Order here is very important - in the hiearchy
        foreach (Transform checkpointsInWorld in checkpointParent)
        {
            //Debug.Log(checkpointsInWorld);
            Checkpoint checkpoint = checkpointsInWorld.GetComponent<Checkpoint>();
            checkpointsInWorldList.Add(checkpoint);

            if (gameManager.ghostmode) 
            {
                var checkpointEffect = Instantiate(checkpointEffectPrefab, checkpointsInWorld.position,
                    checkpointsInWorld.rotation);
                checkpointEffect.transform.parent = checkpoint.transform;
                checkpointEffect.transform.localPosition = Vector3.zero;
                checkpointEffect.SetActive(false);
            }
        }


        checkpoints = checkpointsInWorldList.Count;
        gameManager.checkpoints = checkpoints;

        nextcheckpoint = checkpointsInWorldList[0];
        nextCheckpointIndex = 0;
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
        {

            if (gameManager.ghostmode)
            {
                checkpoint.transform.GetChild(0).gameObject.SetActive(false);

            }
            if ((checkpointsInWorldList.IndexOf(checkpoint) + 1) < checkpointsInWorldList.Count)
            {
                nextcheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                if (gameManager.ghostmode)
                {
                    nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

            currentCheckpoint = checkpointsInWorldList.IndexOf(checkpoint);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
            gameManager.CheckCheckpoint();
            gameManager.spawnPlayerPosition = checkpoint.transform.position;
            gameManager.spawnPlayerRotation = checkpoint.transform.rotation;

            //Debug.Log("correct direction");
        }
    }

    public void SetFirstCheckpointMAT()
    {
        Checkpoint nextcheckpoint = checkpointsInWorldList[1];
        //nextcheckpoint.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
    }
}
