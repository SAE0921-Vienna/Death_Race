using UnityEngine;

namespace PlayerController
{
    public class SpaceshipRotator : MonoBehaviour
    {
        [Range(0f, 90f)] [SerializeField] private float maxRotation;
        [SerializeField] private AnimationCurve rotationalCurve;
        [SerializeField] GameObject spaceShip;
        private VehicleController _vehicleController;

        private void Awake()
        {
            //spaceShip = GetComponentInParent<GameObject>();
            _vehicleController = spaceShip.GetComponent<VehicleController>();
        }

        private void Update()
        {
            RotateSpaceship(_vehicleController.AngleGetter(), maxRotation);
        }

        private void RotateSpaceship(float rotationStrength, float maxRotation)
        {
            transform.rotation = Quaternion.Euler(spaceShip.transform.rotation.eulerAngles + new Vector3(0f, 0f, Mathf.Lerp(maxRotation, -maxRotation, rotationalCurve.Evaluate(rotationStrength))));
        }
    }
}