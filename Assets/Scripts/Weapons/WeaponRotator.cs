using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{

    public Transform weaponOnShip;
    private Camera _camera;
    [Range(0, 1)]
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private Quaternion currentRotation;
    private Quaternion targetRotation;


    private void Awake()
    {
        weaponOnShip = GetComponent<Transform>();
        _camera = Camera.main;
    }


    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            Debug.DrawLine(transform.position, hit.point);
        

        targetRotation = Quaternion.LookRotation(ray.direction);

        currentRotation = weaponOnShip.transform.rotation;

        var angularDifference = Quaternion.Angle(currentRotation, targetRotation);

        weaponOnShip.transform.rotation = angularDifference > 0 ? Quaternion.Slerp(currentRotation, targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference) : targetRotation;
    }
}
