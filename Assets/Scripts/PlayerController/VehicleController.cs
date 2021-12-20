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
        SteerValue(maxSteerAngle, steerSpeed);
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

    private float t;
    private void SteerValue(float maxSteerStrength, float steerSpeed)
    {
        steeringStrength = Mathf.Clamp(steeringStrength, -maxSteerStrength, maxSteerStrength);
        steeringStrength = Mathf.Lerp(-maxSteerStrength, maxSteerStrength, t);
        
        if (Input.GetKey(KeyCode.A))
        {
            t += 0.01f * steerSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            t -= 0.01f * steerSpeed;
        }
        else
        {
            steeringStrength = Mathf.MoveTowards(steeringStrength, 0.5f, steerSpeed);
        }
    }

    private void Steer(float steerStrength)
    {
        var normal = GroundInfo().normal;
        var steeringAngle = new Vector3(normal.x, steerStrength, normal.z);
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(-steeringAngle * Time.fixedDeltaTime));
    }

    public float AngleGetter()
    {
        return steeringStrength;
    }
}
