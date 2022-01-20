using System;
using PlayerController;
using UnityEngine;

namespace AIVehicleController
{
    [RequireComponent(typeof(Rigidbody))]
    public class AI_VehicleController : MonoBehaviour
    {
        [Header("Acceleration and Braking")]
        [Range(0.1f, 3f)]
        public float mAccelerationConstant = 1f;
        [Range(50f, 3000f)]
        public float mMaxSpeed;

        [Range(10f, 800f)]
        [SerializeField] private float brakeForce;
        [Range(0.1f, 5f)]
        [SerializeField] private float decelerationConstant;
        [SerializeField] private AnimationCurve accelerationCurve;

        [Header("Steering")]
        [Range(0f, 1000f)]
        [SerializeField] private float sideThrustAmount;
        [Range(0f, 200f)]
        [SerializeField] private float maxSteerAngle, steerSpeed;

        [Header("Anti Gravity")]
        [Range(1, 2000f)]
        [SerializeField] private float downForceMultiplier;
        [Range(1, 2000f)]
        [SerializeField] private float hoverMultiplier;

        [SerializeField] private float hoverHeight;
        [SerializeField] private PIDController pidController;

        [Header("Is on Track")] public bool isOnRoadtrack;

        private Rigidbody _rBody;
        private InputActions _controls;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _controls = new InputActions();
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public float currentSpeed;
        public float AccelerationValue; /*=> _controls.Player.AccelerateDecelerate.ReadValue<float>();*/
        public float SteerValueRaw; /*=> _controls.Player.Steer.ReadValue<float>();*/

        private void FixedUpdate()
        {
            Accelerate();
            Brake();
            AntiGravity();
            SideThrust();
        }

        private void Update()
        {
            Steer();
        }

        public void Accelerate()
        {
            currentSpeed = AccelerationValue >= 0.01f ? Mathf.Clamp01(currentSpeed += 0.01f * mAccelerationConstant) : Mathf.Clamp01(currentSpeed -= 0.01f * decelerationConstant);
            _rBody.AddForce(transform.forward * mMaxSpeed * accelerationCurve.Evaluate(currentSpeed), ForceMode.Force);
        }

        public void Brake()
        {
            _rBody.AddForce(transform.forward * (brakeForce * AccelerationValue), ForceMode.Force);
        }

        /// <summary>
        /// Adds force towards the steering direction, to make steering feel more responsive.
        /// </summary>
        private void SideThrust()
        {
            _rBody.AddForce(transform.right * (sideThrustAmount * SteerValueRaw * Time.fixedDeltaTime), ForceMode.Force);
        }

        private RaycastHit GroundInfo()
        {
            isOnRoadtrack = Physics.Raycast(transform.position, -transform.up, out var hit);
            return hit;
        }

        private void AntiGravity()
        {
            Vector3 groundNormal;
            if (isOnRoadtrack)
            {
                //...determine how high off the ground it is...
                float height = GroundInfo().distance;
                //...save the normal of the ground...21
                groundNormal = GroundInfo().normal.normalized;
                //...use the PID controller to determine the amount of hover force needed...
                float forcePercent = pidController.Seek(hoverHeight, height);

                //...calulcate the total amount of hover force based on normal (or "up") of the ground...
                Vector3 force = groundNormal * hoverMultiplier * forcePercent;
                //...calculate the force and direction of gravity to adhere the ship to the 
                //track (which is not always straight down in the world)...
                Vector3 gravity = -groundNormal * downForceMultiplier * (height / 100);

                //...and finally apply the hover and gravity forces
                _rBody.AddForce(force, ForceMode.Acceleration);
                _rBody.AddForce(gravity, ForceMode.Acceleration);
            }
            //...Otherwise...
            else
            {
                //...use Up to represent the "ground normal". This will cause our ship to
                //self-right itself in a case where it flips over
                groundNormal = Vector3.up;

                //Calculate and apply the stronger falling gravity straight down on the ship
                Vector3 gravity = -groundNormal * downForceMultiplier;
                _rBody.AddForce(gravity, ForceMode.Acceleration);
            }
            //Calculate the amount of pitch and roll the ship needs to match its orientation
            //with that of the ground. This is done by creating a projection and then calculating
            //the rotation needed to face that projection
            Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
            Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);

            //Move the ship over time to match the desired rotation to match the ground. This is 
            //done smoothly (using Lerp) to make it feel more realistic
            _rBody.MoveRotation(Quaternion.Lerp(_rBody.rotation, rotation, Time.deltaTime * 10f));

            //Calculate the angle we want the ship's body to bank into a turn based on the current rudder.
            //It is worth noting that these next few steps are completetly optional and are cosmetic.
            //It just feels so darn cool
            //float angle = angleOfRoll * -input.rudder;

            //Calculate the rotation needed for this new angle
            //Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
            //Finally, apply this angle to the ship's body
            //shipBody.rotation = Quaternion.Lerp(shipBody.rotation, bodyRotation, Time.deltaTime * 10f);
        }

        /// <summary>
        /// Applies the rotation of the Y-Axis combined with the normal of the road, to make steering work everywhere,
        /// regardless of the world-coordinate rotation of the road.
        /// </summary>
        private void Steer()
        {
            var normal = GroundInfo().normal;
            var steeringAngle = new Vector3(normal.x, SteerValue(maxSteerAngle, steerSpeed), normal.z);

            _rBody.MoveRotation(_rBody.rotation * Quaternion.Euler(steeringAngle * Time.fixedDeltaTime));
        }

        private float t = 0.5f;
        /// <summary>
        /// Returns a float between 0 and 1, which represents interpolator between 2 extremes of steering (-maxSteering, maxSteering).
        /// </summary>
        private float SteerValue(float maxSteerStrength, float steerSpeed)
        {
            t = Mathf.Clamp01(t);
            if (Mathf.Approximately(SteerValueRaw, 0f))
            {
                t = Mathf.MoveTowards(t, 0.5f, .005f * steerSpeed * Time.deltaTime);
            }
            else
            {
                t += .01f * steerSpeed * SteerValueRaw * Time.deltaTime;
            }
            return Mathf.Lerp(-maxSteerStrength, maxSteerStrength, t);
        }

        public float AngleGetter() => t;
    }
}
