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
    private void CameraRoationLeftRight()
    {
        if (Input.GetButton("Horizontal"))
        {
            targetObject.transform.Rotate(0, Input.GetAxis("Horizontal") * -roationspeed * Time.deltaTime, 0);
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
    public void ToggleStats()
    {
        showsStats = !showsStats;
        ShowShipStats.SetActive(showsStats);
        ShowWeaponStats.SetActive(showsStats);
    }
}

