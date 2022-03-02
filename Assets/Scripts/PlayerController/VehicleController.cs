using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Core;
using UnityEngine;

namespace PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleController : MonoBehaviour
    {
        #region Variables

        [Header("Acceleration and Braking")]
        [Range(0.15f, 0.3f)]
        public float mAccelerationConstant = 1f;
        [Range(950f, 1250f)]
        public float mMaxSpeed;
        [HideInInspector]
        public float currentSpeed;
        [HideInInspector]
        public float currentBackwardsSpeed;


        [Range(10f, 1500f)]
        [SerializeField] protected float brakeForce;
        [Range(0.1f, 1f)]
        [SerializeField] protected float decelerationConstant = 0.3f;
        [SerializeField] protected AnimationCurve accelerationCurve;

        [Header("Steering")]
        [Range(0f, 1000f)]
        [SerializeField] protected float sideThrustAmount;

        protected Vector3 steeringAngle;
        [Range(5f, 12f)]
        public float steeringSpeed;

        [Range(2f, 5f)]
        public float speedDependentAngularDragMagnitude;
        [Range(0f, 1f)]
        [SerializeField] protected float idleSteeringAnimationSpeedMultiplier, steeringAnimationSpeedMultiplier;

        [Header("Anti Gravity")]
        [Range(1, 2000f)]
        [SerializeField] protected float downForceMultiplier;
        [Range(1, 2000f)]
        [SerializeField] protected float hoverMultiplier;

        [SerializeField] protected float hoverHeight;
        [SerializeField] protected PIDController pidController;

        [Header("Track Information")]
        [SerializeField] protected LayerMask layerMask;
        [SerializeField] protected LayerMask wallLayerMask;
        [SerializeField] protected float maxSphereCastDistance;
        [SerializeField] protected float sphereCastRadius;
        //[SerializeField] protected float trackSearchRadius;
        public bool isOnRoadtrack;

        private Rigidbody _rBody;
        private InputActions _controls;
        private RaycastHit hit;
        private Timer _timer;
        //private RaycastHit wallHit;
        private const float steerAnimationConstant = 2f;
        private float drag;
        private bool canDrive;
        [SerializeField] private float timerDuration = 1f;

        #endregion

        #region De-Initialization

        protected virtual void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
            //_vehicleAgent = GetComponent<VehicleAgent>();
            drag = mAccelerationConstant * 10 / mMaxSpeed;
            _timer = FindObjectOfType<Timer>();

            _timer.CreateTimer(timerDuration, () => { canDrive = true; Debug.Log("Timer has ended"); });
        }

        protected void OnEnable()
        {
            _controls = new InputActions();
            _controls.Enable();
        }

        protected void OnDisable()
        {
            _controls.Disable();
        }


        public virtual float AccelerationValue => _controls.Player.AccelerateDecelerate.ReadValue<float>();
        protected virtual float SteerValueRaw => _controls.Player.Steer.ReadValue<float>();

        #endregion

        protected void FixedUpdate()
        {
            Accelerate();
            Brake();
            Steer();
            AntiGravity();
            SideThrust();
            GroundInfo();
        }


        /// <summary>
        /// Checks if the current acceleration axis is above threshold, if true, increments interpolator of acceleration curve.
        /// It also functions as an idle-slow-down, because if false, the interpolator decrements, resulting in a loss of movement speed.
        /// </summary>
        protected void Accelerate()
        {
            if (!canDrive) return;
            
            currentSpeed = AccelerationValue >= 0.01f && isOnRoadtrack ? Mathf.Clamp01(currentSpeed += 0.01f * mAccelerationConstant * Time.fixedDeltaTime * 100)
                : Mathf.Clamp01(currentSpeed -= 0.01f * decelerationConstant * Time.fixedDeltaTime * 100);
            _rBody.AddForce(transform.forward * (mMaxSpeed * accelerationCurve.Evaluate(currentSpeed) - drag), ForceMode.Acceleration);
        }

        /// <summary>
        /// Adds a force, towards the negative of the Z-Axis. This functions as a brake and a reverse gear at the same time.
        /// </summary>
        protected void Brake()
        {
            if (AccelerationValue > 0f) return;
            if (!canDrive) return;

            currentBackwardsSpeed = AccelerationValue <= -0.01f && isOnRoadtrack && currentSpeed <= 0.01f ? Mathf.Clamp01(currentBackwardsSpeed += 0.01f * mAccelerationConstant * Time.fixedDeltaTime * 200)
                : Mathf.Clamp01(currentBackwardsSpeed -= 0.01f * decelerationConstant * Time.fixedDeltaTime * 500);

            _rBody.AddForce(transform.forward * (brakeForce * AccelerationValue * Time.fixedDeltaTime * 10), ForceMode.Acceleration);
        }

        /// <summary>
        /// Uses PID Controller to create a constant balance between gravity (down on local y-axis) and force (up on local y-axis).
        /// This creates a smooth flying experience. If it detects no ground underneath, it automatically uses the world y-axis as the gravitational direction.
        /// The ship itself gets rotated along a Vector that gets projected on a plane. (With interpolation)
        /// </summary>
        protected void AntiGravity()
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

        /// <summary>
        /// Adds force towards the steering direction, to make steering feel more responsive.
        /// </summary>
        protected void SideThrust()
        {
            if (!canDrive) return;
            _rBody.AddForce(transform.right * (sideThrustAmount * SteerValueRaw * Time.fixedDeltaTime * 100), ForceMode.Force);
        }
        protected RaycastHit GroundInfo()
        {
            var myTransform = transform;
            var position = myTransform.position;
            var down = -myTransform.up;


            if (Physics.SphereCast(position, sphereCastRadius, down, out hit, maxSphereCastDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                isOnRoadtrack = true;
            }
            else
            {
                isOnRoadtrack = false;
            }
            return hit;
        }
        #region Steering

        /// <summary>
        /// Applies the rotation of the local Y-Axis, by adding a torque towards an angle, resulting in somewhat realistic flying turn behavior.
        /// The amount of torque is dependent on the current speed variable, as higher speed equals higher angular drag (not of the rigidbody but added locally here).
        /// It then calculates the friction of the turn, by taking the dot-product of the current velocity * transform.right to get the magnitude of sidewards-movement.
        /// Finally the calculated force gets applied via Acceleration.
        /// </summary>
        protected void Steer()
        {
            if (!canDrive) return;
            steeringAngle = Vector3.up * ((steeringSpeed - speedDependentAngularDragMagnitude * currentSpeed) * SteerValueRaw * Time.fixedDeltaTime);
            _rBody.AddRelativeTorque(steeringAngle, ForceMode.VelocityChange);

            var right = transform.right;
            var sidewaysSpeed = Vector3.Dot(_rBody.velocity, right);

            var sideFriction = -right * (sidewaysSpeed / Time.fixedDeltaTime / 2f);
            _rBody.AddForce(sideFriction, ForceMode.Acceleration);
        }

        protected float t = 0.5f;
        /// <summary>
        /// Returns a float between 0 and 1, which represents interpolator between 2 extremes of steering (-maxSteering, maxSteering).
        /// </summary>
        public float AnimationInterpolator()
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

        public int GetSpeed()
        {
            if (currentSpeed <= 0.01f)
            {
                return Mathf.RoundToInt((-currentBackwardsSpeed * brakeForce) * 0.3f);
            }
            else
            {
                return Mathf.RoundToInt(currentSpeed * mMaxSpeed);
            }
        }

        protected void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Wall")) return;

            var up = transform.up;
            var upwardForceFromCollision = Vector3.Dot(collision.impulse, up) * up;
            _rBody.AddForce(-upwardForceFromCollision, ForceMode.Impulse);

            currentSpeed = Mathf.Clamp01(currentSpeed - 0.001f * Mathf.Clamp(collision.impulse.magnitude, 0f, float.MaxValue));
            //print(collision.impulse.magnitude);
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(GroundInfo().point, sphereCastRadius);
        }

        public void ResetVehicle()
        {
            currentSpeed = 0f;
            steeringAngle = Vector3.zero;
        }
    }
}