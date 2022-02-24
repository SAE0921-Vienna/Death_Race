using System.Collections.Generic;
using AI;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameManager _gameManager;
    public List<Checkpoint> checkpointsInWorldList;

    public int checkpointCount;
    //public int currentCheckpointIndex;
    //public int nextCheckpointIndex;
    //public Checkpoint nextCheckpoint;

    private Transform checkpointParent;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        GetAllCheckpoints();
    }

    protected void GetAllCheckpoints()
    {
        checkpointParent = transform;
        checkpointsInWorldList = new List<Checkpoint>();

        //Order here is very important - in the hierarchy
        foreach (Transform checkpointsInWorld in checkpointParent)
        {
            Checkpoint checkpoint = checkpointsInWorld.GetComponent<Checkpoint>();
            checkpointsInWorldList.Add(checkpoint);

            //if (_gameManager.ghostMode)
            //{
            //    //var checkpointEffect = Instantiate(checkpointEffectPrefab, checkpointsInWorld.position,
            //    //    checkpointsInWorld.rotation);
            //    //checkpointEffect.transform.parent = checkpoint.transform;
            //    //checkpointEffect.transform.localPosition = Vector3.zero;
            //    //checkpointEffect.SetActive(false);
            //}
        }
        checkpointCount = checkpointsInWorldList.Count;
    }

    public virtual void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
    {
        var vehicleManager = vehicle.GetComponentInParent<BaseVehicleManager>();

        if (checkpointsInWorldList.IndexOf(checkpoint) == vehicleManager.nextCheckpointIndex)
        {
            //if (_gameManager.ghostMode)
            //{
            //    //checkpoint.transform.GetChild(0).gameObject.SetActive(false);
            //}
            if ((checkpointsInWorldList.IndexOf(checkpoint) + 1) < checkpointsInWorldList.Count)
            {
                vehicleManager.nextCheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                //if (_gameManager.ghostMode)
                //{
                //    //nextCheckpoint.transform.GetChild(0).gameObject.SetActive(true);
                //}
            }
            
            vehicleManager.currentCheckpointIndex = checkpointsInWorldList.IndexOf(checkpoint);
            vehicleManager.nextCheckpointIndex = (vehicleManager.nextCheckpointIndex + 1) % checkpointsInWorldList.Count;

            vehicleManager.spawnPosition = checkpoint.transform.position;
            //vehicleManager.spawnRotation = checkpointCollider;
            
            Debug.LogFormat("Gone through checkpoint {0}.", vehicleManager.currentCheckpointIndex);
            Debug.LogFormat("Next Checkpoint is: {0}.", vehicleManager.nextCheckpointIndex);
        }
    }

    public void SetFirstCheckpointMAT()
    {
        //var nextcheckpoint = checkpointsInWorldList[1];
        ////nextcheckpoint.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        //nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
    }
}
