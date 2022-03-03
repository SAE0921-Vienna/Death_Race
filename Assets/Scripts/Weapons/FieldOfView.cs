using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    //Script überarbeitet -> Base-Script Link: https://www.youtube.com/watch?v=rQG9aUWarwE

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //[HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    public Transform nearestObject;

    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.5f));
    }

    /// <summary>
    /// Checks every 0.5 seconds if there is an enemy ship in the field of view radius
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    /// <summary>
    /// Look for the visible opponents and target the closest one.
    /// </summary>
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && dstToTarget > GetComponentInChildren<BoxCollider>().size.x)
                {
                    visibleTargets.Add(target);
                }
            }
        }
        nearestObject = null;
        if (visibleTargets.Count != 0)
        {
            foreach (Transform vT in visibleTargets)
            {
                if (nearestObject == null)
                    nearestObject = vT;
                if (nearestObject != vT)
                {
                    float currentShortestDistance = Vector3.Distance(transform.position, nearestObject.position);
                    float distance = Vector3.Distance(transform.position, vT.position);
                    if (distance < currentShortestDistance || visibleTargets.Count == 0)
                    {
                        nearestObject = vT;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns the range in radians.
    /// </summary>
    /// <param name="angleInDegrees"></param>
    /// <param name="angleIsGlobal"></param>
    /// <returns></returns>
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}