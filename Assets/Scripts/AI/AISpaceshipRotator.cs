using UnityEngine;

namespace AIVehicleController
{
    public class AISpaceshipRotator : MonoBehaviour
    {
        [Range(0f, 90f)] [SerializeField] private float maxRotation;
        [SerializeField] private AnimationCurve rotationalCurve;
        [SerializeField] GameObject spaceShip;
        private AI_VehicleController ai_VehicleController;

        private void Awake()
        {
            //spaceShip = GetComponentInParent<GameObject>();
            ai_VehicleController = spaceShip.GetComponent<AI_VehicleController>();
        }

        private void Update()
        {
            RotateSpaceship(ai_VehicleController.AngleGetter(), maxRotation);
        }

        private void RotateSpaceship(float rotationStrength, float maxRotation)
        {
            transform.rotation = Quaternion.Euler(spaceShip.transform.rotation.eulerAngles + new Vector3(0f, 0f, Mathf.Lerp(maxRotation, -maxRotation, rotationalCurve.Evaluate(rotationStrength))));
        }
    }
}