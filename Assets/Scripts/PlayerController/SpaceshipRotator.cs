using UnityEngine;

namespace PlayerController
{
    public class SpaceshipRotator : MonoBehaviour
    {
        [Range(0f, 90f)] 
        [SerializeField] private float maxRotation;
        [SerializeField] private AnimationCurve rotationalCurve;
        
        private VehicleController _vehicleController;

        private void Awake()
        {
            _vehicleController = GetComponentInParent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceship(_vehicleController.AngleGetter(), maxRotation);
        }

        private void RotateSpaceship(float rotationStrength, float maximumRotation)
        {
            var rotationVector = new Vector3(0f, 0f,
                Mathf.Lerp(maximumRotation, -maximumRotation, rotationalCurve.Evaluate(rotationStrength)));
            
            transform.rotation = Quaternion.Euler(_vehicleController.transform.rotation.eulerAngles + rotationVector);
        }
    }
}