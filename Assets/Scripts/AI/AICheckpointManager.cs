using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICheckpointManager : MonoBehaviour
    {
        [Header("AI's Checkpoints")]
        public int checkpoints;
        public int currentCheckpoint;
        public int nextCheckpoint;

        public Transform aisTarget;
        public Vector3 targetPosition;


        private CheckpointManager checkpointManager;
        private AIManager aIManager;

        public List<Checkpoint> checkpointsInWorldList;
        private int nextCheckpointIndex;


        private void Start()
        {

            checkpointManager = FindObjectOfType<CheckpointManager>().GetComponent<CheckpointManager>();
            aIManager = FindObjectOfType<AIManager>().GetComponent<AIManager>();

            checkpointsInWorldList = checkpointManager.checkpointsInWorldList;

            checkpoints = checkpointsInWorldList.Count;


            targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
            aisTarget.transform.position = targetPosition;

            nextCheckpointIndex = 0;

        }


        public void AIThroughCheckpoint(Checkpoint checkpoint)
        {
            //Debug.Log(checkpoint.transform.name);
            if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
            {

                currentCheckpoint = checkpointsInWorldList.IndexOf(checkpoint);
                nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
                nextCheckpoint = nextCheckpointIndex;
                aIManager.spawnAIPosition = checkpoint.transform.position;
                aIManager.spawnAIRotation = checkpoint.transform.rotation;
                targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
                aisTarget.transform.position = targetPosition;

                aIManager.checkpoints = checkpoints;
                aIManager.currentCheckpoint = currentCheckpoint;
                aIManager.nextCheckpoint = nextCheckpoint;

                //Debug.Log("correct direction");
            }
            else
            {
                //Debug.Log("wrong direction or missed checkpoint");

            }

        }



    }
}
