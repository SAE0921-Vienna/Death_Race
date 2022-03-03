using Cinemachine;
using UnityEngine;

public class CameraLookBackwards : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPosition;
    [SerializeField]
    private GameObject _cameraPositionBack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            print("Button Pressed");
            _cameraPosition.SetActive(false);
            _cameraPositionBack.SetActive(true);
        }
        else
        {
            _cameraPosition.SetActive(true);
            _cameraPositionBack.SetActive(false);
        }
    }
}
