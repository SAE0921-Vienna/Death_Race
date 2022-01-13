using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GarageCameraController : MonoBehaviour
{
    public GameObject targetObject;
    public float roationspeed;
    public float zoomSpeed;
    CinemachineVirtualCamera virutalCamera;
    public float cameraDistanceMin = 5f;
    public float cameraDistanceMax = 13f;

    private void Start()
    {
        virutalCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        CameraRoationLeftRight();
        CameraZoom();
    }
    private void CameraRoationLeftRight()
    {
        if (Input.GetButton("Horizontal"))
        {
            targetObject.transform.Rotate(0, Input.GetAxis("Horizontal") * roationspeed * Time.deltaTime, 0);
        }
    }
    private void CameraZoom()
    {
        if (Input.GetButton("Vertical"))
        {
            virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= Input.GetAxisRaw("Vertical") * zoomSpeed * Time.deltaTime;
            virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Clamp(virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance, cameraDistanceMin, cameraDistanceMax);
        }
    }
}

