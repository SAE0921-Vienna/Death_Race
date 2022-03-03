using PlayerController;
using UnityEngine;

namespace AI
{
    public class AIManager : BaseVehicleManager
    {
        //public VehicleController ai_vehicleController;
        //public AI_VehicleController_ML ai_vehicleController;
        public AICheckpointManager ai_checkpointManager;
        
        protected override void Awake()
        {
            base.Awake();
            
            //ai_vehicleController = GetComponent<AI_VehicleController_ML>();
            //ai_checkpointManager = GetComponent<AICheckpointManager>();
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }

        protected override void Start()
        {
            base.Start();
            //nextCheckpoint = ai_checkpointManager.checkpointsInWorldList[0];
        }

        protected override void Update()
        {
            base.Update();
            Debug.DrawLine(transform.position, FacingInfo().point, Color.red);
        }

        public RaycastHit FacingInfo()
        {
            Physics.Raycast(transform.position, transform.forward, out var hit);
            return hit;
        }

        public void ResetCheckpoints()
        {
            //nextCheckpoint = ai_checkpointManager.checkpointsInWorldList[0];
            //nextCheckpointIndex = 0;
            //currentCheckpointIndex = 0;
            //currentLapIndex = 0;
        }

        //public void CheckLaps()
        //{
        //    if (currentLapIndex > _gameManager.laps)
        //    {
        //        currentLapIndex = _gameManager.laps;
        //        //Game Finish
        //        Debug.Log("YAY FINISH");
        //    }
        //    if (currentLapIndex < 0)
        //    {
        //        currentLapIndex = 0;
        //    }
        //}
    }
}
