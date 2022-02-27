using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;

public class AIFollowCurve : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private EndOfPathInstruction end;
    [SerializeField] private PathCreator pathCreator;
    private float _distanceTravelled;
    

    private void FixedUpdate()
    {
        TravelAlongCurve();
    }

    private void TravelAlongCurve()
    {
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, end);
        
        _distanceTravelled += speed * Time.fixedDeltaTime;
    }
}
