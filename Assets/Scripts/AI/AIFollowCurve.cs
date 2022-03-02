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
        //transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.position = AIPosition(pathCreator.path.GetPointAtDistance(_distanceTravelled, end));
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, end);
        
        _distanceTravelled += speed * Time.fixedDeltaTime;
    }

    Vector3 AIPosition(Vector3 curveLocation)
    {
        return pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
    }

    private void Accelerate()
    {
        speed = Mathf.MoveTowards(speed, maxSpeed, accelerationConstant);
    }
}
