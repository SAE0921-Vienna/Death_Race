using System;
using UnityEngine;

namespace PlayerController
{
    public class VehicleMeshSmoothing : MonoBehaviour
    {
        [SerializeField] Transform target;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = target.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _rb.velocity.magnitude);
        }
    }
}