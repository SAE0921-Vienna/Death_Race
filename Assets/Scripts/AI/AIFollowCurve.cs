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
    [SerializeField] private float accelerationConstant;
    private float _distanceTravelled;


    private void FixedUpdate()
    {
        TravelAlongCurve();
        Accelerate();
    }

    private void TravelAlongCurve()
    {
        //transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, end);
        
        _distanceTravelled += speed * Time.fixedDeltaTime;
    }
    

    private void Accelerate()
    {
        speed = Mathf.MoveTowards(speed, maxSpeed, accelerationConstant);
    }

    public float GetCurrentSpeed()
    {
        return Mathf.InverseLerp(0f, maxSpeed, speed);
    }
}
