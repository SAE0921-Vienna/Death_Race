using System.Collections.Generic;
using AI;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameManager _gameManager;
    public List<Checkpoint> checkpointsInWorldList;

    public int checkpointCount;
    public int currentCheckpointIndex;
    public int nextCheckpointIndex;
    public Checkpoint nextCheckpoint;

    private Transform checkpointParent;
    [SerializeField]
    private GameObject checkpointEffectPrefab;
    

    protected void Awake()
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
            //Debug.Log(checkpointsInWorld);
            Checkpoint checkpoint = checkpointsInWorld.GetComponent<Checkpoint>();
            checkpointsInWorldList.Add(checkpoint);

            if (_gameManager.ghostMode)
            {
                var checkpointEffect = Instantiate(checkpointEffectPrefab, checkpointsInWorld.position,
                    checkpointsInWorld.rotation);
                checkpointEffect.transform.parent = checkpoint.transform;
                checkpointEffect.transform.localPosition = Vector3.zero;
                checkpointEffect.SetActive(false);
            }
        }
        checkpointCount = checkpointsInWorldList.Count;

        nextCheckpoint = checkpointsInWorldList[0];
        nextCheckpointIndex = 0;
    }

    public virtual void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
    {
        if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            var vehicleManager = vehicle.GetComponent<BaseVehicleManager>();
            
            if (_gameManager.ghostMode)
            {
                checkpoint.transform.GetChild(0).gameObject.SetActive(false);
            }
            if ((checkpointsInWorldList.IndexOf(checkpoint) + 1) < checkpointsInWorldList.Count)
            {
                nextCheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                if (_gameManager.ghostMode)
                {
                    nextCheckpoint.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            
            currentCheckpointIndex = checkpointsInWorldList.IndexOf(checkpoint);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
            
            var vehicleTransform = checkpoint.transform;
            
            vehicleManager.CheckCheckpoint(this);
            vehicleManager.spawnPosition = vehicleTransform.position;
            vehicleManager.spawnRotation = vehicleTransform.rotation;
        }
    }

    public void SetFirstCheckpointMAT()
    {
        //var nextcheckpoint = checkpointsInWorldList[1];
        ////nextcheckpoint.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        //nextcheckpoint.transform.GetChild(0).gameObject.SetActive(true);
    }
}
