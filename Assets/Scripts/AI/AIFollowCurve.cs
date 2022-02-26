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
    [SerializeField] private LayerMask layerMask;

    private float _distanceTravelled;
    private CheckpointManager _checkpointManager;
    [SerializeField] private PathCreator pathCreator;

    private void Start()
    {
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        InstantiatePath();
    }

    private void InstantiatePath()
    {
        var path = new GameObject("AI-Path");
        pathCreator = path.AddComponent<PathCreator>();
        pathCreator.bezierPath = GeneratePath(GetCheckpointPositions(), true);
        
        SetNormals();
    }

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

    private BezierPath GeneratePath(List<Vector3> points, bool closedPath)
    {
        var bezierPath = new BezierPath(points, closedPath);
        return bezierPath;
    }

    private void SetNormals()
    {
        var allNormals = GetTrackNormals();
        for (var anchorIndex = 0; anchorIndex < pathCreator.path.localPoints.Length; anchorIndex++)
        {
            var normal = allNormals[anchorIndex];

            var signedAngle = Vector3.SignedAngle(Vector3.right, normal, pathCreator.path.GetTangent(anchorIndex));
            pathCreator.bezierPath.SetAnchorNormalAngle(anchorIndex, signedAngle);
        }
    }

    private List<Vector3> GetTrackNormals()
    {
        var allNormals = new List<Vector3>();
        foreach (var position in GetCheckpointPositions())
        {
            var allColliders = Physics.OverlapSphere(position, 100f, layerMask, QueryTriggerInteraction.Ignore);
            var collidersByDistance = allColliders.OrderBy(col => col.ClosestPoint(position)).ToList();

            foreach (var col in collidersByDistance)
            {
                print(col.name);
            }

            var closestPoint = collidersByDistance[0].ClosestPoint(position);
            var ray = new Ray(position, (position + closestPoint).normalized);

            Physics.Raycast(ray, out RaycastHit hit,100f, LayerMask.NameToLayer("Roadtrack"));
            allNormals.Add(hit.normal);
        }
        return allNormals;
    }

    private List<Vector3> GetCheckpointPositions()
    {
        var vectorList = new List<Vector3>();
        foreach (var checkpoint in _checkpointManager.checkpointsInWorldList)
        {
            vectorList.Add(checkpoint.transform.position);
        }
        return vectorList;
    }
}
