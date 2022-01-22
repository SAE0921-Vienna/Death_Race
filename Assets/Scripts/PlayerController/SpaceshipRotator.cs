using UnityEngine;

namespace PlayerController
{
    public class SpaceshipRotator : MonoBehaviour
    {
        [Range(0f, 90f)] 
        [SerializeField] private float maxRotationZ, maxRotationX;
        [SerializeField] private AnimationCurve rotationalCurve;
        
        private VehicleController _vehicleController;
        private Vector3 rotationVector;

        private float xRotationInterpolator;


        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceshipZ(_vehicleController.AngleGetter(), maxRotationZ);
        }

        private void RotateSpaceshipZ(float rotationStrength, float maximumRotation)
        {
            rotationVector = new Vector3(RotateSpaceshipX(_vehicleController.AccelerationValue), 0f,
                Mathf.Lerp(maximumRotation, -maximumRotation, rotationalCurve.Evaluate(rotationStrength)));
            
            transform.rotation = Quaternion.Euler(_vehicleController.transform.rotation.eulerAngles + rotationVector);
        }

        private float RotateSpaceshipX(float brakeValue)
        {
            if (brakeValue >= 0f)
            {
                xRotationInterpolator =
                    Mathf.Clamp01(xRotationInterpolator - Time.deltaTime);
                print("brake value higher or equal to 0");
            }
            else
            {
                xRotationInterpolator =
                    Mathf.Clamp01(xRotationInterpolator + Time.deltaTime);
            }
                

            print(xRotationInterpolator);
            //print(_vehicleController.currentSpeed);
            return Mathf.Lerp(0f, -maxRotationX, rotationalCurve.Evaluate(xRotationInterpolator));
        }
    }
}