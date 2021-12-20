using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    // Encapsulate this.
    public float speed = 20f;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSteerAngle, steerSpeed;
    
    private float steeringStrength;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Accelerate(speed);
        Brake(speed);
        AntiGravity(GroundInfo());
        //SteerValue(maxSteerAngle, steerSpeed);
        
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
            rb.AddForce(new Vector3(0f, 0f, -brakeStrength), ForceMode.Force);
        }
    }

    private RaycastHit GroundInfo()
    {
        Physics.Raycast(transform.position, -transform.up, out var hit);
        return hit;
    }

    private void AntiGravity(RaycastHit hit)
    {
        rb.AddForce(-GroundInfo().normal * 100, ForceMode.Force);

       // print(hit.normal);
       
    }

    private float t = 0.5f;
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
            t = Mathf.MoveTowards(t, 0.5f, .01f * steerSpeed);
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
