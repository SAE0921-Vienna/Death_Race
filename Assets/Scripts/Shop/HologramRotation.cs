using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramRotation : MonoBehaviour
{
    #region Rotate and Hover Points
    Vector3 pointA;
    Vector3 pointB;
    Vector3 rotation;
    #endregion
    [SerializeField]
    [Range(0, 10)]
    private float hoverSpeed = 0f;
    [SerializeField]
    [Range(0, 180)]
    private float rotationSpeed = 0.5f;


    private void Start()
    {
        #region Defining Points
        pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pointB = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rotation = new Vector3(0, Time.deltaTime, 0);
        #endregion
    }

    private void Update()
    {
        //Hovering Effect
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * hoverSpeed, 1));
        //Rotation Effect
        transform.Rotate(rotation, rotationSpeed);
    }




}
