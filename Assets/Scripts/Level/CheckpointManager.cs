using System.Collections.Generic;
using AI;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpointsInWorldList;
    public int checkpointCount;
    private Transform checkpointParent;

    public GameManager _gameManager;
    public GameObject checkpointEffectPrefab;

    private void Awake()
    {
        GetAllCheckpoints();

        if (_gameManager == null)
        {
            Debug.Log("GameManager has NOT been found");
        }
    }

    protected void GetAllCheckpoints()
    {
        checkpointParent = transform;
        checkpointsInWorldList = new List<Checkpoint>();

        foreach (Transform checkpointsInWorld in checkpointParent)
        {
            Checkpoint checkpoint = checkpointsInWorld.GetComponent<Checkpoint>();
            checkpointsInWorldList.Add(checkpoint);

            if (_gameManager.withEffects)
            {
                var checkpointEffect = Instantiate(checkpointEffectPrefab, checkpointsInWorld.position, checkpointsInWorld.rotation);
                checkpointEffect.transform.parent = checkpoint.transform;
                checkpointEffect.transform.localPosition = checkpointEffectPrefab.transform.localPosition;
                checkpointEffect.transform.localScale = checkpointEffectPrefab.transform.localScale;
                checkpointEffect.SetActive(false);
            }
        }
        
        checkpointsInWorldList.Reverse();

        if (_gameManager.withEffects)
        {
            checkpointsInWorldList[0].transform.GetChild(0).gameObject.SetActive(false);
        }

        checkpointCount = checkpointsInWorldList.Count;
    }

    public virtual void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
    {
        var vehicleManager = vehicle.GetComponentInParent<BaseVehicleManager>();

        if (checkpointsInWorldList.IndexOf(checkpoint) == vehicleManager.nextCheckpointIndex)
        {
            if ((checkpointsInWorldList.IndexOf(checkpoint) + 1) < checkpointsInWorldList.Count)
            {
                vehicleManager.previousCheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint)];
                vehicleManager.nextCheckpoint = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];

                if (_gameManager.withEffects)
                {
                    DeactivateNextCheckpointEffect(vehicleManager);

                    ActivateNextCheckpointEffect(vehicleManager);

                    if (vehicleManager.nextCheckpointIndex == 0)
                    {
                        checkpointsInWorldList[checkpointsInWorldList.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }


            }

            vehicleManager.currentCheckpointIndex = checkpointsInWorldList.IndexOf(checkpoint);
            vehicleManager.nextCheckpointIndex = (vehicleManager.nextCheckpointIndex + 1) % checkpointsInWorldList.Count;

            vehicleManager.spawnPosition = checkpoint.transform.position;
            vehicleManager.spawnRotation = vehicleManager.transform.rotation;

            Debug.LogFormat("Gone through checkpoint {0}.", vehicleManager.currentCheckpointIndex);
            Debug.LogFormat("Next Checkpoint is: {0}.", vehicleManager.nextCheckpointIndex);
        }
    }

    public void ActivateNextCheckpointEffect(BaseVehicleManager _vehicleManager)
    {
        _vehicleManager.nextCheckpoint.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeactivateNextCheckpointEffect(BaseVehicleManager _vehicleManager)
    {
        _vehicleManager.previousCheckpoint.transform.GetChild(0).gameObject.SetActive(false);
    }

}
