using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotater : MonoBehaviour
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


    void Update()
    {

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);
        }

        targetRotation = Quaternion.LookRotation(ray.direction);

        currentRotation = weaponOnShip.transform.rotation;

        float angularDifference = Quaternion.Angle(currentRotation, targetRotation);

        if (angularDifference > 0)
        {
            weaponOnShip.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference);
        }
        else
        {
            weaponOnShip.transform.rotation = targetRotation;
        }





    }




}
