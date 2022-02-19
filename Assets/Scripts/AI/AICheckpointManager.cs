using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICheckpointManager : CheckpointManager
    {
        public Transform aisTarget;
        public Vector3 targetPosition;
        private AIManager aIManager;
        

        public override void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
        {
            base.VehicleThroughCheckpoint(checkpoint, vehicle);
            //Debug.Log(checkpoint.transform.name);
            //if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
            //{
            //    currentCheckpointIndex = checkpointsInWorldList.IndexOf(checkpoint);
            //    
            //    nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
            //    aIManager.spawnPosition = checkpoint.transform.position;
            //    aIManager.spawnRotation = checkpoint.transform.rotation;
            //    //targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
            //    
            //    aisTarget.transform.position = targetPosition;
//
            //    ////bug loop -- ai cant loop (it doesnt know how to go up and down) - so im improvising
            //    //if (nextCheckpointIndex == 12)
                //{
                //    aIManager.spawnPosition = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.position;
                //    aIManager.spawnRotation = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.rotation;
                //    //aIManager.RespawnAI();
                //}
                //if (nextCheckpointIndex == checkpointCount - 2)
                //{
                //    currentCheckpointIndex = 0;
                //    nextCheckpointIndex = 1;
                //    targetPosition = aiCheckpointsList[nextCheckpointIndex].transform.position;
                //    aisTarget.transform.position = targetPosition;
                //}
            //}
        }

        private void GetAllAICheckpoints()
        {
            //targetPosition = aiCheckpointsList[nextCheckpointIndex].transform.position;
            //aIManager.nextCheckpoint = nextCheckpointAI;
            //nextCheckpointAI = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
        }
    }
}
