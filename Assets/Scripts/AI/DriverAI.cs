using UnityEngine;

namespace AI
{
    public class DriverAI : MonoBehaviour
    {
        [SerializeField] private Transform targetPositionTransform;

        private AI_VehicleController aiVehicleController;
        private Vector3 targetPosition;

        [SerializeField] private float forwardAmount;
        public float ForwardAmount => forwardAmount;
        
        [SerializeField] private float turnAmount;
        public float TurnAmount => turnAmount;

        [SerializeField]
        private float forwardPlusValue = 1f;
        [SerializeField]
        private float turnPlusValue = 1f;


        [SerializeField]
        [Range(0, 100)]
        private float minDistanceToTarget = 40f;

        [SerializeField]
        [Range(0, 300f)]
        private float stoppingDistance = 100f;
        [SerializeField]
        [Range(0, 1)]
        private float stoppingSpeed = 0.1f;



        private void Awake()
        {
            aiVehicleController = GetComponent<AI_VehicleController>();
        }


        private void Update()
        {
            SetTargetPosition(targetPositionTransform.position);

            //distance to the target..
            var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            
            //if distance to target is greater than the reachedTarget --> too far
            if (distanceToTarget > minDistanceToTarget)
            {
                //Too far
                var dirToMovePosition = (targetPosition - transform.position).normalized;

                //if above 1 its infront, if below 0 its behind
                var dot = Vector3.Dot(transform.forward, dirToMovePosition);
                //Debug.Log(dot);

                //if target is infront
                if (dot > 0)
                {
                    //move forward
                    forwardAmount = forwardPlusValue;

                    //if distance to target is smaller than stopping distance and the current speed is greater than the stopping speed
                    if (distanceToTarget < stoppingDistance && aiVehicleController.currentSpeed > stoppingSpeed)
                    {
                        //"brake" - move backwards
                        forwardAmount = -forwardPlusValue;
                    }
                }
                else
                {
                    //if target is behind
                    var reverseDistance = 25f;                
                    if (distanceToTarget > reverseDistance)
                    {
                        //Too far to reverse
                        forwardAmount = forwardPlusValue;
                    }
                    else
                    {
                        forwardAmount = -forwardPlusValue;
                    }
                }

                //for turning
                //this bit needs Fixing!!!
                var angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
                //Debug.Log(angleToDir);
                if (angleToDir > 0)
                {
                    turnAmount = turnPlusValue;
                }
                else
                {
                    turnAmount = -turnPlusValue;
                }
            }
            else
            {
                //Reached target
                if (aiVehicleController.currentSpeed > 0.1f)
                {
                    forwardAmount = -forwardPlusValue;
                }
                else
                {
                    forwardAmount = 0f;
                }
                turnAmount = 0f;
            }
        }

        /// <summary>
        /// Sets the targetposition
        /// </summary>
        /// <param name="targetPositionTmp"></param>
        private void SetTargetPosition(Vector3 targetPositionTmp)
        {
            targetPosition = targetPositionTmp;
        }

    }
}
