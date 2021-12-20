using System;
using UnityEngine;

namespace PlayerController
{
    public class SpaceshipRotator : MonoBehaviour
    {
        private VehicleController _vehicleController;

        private void Awake()
        {
            _vehicleController = GameObject.Find("Spaceship").GetComponent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceship(_vehicleController.AngleGetter());
        }

        private void RotateSpaceship(float rotationStrength)
        {
            float normalized = (rotationStrength - -60) / (60 - -60);
            print(normalized);
            print(rotationStrength);

            Vector3 targetRotation =
                new Vector3(transform.localRotation.x, transform.localRotation.y, rotationStrength);
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, targetRotation, normalized));
        }
    }
}