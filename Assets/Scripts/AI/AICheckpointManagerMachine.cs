using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    public class AICheckpointManagerMachine : CheckpointManager
    {
        private VehicleAgent _vehicleAgent;
        
        private void Start()
        {
            _vehicleAgent = GetComponent<VehicleAgent>();
        }

        public override void VehicleThroughCheckpoint(Checkpoint checkpoint, Collider vehicle)
        {
            base.VehicleThroughCheckpoint(checkpoint, vehicle);
            _vehicleAgent.AddAgentReward(1f);
        }
    }
}