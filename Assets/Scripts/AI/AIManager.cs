using PlayerController;
using UnityEngine;

namespace AI
{
    public class AIManager : BaseVehicleManager
    {
        public AIFollowCurve aiFollowCurve;

        protected override void Awake()
        {
            base.Awake();

            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            aiFollowCurve = GetComponent<AIFollowCurve>();
        }

        protected override void Update()
        {
            Debug.DrawLine(transform.position, FacingInfo().point, Color.red);
            currentSpeed = aiFollowCurve.Speed;
        }

        public RaycastHit FacingInfo()
        {
            Physics.Raycast(transform.position, transform.forward, out var hit);
            return hit;
        }

        public override void RespawnVehicle()
        {
            transform.position = aiFollowCurve.lastPosition;
            aiFollowCurve.Speed = 0f;
            base.RespawnVehicle();
        }
    }
}