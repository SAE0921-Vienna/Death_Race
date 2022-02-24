using PlayerController;

namespace AI
{
    using Unity.MLAgents;
    using Unity.MLAgents.Actuators;
    using Unity.MLAgents.Sensors;
    using UnityEngine;
    using Random = UnityEngine.Random;
    
    public class VehicleAgent : Agent
    {
        private CheckpointManager _checkpointManager;
        private Vector3 _spawnPositionVector;
        private AIManager _aiManager;

        [HideInInspector]
        public float[] inputActions = new float[2]; //These are the input actions for the 
                                                    //Vehicle Controller.
    
        private void Awake()
        {
            _checkpointManager = FindObjectOfType<CheckpointManager>();
            _aiManager = GetComponent<AIManager>();
            
            var myTransform = transform;

            _spawnPositionVector = myTransform.position;
        }
        
        /* vectorAction – An array containing the action vector. The length of the array is
         specified by the BrainParameters of the agent's associated BehaviorParameters component */
        public override void OnActionReceived(ActionBuffers actions)
        {
            inputActions[0] = actions.ContinuousActions[0];
            inputActions[1] = actions.ContinuousActions[1];
        }
        
    
        /* Use this to add extra data from the environment.
        The Ray Perception Sensors Input gets added automatically, and no longer
        needs to be referenced by hand. */
        public override void CollectObservations(VectorSensor sensor)
        {
            var checkpointForward = 
                _checkpointManager.checkpointsInWorldList[_aiManager.nextCheckpointIndex].transform.forward;
            
            var directionalDot = Vector3.Dot(transform.forward, checkpointForward);
            var signedAngle = Vector3.SignedAngle(transform.forward,
                _checkpointManager.checkpointsInWorldList[_aiManager.nextCheckpointIndex].transform.position,
                Vector3.up);
            sensor.AddObservation(signedAngle);
        }
        
        /* On each new Episode (Reset of the Vehicle) this gets called,
        meaning in this case the spaceship gets reset to the start-position. */
        public override void OnEpisodeBegin()
        {
            var vehicleController = GetComponent<VehicleController>();
            transform.position = transform.position = _spawnPositionVector + new Vector3(Random.Range(-50, 50), 0f, Random.Range(-50, 50));
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            
            _aiManager.ResetCheckpoints();
            vehicleController.ResetVehicle();
        }
        
        /* Add (magnitude) reward to agent.
        Magnitude should not exceed ABS. 1. (-1 ; 1) */
        public void AddAgentReward(float magnitude)
        {
            Debug.LogFormat("Added {0} Reward", magnitude);
            AddReward(magnitude);
        }
    
        /* This is responsible for manual player input, should it be needed.
        This function only gets called if the Behavior Type is set to "Heuristic Only". */
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            
            continuousActions[0] = Input.GetAxisRaw("Vertical");
            continuousActions[1] = Input.GetAxisRaw("Horizontal");
        } 
    
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                AddAgentReward(-1F);
                //EndEpisode();
            }
        }
    
        public void ResetOnFalloff()
        {
            if (GetComponent<VehicleController>().isOnRoadtrack) return;
            EndEpisode();
        }
    }
}