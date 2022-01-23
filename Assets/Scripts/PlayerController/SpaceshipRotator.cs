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
        private const float interpolationConstant = 2;


        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceshipZ(_vehicleController.SteeringAnimationValue(), maxRotationZ);
        }

        private void RotateSpaceshipZ(float rotationStrength, float maximumRotation)
        {
            rotationVector = new Vector3(RotateSpaceshipX(_vehicleController.AccelerationValue), 0f,
                Mathf.Lerp(maximumRotation, -maximumRotation, rotationalCurve.Evaluate(rotationStrength)));
            
            transform.rotation = Quaternion.Euler(_vehicleController.transform.localEulerAngles + rotationVector);
        }

        private float RotateSpaceshipX(float brakeValue)
        {
            xRotationInterpolator = brakeValue >= 0f ? Mathf.Clamp01(xRotationInterpolator - interpolationConstant * Time.deltaTime) : Mathf.Clamp01(xRotationInterpolator + 2 * Time.deltaTime);
            return Mathf.Lerp(0f, -maxRotationX, rotationalCurve.Evaluate(xRotationInterpolator));
        }
    }
}