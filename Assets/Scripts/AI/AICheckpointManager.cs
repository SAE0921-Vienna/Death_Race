using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICheckpointManager : MonoBehaviour
    {
        [Header("AI's Checkpoints")]
        public int checkpoints;
        public int currentCheckpoint;

        public Transform aisTarget;
        public Vector3 targetPosition;


        private CheckpointManager checkpointManager;
        private AIManager aIManager;

        public List<Checkpoint> checkpointsInWorldList;
        public List<GameObject> aiCheckpointsList;
        public GameObject aiCheckpointParent;
        public Checkpoint nextCheckpointAI;
        private int nextCheckpointIndex;



        private void Start()
        {

            checkpointManager = FindObjectOfType<CheckpointManager>().GetComponent<CheckpointManager>();
            aIManager = FindObjectOfType<AIManager>().GetComponent<AIManager>();

            checkpointsInWorldList = checkpointManager.checkpointsInWorldList;

            checkpoints = checkpointsInWorldList.Count;


            targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
            aisTarget.transform.position = targetPosition;



            for (int i = 0; i < aiCheckpointParent.transform.childCount; i++)
            {
                aiCheckpointsList.Add(aiCheckpointParent.transform.GetChild(i).gameObject);
            }

            aIManager.nextCheckpoint = checkpointsInWorldList[0];
            nextCheckpointIndex = 0;
            aIManager.nextCheckpointIndex = nextCheckpointIndex;
        }


        public void AIThroughCheckpoint(Checkpoint checkpoint)
        {
            //Debug.Log(checkpoint.transform.name);
            if (checkpointsInWorldList.IndexOf(checkpoint) == nextCheckpointIndex)
            {

                currentCheckpoint = checkpointsInWorldList.IndexOf(checkpoint);
                nextCheckpointAI = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1];
                nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointsInWorldList.Count;
                aIManager.spawnAIPosition = checkpoint.transform.position;
                aIManager.spawnAIRotation = checkpoint.transform.rotation;
                //targetPosition = checkpointsInWorldList[nextCheckpointIndex].transform.position;
                targetPosition = aiCheckpointsList[nextCheckpointIndex].transform.position;
                aisTarget.transform.position = targetPosition;

                //bug loop -- ai cant loop (it doesnt know how to go up and down) - so im improvising
                if (nextCheckpointIndex == 12)
                {
                    aIManager.spawnAIPosition = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.position;
                    aIManager.spawnAIRotation = checkpointsInWorldList[checkpointsInWorldList.IndexOf(checkpoint) + 1].transform.rotation;
                    aIManager.RespawnAI();
                }
                if (nextCheckpointIndex == checkpoints - 2)
                {
                    currentCheckpoint = 0;
                    nextCheckpointIndex = 1;
                    targetPosition = aiCheckpointsList[nextCheckpointIndex].transform.position;
                    aisTarget.transform.position = targetPosition;
                }


                aIManager.checkpoints = checkpoints;
                aIManager.currentCheckpointIndex = currentCheckpoint;
                aIManager.nextCheckpoint = nextCheckpointAI;
                aIManager.nextCheckpointIndex = nextCheckpointIndex;

                //Debug.Log("correct direction");
            }
            else
            {
                //Debug.Log("wrong direction or missed checkpoint");

            }
        }
    }
}
