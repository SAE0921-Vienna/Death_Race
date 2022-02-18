using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    public class AICheckpointManagerMachine : MonoBehaviour
    {
        [Header("AI's Checkpoints")]
        public int checkpointCount;
        public int currentCheckpointIndex;

        //public Transform aisTarget;
        public Vector3 targetPosition;


        private CheckpointManager checkpointManager;
        private AIManager aIManager;
        private VehicleAgent _vehicleAgent;

        public List<Checkpoint> checkpointsInWorldList;
        //public List<GameObject> aiCheckpointsList;
        public GameObject aiCheckpointParent;
        public Checkpoint nextCheckpointAI;
        private int nextCheckpointIndex;



        private void Start()
        {

            checkpointManager = FindObjectOfType<CheckpointManager>().GetComponent<CheckpointManager>();
            aIManager = FindObjectOfType<AIManager>().GetComponent<AIManager>();
            _vehicleAgent = GetComponent<VehicleAgent>();

            checkpointsInWorldList = checkpointManager.checkpointsInWorldList;

            checkpointCount = checkpointsInWorldList.Count;


            //targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
            //aisTarget.transform.position = targetPosition;



            //for (int i = 0; i < aiCheckpointParent.transform.childCount; i++)
            //{
            //    aiCheckpointsList.Add(aiCheckpointParent.transform.GetChild(i).gameObject);
            //}

            aIManager.nextCheckpoint = checkpointsInWorldList[0];
            nextCheckpointIndex = 0;
            aIManager.nextCheckpointIndex = nextCheckpointIndex;
        }


        public void AIThroughCheckpoint(Checkpoint checkpoint)
        {
            //Debug.Log(checkpoint.transform.name);
            if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
            {
                currentCheckpointIndex = checkpointsInWorldList.IndexOf(checkpoint);
                nextCheckpointAI = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
                
                //targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
                //targetPosition = aiCheckpointsList[nextCheckpointIndex].transform.position;
                //aisTarget.transform.position = targetPosition;
                
                aIManager.checkpoints = checkpointCount;
                aIManager.currentCheckpointIndex = currentCheckpointIndex;
                aIManager.nextCheckpoint = nextCheckpointAI;
                aIManager.nextCheckpointIndex = nextCheckpointIndex;
                
                aIManager.spawnAIPosition = checkpoint.transform.position;
                aIManager.spawnAIRotation = checkpoint.transform.rotation;
                
                _vehicleAgent.AddAgentReward(1f); //This adds a reward to the agent.
            }
        }

        public void ResetCheckpoints()
        {
            Start();
        }
    }
}