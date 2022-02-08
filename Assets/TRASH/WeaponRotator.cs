using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponRotator : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] protected float rotationSpeed = 100;
    [SerializeField] protected Quaternion currentRotation;

    protected Quaternion _targetRotation;
    public Transform weaponOnShip;
    private Camera _camera;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponent<WeaponRotator>().enabled = false;
        }


        weaponOnShip = GetComponent<Transform>();
        _camera = Camera.main;
    }

    private void Update()
    {
        RotateWeapon();
    }

    protected virtual void RotateWeapon()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
            Debug.DrawLine(transform.position, hit.point);

        _targetRotation = Quaternion.LookRotation(ray.direction);
        currentRotation = weaponOnShip.transform.rotation;

        var angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

        weaponOnShip.transform.rotation = angularDifference > 0
            ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
            : _targetRotation;
    }
}
