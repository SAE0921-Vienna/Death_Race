using AI;
using UnityEngine;

public class GhostVehicleManager : BaseVehicleManager
{

    protected override void Start()
    {
        nextCheckpoint = _checkpointManager.checkpointsInWorldList[0];
        nextCheckpointIndex = 0;
    }


    protected override void Update()
    {
        //nothing
    }
}
