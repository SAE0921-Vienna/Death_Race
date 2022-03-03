using PlayerController;
using UnityEngine;

namespace AI
{
    public class AIManager : BaseVehicleManager
    {
        public AICheckpointManager ai_checkpointManager;
        public AIFollowCurve aiFollowCurve;
        
        protected override void Awake()
        {
            base.Awake();

            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            aiFollowCurve = GetComponent<AIFollowCurve>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            Debug.DrawLine(transform.position, FacingInfo().point, Color.red);

            currentSpeed = aiFollowCurve.speed;
            //isOnRoadtrack = _vehicleController.isOnRoadtrack;

            if (health <= 0)
            {
                isAlive = false;
                RespawnVehicleAI();
                health = healthLimit;
            }
        }

        public RaycastHit FacingInfo()
        {
            Physics.Raycast(transform.position, transform.forward, out var hit);
            return hit;
        }

        public void RespawnVehicleAI()
        {
            
            //Respawn when dead

            // Respawn when off Roadtrack

        }

        //CheckIfOnRoadtrack()

    }
}
