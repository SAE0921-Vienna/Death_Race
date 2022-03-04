using UnityEngine;
using Cinemachine;

public class GarageController : MonoBehaviour
{
    [Header("Camera Settings")]
    public GameObject targetObject;
    private float roationspeed = 1000f;
    private float zoomSpeed = 10f;
    CinemachineVirtualCamera virutalCamera;
    private float cameraDistanceMin = 5f;
    private float cameraDistanceMax = 13f;
    
    [Header("Ship & Weapon Stats")]
    [SerializeField] private bool showsStats = true;
    [SerializeField] private GameObject ShowShipStats;
    [SerializeField] private GameObject ShowWeaponStats;

    private void Start()
    {
        virutalCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        CameraRoationLeftRight();
        CameraZoom();
    }

    /// <summary>
    /// A/D to move around the ship with the camera in the garage
    /// </summary>
    private void CameraRoationLeftRight()
    {
        if (Input.GetButton("Horizontal"))
        {
            targetObject.transform.Rotate(0, Input.GetAxis("Horizontal") * -roationspeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// W/S to zoom in and out with the camera in the garage to the ship
    /// </summary>
    private void CameraZoom()
    {
        if (Input.GetButton("Vertical"))
        {
            virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= Input.GetAxisRaw("Vertical") * zoomSpeed * Time.deltaTime;
            virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Clamp(virutalCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance, cameraDistanceMin, cameraDistanceMax);
        }
    }

    /// <summary>
    /// Enables/disables stats in garage
    /// </summary>
    public void ToggleStats()
    {
        showsStats = !showsStats;
        ShowShipStats.SetActive(showsStats);
        ShowWeaponStats.SetActive(showsStats);
    }
}

