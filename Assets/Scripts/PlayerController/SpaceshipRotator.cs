using System;
using UnityEngine;

namespace PlayerController
{
    public class SpaceshipRotator : MonoBehaviour
    {
        [Range(0f, 90f)] [SerializeField] private float maxRotation;
        private VehicleController _vehicleController;

        private void Awake()
        {
            _vehicleController = GameObject.Find("Spaceship").GetComponent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceship(_vehicleController.AngleGetter(), maxRotation);
        }

        private void RotateSpaceship(float rotationStrength, float maxRotation)
        {
            Vector3 targetRotation =
                new Vector3(transform.localRotation.x, transform.localRotation.y, rotationStrength);
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, targetRotation, rotationStrength));
        }
    }
}