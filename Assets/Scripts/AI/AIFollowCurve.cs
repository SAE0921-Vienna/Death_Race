using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;

public class AIFollowCurve : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private EndOfPathInstruction end;
    [SerializeField] private PathCreator pathCreator;
    private float _distanceTravelled;
    [SerializeField] private float accelerationConstant;

    [HideInInspector] public float currentSpeed;


    private void FixedUpdate()
    {
        TravelAlongCurve();
        Accelerate();
    }

    private void TravelAlongCurve()
    {
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, end);
        
        _distanceTravelled += speed * Time.fixedDeltaTime;
    }

    private void Accelerate()
    {
        currentSpeed = Mathf.Clamp01(speed += 0.01f * accelerationConstant * Time.fixedDeltaTime);
        speed = Mathf.Lerp(0f, maxSpeed, currentSpeed);
    }
}
