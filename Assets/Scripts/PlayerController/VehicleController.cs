using UnityEngine;

// Convert all this to the new input system.

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
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
    [Range(50f, 2000f)]
    [SerializeField] private float downForceMultiplier;

    [Header("MISC")] [SerializeField] private float rotationalSmoothingDelta;
    
    
    
    private Rigidbody rBody;
    
    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Brake(brakeForce);
        AntiGravity();
        SideThrust();
    }

    private void Update()
    {
        Steer();
    }
    
    [HideInInspector]
    public float currentSpeed;
    
    private void Accelerate()
    {
        currentSpeed = Input.GetKey(KeyCode.W) ? Mathf.Clamp01(currentSpeed += 0.01f * mAccelerationConstant) : Mathf.Clamp01(currentSpeed -= 0.01f * decelerationConstant);
        rBody.AddForce(transform.forward * mMaxSpeed * accelerationCurve.Evaluate(currentSpeed), ForceMode.Force);
    }

    private void Brake(float brakeStrength)
    {
        if (Input.GetKey(KeyCode.S))
        {
            rBody.AddForce(-transform.forward * brakeStrength, ForceMode.Force);
        }
    }

    /// <summary>
    /// Adds force towards the steering direction, to make steering feel more responsive.
    /// </summary>
    private void SideThrust()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rBody.AddForce(-transform.right * sideThrustAmount * Time.fixedDeltaTime, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rBody.AddForce(transform.right * sideThrustAmount * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private RaycastHit GroundInfo()
    {
        Physics.Raycast(transform.position, -transform.up, out var hit);
        return hit;
    }

    private void AntiGravity()
    {
        //Debug.DrawRay(transform.position, -transform.up * downForceMultiplier, Color.green);
        rBody.AddForce(-GroundInfo().normal * downForceMultiplier, ForceMode.Force);
    }

    private float t = 0.5f;
    
    /// <summary>
    /// Returns a float between 0 and 1, which represents interpolator between 2 extremes of steering (-maxSteering, maxSteering).
    /// </summary>
    private float SteerValue(float maxSteerStrength, float steerSpeed)
    {
        t = Mathf.Clamp01(t);
        if (Input.GetKey(KeyCode.A))
        {
            t -= .01f * steerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            t += .01f * steerSpeed * Time.deltaTime;
        }
        else
        {
            t = Mathf.MoveTowards(t, 0.5f, .005f * steerSpeed * Time.deltaTime);
        }
        return Mathf.Lerp(-maxSteerStrength, maxSteerStrength, t);
    }

    /// <summary>
    /// Applies the rotation of the Y-Axis combined with the normal of the road, to make steering work everywhere,
    /// regardless of the world-coordinate rotation of the road.
    /// </summary>
    private void Steer()
    {
        var normal = GroundInfo().normal;
        var steeringAngle = new Vector3(normal.x, SteerValue(maxSteerAngle, steerSpeed), normal.z);
        
        
        rBody.MoveRotation(rBody.rotation * Quaternion.Euler(steeringAngle * Time.fixedDeltaTime));
    }

    public float AngleGetter()
    {
        return t;
    }
}
