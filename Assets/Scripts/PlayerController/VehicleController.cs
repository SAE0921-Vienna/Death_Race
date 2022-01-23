using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleController : MonoBehaviour
    {
        #region Variables

        [Header("Acceleration and Braking")]
        [Range(0.1f, 3f)]
        public float mAccelerationConstant = 1f;
        [Range(50f, 3000f)]
        public float mMaxSpeed;
        [HideInInspector] public float currentSpeed;

        [Range(10f, 800f)]
        [SerializeField] private float brakeForce;
        [Range(0.1f, 5f)]
        [SerializeField] private float decelerationConstant;
        [SerializeField] private AnimationCurve accelerationCurve;
        
        [Header("Steering")]
        [Range(0f, 1000f)]
        [SerializeField] private float sideThrustAmount;
        [Range(0f, 200f)]
        [SerializeField] private float animationSteerSpeed;
        [Range(0f, 1f)]
        [SerializeField] private float idleSteeringAnimationSpeedMultiplier, steeringAnimationSpeedMultiplier;
        private const float steerAnimationConstant = 2f;

        [Header("Anti Gravity")]
        [Range(1, 2000f)]
        [SerializeField] private float downForceMultiplier;
        [Range(1, 2000f)]
        [SerializeField] private float hoverMultiplier;

        [SerializeField] private float hoverHeight;
        [SerializeField] private PIDController pidController;

        [Header("Track Information")]
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask wallLayerMask;
        [SerializeField] private float maxRaycastDistance;
        [SerializeField] private float trackSearchRadius;
        public bool isOnRoadtrack;
        

        private Rigidbody _rBody;
        private Ray _ray;
        private InputActions _controls;
        private List<float> trackDistances = new List<float>();
        private RaycastHit hit;

        #endregion

        #region De-Initialization
        
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

        
        public float AccelerationValue => _controls.Player.AccelerateDecelerate.ReadValue<float>();
        private float SteerValueRaw => _controls.Player.Steer.ReadValue<float>();

        #endregion

        private void FixedUpdate()
        {
            Accelerate();
            Brake();
            Steer();
            AntiGravity();
            SideThrust();
        }

        private void Accelerate()
        {
            currentSpeed = AccelerationValue >= 0.01f && isOnRoadtrack ? Mathf.Clamp01(currentSpeed += 0.01f * mAccelerationConstant * Time.fixedDeltaTime * 100) : Mathf.Clamp01(currentSpeed -= 0.01f * decelerationConstant * Time.fixedDeltaTime * 100);
            _rBody.AddForce(transform.forward * mMaxSpeed * accelerationCurve.Evaluate(currentSpeed), ForceMode.Acceleration);
        }

        private void Brake()
        {
            _rBody.AddForce(transform.forward * (brakeForce * AccelerationValue * Time.fixedDeltaTime * 10), ForceMode.Force);
        }

        /// <summary>
        /// Adds force towards the steering direction, to make steering feel more responsive.
        /// </summary>
        private void SideThrust()
        {
            _rBody.AddForce(transform.right * (sideThrustAmount * SteerValueRaw * Time.fixedDeltaTime * 100), ForceMode.Force);
        }
        private RaycastHit GroundInfo()
        {
            var ray = new Ray(transform.position, -transform.up);
            if (!isOnRoadtrack)
            {
                var availableTracks = Physics.SphereCastAll(ray, trackSearchRadius, 1f, layerMask);
                var sortedTracks = availableTracks.OrderBy(tracks => tracks.distance).ToList();
            }
            
            isOnRoadtrack = Physics.Raycast(transform.position, -transform.up, out hit, maxRaycastDistance);
            return hit;
        }

        /// <summary>
        /// Uses PID Controller to create a constant balance between gravity (down on local y-axis) and force (up on local y-axis).
        /// This creates a smooth flying experience. If it detects no ground underneath, it automatically uses the world y-axis as the gravitational direction.
        /// The ship itself gets rotated along a Vector that gets projected on a plane. (With interpolation)
        /// </summary>
        private void AntiGravity()
        {
            Vector3 groundNormal;
            if (isOnRoadtrack)
            {
                var height = GroundInfo().distance;
                groundNormal = GroundInfo().normal.normalized;
                
                //...use the PID controller to determine the amount of hover force needed...
                var forcePercent = pidController.Seek(hoverHeight, height);

                //...calculate the total amount of hover force based on normal (or "up") of the ground...
                var force = groundNormal * hoverMultiplier * forcePercent;
                //...calculate the force and direction of gravity to adhere the ship to the track.
                var gravity = -groundNormal * downForceMultiplier * (height / 100);

                //...and finally apply the hover and gravity forces
                _rBody.AddForce(force, ForceMode.Acceleration);
                _rBody.AddForce(gravity, ForceMode.Acceleration);
            }
            else
            {
                //...use Up to represent the ground normal.
                groundNormal = Vector3.up;

                //Calculate and apply the stronger falling gravity straight down on the ship
                var gravity = -groundNormal * downForceMultiplier;
                _rBody.AddForce(gravity, ForceMode.Acceleration);
            }
            //Calculate the amount of pitch and roll the ship needs to match its orientation with that of the ground. 
            var projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
            var rotation = Quaternion.LookRotation(projection, groundNormal);

            //Move the ship over time to match the desired rotation to match the ground.
            _rBody.MoveRotation(Quaternion.Lerp(_rBody.rotation, rotation, Time.deltaTime * 10f));
        }

        #region Steering
        
        /// <summary>
        /// Applies the rotation of the local Y-Axis, by adding a torque towards an angle, resulting in somewhat realistic flying turn behavior.
        /// It then calculates the friction of the turn, by taking the dot-product of the current velocity * transform.right to get the magnitude of sidewards-movement.
        /// Finally the calculated force gets applied via Acceleration.
        /// </summary>
        private void Steer()
        {
            var steeringAngle = Vector3.up * animationSteerSpeed * SteerValueRaw * Time.fixedDeltaTime;
            _rBody.AddRelativeTorque(steeringAngle, ForceMode.VelocityChange);
            
            var right = transform.right;
            var sidewaysSpeed = Vector3.Dot(_rBody.velocity, right);
            
            var sideFriction = -right * (sidewaysSpeed / Time.fixedDeltaTime / 4f); 

            //Finally, apply the sideways friction
            _rBody.AddForce(sideFriction, ForceMode.Acceleration);
        }

        private float t = 0.5f;
        /// <summary>
        /// Returns a float between 0 and 1, which represents interpolator between 2 extremes of steering (-maxSteering, maxSteering).
        /// </summary>
        public float SteeringAnimationValue()
        {
            t = Mathf.Clamp01(t);
            if (Mathf.Approximately(SteerValueRaw, 0f))
            {
                t = Mathf.MoveTowards(t, 0.5f, steerAnimationConstant * idleSteeringAnimationSpeedMultiplier * Time.deltaTime);
            }
            else
            {
                t += steerAnimationConstant * steeringAnimationSpeedMultiplier * SteerValueRaw * Time.deltaTime;
            }
            return t;
        }
        #endregion
        
        private void OnCollisionStay(Collision collision)
        {
            //If the ship has collided with an object on the Wall layer...
            if (collision.gameObject.layer != wallLayerMask) return;

            var up = transform.up;
            var upwardForceFromCollision = Vector3.Dot(collision.impulse, up) * up;
            _rBody.AddForce(-upwardForceFromCollision, ForceMode.Impulse);
        }
    }
}
