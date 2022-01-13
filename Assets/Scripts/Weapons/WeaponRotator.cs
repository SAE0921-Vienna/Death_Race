using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private Quaternion currentRotation;

    public Transform weaponOnShip;
    private Camera _camera;
    private Quaternion _targetRotation;

    private void Awake()
    {
        weaponOnShip = GetComponent<Transform>();
        _camera = Camera.main;
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
            Debug.DrawLine(transform.position, hit.point);

        _targetRotation = Quaternion.LookRotation(ray.direction);
        currentRotation = weaponOnShip.transform.rotation;


        var angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

        weaponOnShip.transform.rotation = angularDifference > 0 ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference) : _targetRotation;
    }
}
