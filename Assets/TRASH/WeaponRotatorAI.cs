using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotatorAI : WeaponRotator
{
    public FieldOfView fieldOfView;
    public Vector3 weaponToTarget;
    public float changePositionTimer;
    public bool checkTimer;

    private void Awake()
    {
        fieldOfView = GetComponentInParent<FieldOfView>();
        weaponToTarget = new Vector3(0f, 0f, 0f);
        weaponOnShip = GetComponent<Transform>();
        checkTimer = false;
        changePositionTimer = 5f;
    }
    private void Update()
    {
        RotateWeapon();

        if (checkTimer)
            Timer();
    }
    protected override void RotateWeapon()
    {
        if (fieldOfView.nearestObject != null)
        {

            weaponToTarget.x = fieldOfView.nearestObject.transform.position.x - weaponOnShip.transform.position.x;
            weaponToTarget.y = fieldOfView.nearestObject.transform.position.y - weaponOnShip.transform.position.y;
            weaponToTarget.z = fieldOfView.nearestObject.transform.position.z - weaponOnShip.transform.position.z;

            _targetRotation = Quaternion.LookRotation(weaponToTarget);
            currentRotation = weaponOnShip.transform.rotation;

            var angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

            weaponOnShip.transform.rotation = angularDifference > 0
                ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
                : _targetRotation;

            changePositionTimer = 5f;
        }
        else
        {
            if (!checkTimer)
                checkTimer = true;

            if (changePositionTimer < 0 && checkTimer)
            {
                weaponOnShip.transform.localRotation = Quaternion.identity;

                checkTimer = false;
                changePositionTimer = 5f;
            }
        }

    }
    private void Timer()
    {
        changePositionTimer -= Time.deltaTime;
    }
}
