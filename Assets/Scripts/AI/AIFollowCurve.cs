using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;

public class AIFollowCurve : MonoBehaviour
{
    public float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private EndOfPathInstruction end;
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float accelerationConstant;
    private float _distanceTravelled;
    private GameManager _gameManager;
    private bool _canDrive;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.StartOfRace += () => { _canDrive = true; };
    }


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
        if (!_canDrive) return;
        speed = Mathf.MoveTowards(speed, maxSpeed, accelerationConstant);
    }

    public float GetCurrentSpeed()
    {
        return Mathf.InverseLerp(0f, maxSpeed, speed);
    }
}
