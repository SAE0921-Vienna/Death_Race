using PlayerController;

namespace AI
{
    public class AISpaceshipRotator : SpaceshipRotator
    {
        protected override void Awake()
        {
            _vehicleController = GetComponentInParent<AI_VehicleController>();
        }
    }
}