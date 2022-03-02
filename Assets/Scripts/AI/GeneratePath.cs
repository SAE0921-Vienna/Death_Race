using System;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace AI
{
    [RequireComponent(typeof(PathCreator))]
    public class GeneratePath : MonoBehaviour
    {
        [SerializeField]
        private CheckpointManager _checkpointManager;
        private PathCreator _pathCreator;
        
        private void Start()
        {
            _checkpointManager = FindObjectOfType<CheckpointManager>();
            _pathCreator = GetComponent<PathCreator>();
            
            InstantiatePath();
            SetNormals();
        }
        
        private void InstantiatePath()
        {
            _pathCreator.bezierPath = GenerateNewPath(GetCheckpointTransforms(), true);
        }
        private BezierPath GenerateNewPath(List<Transform> checkpoints, bool closedPath)
        {
            if (checkpoints.Count == 0) return null;
            
            var positionsList = new List<Vector3>();
            foreach (var checkpointTransform in checkpoints)
            {
                positionsList.Add(checkpointTransform.position);
            }

            var bezierPath = new BezierPath(positionsList, closedPath);
            return bezierPath;
        }
        
        private List<Transform> GetCheckpointTransforms()
        {
            var transformList = new List<Transform>();
            foreach (var checkpoint in _checkpointManager.checkpointsInWorldList)
            {
                transformList.Add(checkpoint.transform);
            }
            return transformList;
        }

        private void SetNormals()
        {
            List<Transform> checkpointTransforms = GetCheckpointTransforms();
            for (int i = 0; i < GetCheckpointTransforms().Count; i++)
            {
                _pathCreator.bezierPath.SetAnchorNormalAngle(i, GetAngleBetweenVectors(checkpointTransforms[i], i));
            }
        }
        
        private float GetAngleBetweenVectors(Transform checkpointTransform, int checkpointIndex)
        {
            var pathNormal = _pathCreator.path.GetNormal(checkpointIndex).normalized;
            var checkpointUp = checkpointTransform.up.normalized;
            var curveTangent = -checkpointTransform.forward.normalized;

            var angle = Vector3.SignedAngle(pathNormal, checkpointUp, curveTangent);
            Debug.LogFormat("Path Normal: {0}, Checkpoint Up: {1}, Curve Tangent: {2}, Signed Angle: {3}", pathNormal, checkpointUp, curveTangent, angle);

            return angle;
        }
    }
}