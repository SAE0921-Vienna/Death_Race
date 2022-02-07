using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotatorAI : WeaponRotator
{
    private FieldOfView fieldOfView;
    Vector3 weaponToTarget;

    private void Awake()
    {
        fieldOfView = GetComponentInParent<FieldOfView>();
    }
    private void Update()
    {
        RotateWeapon();
    }
    protected override void RotateWeapon()
    {
        //base.RotateWeapon();
        if(fieldOfView.nearestObject != null)
        {
            weaponToTarget = new Vector3(0f, 0f, 0f);
            weaponToTarget.x = fieldOfView.nearestObject.transform.position.x - weaponOnShip.transform.position.x;
            weaponToTarget.y = fieldOfView.nearestObject.transform.position.y - weaponOnShip.transform.position.y;
            weaponToTarget.z = fieldOfView.nearestObject.transform.position.z - weaponOnShip.transform.position.z;

        _targetRotation = Quaternion.LookRotation(weaponToTarget);
        currentRotation = weaponOnShip.transform.rotation;

        var angularDifference = Quaternion.Angle(currentRotation, _targetRotation);

        weaponOnShip.transform.rotation = angularDifference > 0
            ? Quaternion.Slerp(currentRotation, _targetRotation, (rotationSpeed * 180 * Time.deltaTime) / angularDifference)
            : _targetRotation;
        }

    }
}
