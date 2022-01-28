using PlayerController;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class AI_VehicleController : VehicleController
    {
        private DriverAI _driverAI;
        
        protected override void Awake()
        {
            base.Awake();
            _driverAI = GetComponent<DriverAI>();
        }

        public override float AccelerationValue => _driverAI.ForwardAmount;
        protected override float SteerValueRaw => _driverAI.TurnAmount;
    }
}
