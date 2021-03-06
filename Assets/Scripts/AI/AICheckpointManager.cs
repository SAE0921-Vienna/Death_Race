using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICheckpointManager : CheckpointManager
    {
        public Transform aisTarget;
        public Vector3 targetPosition;
        private AIManager aIManager;
        public Transform aiCheckpointParent;
        public List<Transform> aiCheckpointsList;
        
        private void Awake()
        {
            //foreach (Transform checkpointsInWorld in aiCheckpointParent)
            //{
            //    Transform checkpoint = checkpointsInWorld.transform;
            //    aiCheckpointsList.Add(checkpoint);
            //}
            
            base.GetAllCheckpoints();
        }
        
        public override void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
        {
            //var _vehicleAgent = vehicle.GetComponentInParent<VehicleAgent>();
            //if (checkpointsInWorldList.IndexOf(checkpoint) == vehicle.GetComponentInParent<BaseVehicleManager>().nextCheckpointIndex)
            //{
            //    _vehicleAgent.AddAgentReward(1f);
            //}
            base.VehicleThroughCheckpoint(checkpoint, vehicle);
            //targetPosition = checkpointsInWorldList[aIManager.nextCheckpointIndex].transform.position;
            //aisTarget.transform.position = targetPosition;

            ////bug loop -- ai cant loop (it doesnt know how to go up and down) - so im improvising
            //if (aIManager.nextCheckpointIndex == 12)
            //{
            //    aIManager.spawnPosition = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.position;
            //    aIManager.spawnRotation = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.rotation;
            //    aIManager.RespawnVehicle();
            //}
            //if (aIManager.nextCheckpointIndex == checkpointCount - 2)
            //{
            //    aIManager.currentCheckpointIndex = 0;
            //    aIManager.nextCheckpointIndex = 1;
            //    targetPosition = aiCheckpointsList[aIManager.nextCheckpointIndex].transform.position;
            //    aisTarget.transform.position = targetPosition;
            //}
        }
    }
}
