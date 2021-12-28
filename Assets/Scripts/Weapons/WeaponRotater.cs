using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotater : MonoBehaviour
{

    public Transform weaponOnShip;
    [SerializeField] private float yrotationClamp = 90f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float weaponAngle;

    private void Awake()
    {
        weaponOnShip = GetComponent<Transform>();
    }


    void Update()
    {
        weaponAngle += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        weaponAngle = Mathf.Clamp(weaponAngle, -yrotationClamp, yrotationClamp);
        weaponOnShip.localRotation = Quaternion.AngleAxis(weaponAngle, Vector3.up);

    }




}
