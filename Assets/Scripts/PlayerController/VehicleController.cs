using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float steeringStrength = 50f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Accelerate(speed);
        Brake(speed);
        AntiGravity(CheckGround());
        Steer(steeringStrength);
    }

    private void Accelerate(float accelerationSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0f, 0f, accelerationSpeed), ForceMode.Force);
        }
    }
    
    private void Brake(float brakeStrength)
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0f, 0f, -brakeStrength), ForceMode.Force);
        }
    }

    private RaycastHit CheckGround()
    {
        return Physics.Raycast(transform.position, -transform.up, out var hit) ? hit : new RaycastHit();
    }
    
    private void AntiGravity(RaycastHit hit)
    {
        //Smooth Interpolation towards target hit point
        transform.position = Vector3.MoveTowards(transform.position, hit.point + hit.normal, 1);
        //Rotates the Spaceship along the normal of the road
        transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        
        print (hit.normal);
    }

    private void Steer(float steerStrength)
    {
        var steeringAngle = new Vector3(0f, steerStrength, 0f);
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.MoveRotation(Quaternion.Euler(-steeringAngle * Time.fixedDeltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MoveRotation(Quaternion.Euler(steeringAngle * Time.fixedDeltaTime));
        }
    }
}
