using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    // Encapsulate this.
    public float speed = 20f;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSteerAngle, steerSpeed;
    [SerializeField] private float sideThrustAmount;
    [SerializeField] private float brakeForce;
    
    private float steeringStrength;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Accelerate(speed);
        Brake(brakeForce);
        AntiGravity(GroundInfo());
        //SteerValue(maxSteerAngle, steerSpeed);
        SideThrust();
    }

    private void Update()
    {
        Steer(steeringStrength);
    }

    private void Accelerate(float accelerationSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * accelerationSpeed, ForceMode.Force);
        }
    }

    private void Brake(float brakeStrength)
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * brakeStrength, ForceMode.Force);
        }
    }

    /// <summary>
    /// Adds force towards the steering direction, to make steering feel more responsive.
    /// </summary>
    private void SideThrust()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * sideThrustAmount * Time.fixedDeltaTime, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right* sideThrustAmount * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private RaycastHit GroundInfo()
    {
        Physics.Raycast(transform.position, -transform.up, out var hit);
        return hit;
    }

    private void AntiGravity(RaycastHit hit)
    {
        //This kinda sucks, but it will do for now.
        rb.AddForce(-GroundInfo().normal * 100, ForceMode.Force);
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
            t = Mathf.MoveTowards(t, 0.5f, .01f * steerSpeed * Time.deltaTime);
        }
        return steeringStrength = Mathf.Lerp(-maxSteerStrength, maxSteerStrength, t);
    }

    private void Steer(float steerStrength)
    {
        var normal = GroundInfo().normal;
        var steeringAngle = new Vector3(normal.x, SteerValue(maxSteerAngle, steerSpeed), normal.z);

        print(steeringAngle);
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(steeringAngle * Time.fixedDeltaTime));
    }

    public float AngleGetter()
    {
        return t;
    }
}
