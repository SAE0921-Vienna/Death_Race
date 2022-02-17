using System;
using PlayerController;

namespace AI
{
    public class AI_VehicleController_ML : VehicleController
    {
        private VehicleAgent _vehicleAgent;
        
        protected override void Awake()
        {
            base.Awake();
            _vehicleAgent = GetComponent<VehicleAgent>();
        }

        private void Update()
        {
            if (isOnRoadtrack == false)
            {
                _vehicleAgent.ResetOnFalloff();
            }

            print(AccelerationValue);
            print(SteerValueRaw);
        }

        public override float AccelerationValue => _vehicleAgent.inputActions[0];
        protected override float SteerValueRaw => _vehicleAgent.inputActions[1];
    }
}