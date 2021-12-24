using System;
using UnityEngine;

namespace PlayerController
{
    public class VehicleMeshSmoothing : MonoBehaviour
    {
        private Transform _target;

        private void Awake()
        {
            _target = GetComponentInParent<Transform>();
        }

        private void FixedUpdate()
        {
            transform.position = _target.position;
        }
    }
}