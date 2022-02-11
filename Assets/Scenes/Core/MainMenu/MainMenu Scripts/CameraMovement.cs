using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Cinemachine;
using Audio;

public class CameraMovement : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineBrain mainCamera;
    public float cinemachineBrainBlendtime;
    public List<CameraMove> cameraMove;
    public List<MoveToTargetCam> moveToTargetCam;

    public AudioMixerGroup effectVolumeGroup;

    private void Start()
    {
        //mainCamera = GetComponent<CinemachineBrain>();
        cinemachineBrainBlendtime = mainCamera.m_DefaultBlend.m_Time;
    }
    public void MoveCamera(int _cameraMoveElementNumber = 0)
    {
        if(_cameraMoveElementNumber >= cameraMove.Count)
        {
            _cameraMoveElementNumber = 0;
        }
        StartCoroutine(IMoveCamera( _cameraMoveElementNumber));
    }
    private IEnumerator IMoveCamera(int _cameraMoveElementNumber = 0)
    {
        if(cameraMove[_cameraMoveElementNumber].startpointCam.gameObject.activeSelf == true) //Dadurch kann in beiden Richtungen geswitcht werden
        {
            cameraMove[_cameraMoveElementNumber].startpointCam.gameObject.SetActive(false);
            foreach (GameObject dObject in cameraMove[_cameraMoveElementNumber].disableObjects)
            {
                dObject.SetActive(false);
            }

            AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);
            
            cameraMove[_cameraMoveElementNumber].endpointCam.gameObject.SetActive(true);

            yield return new WaitForSeconds(cinemachineBrainBlendtime + cameraMove[_cameraMoveElementNumber].additionalDelay);

            foreach (GameObject aObject in cameraMove[_cameraMoveElementNumber].enableObjects)
            {
                aObject.SetActive(true);
            }
        }
        else
        {
            cameraMove[_cameraMoveElementNumber].endpointCam.gameObject.SetActive(false);
            foreach (GameObject aObject in cameraMove[_cameraMoveElementNumber].enableObjects)
            {
                aObject.SetActive(false);
            }

            AudioManager.PlaySound(AudioManager.Sound.MMWhoosh, .4f);
            cameraMove[_cameraMoveElementNumber].startpointCam.gameObject.SetActive(true);

            yield return new WaitForSeconds(cinemachineBrainBlendtime + cameraMove[_cameraMoveElementNumber].additionalDelay);

            foreach (GameObject dObject in cameraMove[_cameraMoveElementNumber].disableObjects)
            {
                dObject.SetActive(true);
            }
        }
    }
    public void MoveToTargetCamera(int _targetCamElementNumber)
    {
        if (_targetCamElementNumber >= moveToTargetCam.Count)
        {
            _targetCamElementNumber = 0;
        }
        StartCoroutine(IMoveToTargetCamera(_targetCamElementNumber));
    }
    private IEnumerator IMoveToTargetCamera(int _targetCamElementNumber = 0)
    {
        foreach(CinemachineVirtualCamera cmvcCam in moveToTargetCam[_targetCamElementNumber].disableOtherCams)
        {
            cmvcCam.gameObject.SetActive(false);
        }
        foreach (GameObject dObject in moveToTargetCam[_targetCamElementNumber].disableObjects)
        {
            dObject.SetActive(false);
        }
        moveToTargetCam[_targetCamElementNumber].targetCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cinemachineBrainBlendtime + moveToTargetCam[_targetCamElementNumber].additionalDelay);

        foreach (GameObject aObject in moveToTargetCam[_targetCamElementNumber].enableObjects)
        {
            aObject.SetActive(true);
        }
    }
 
}

[System.Serializable]
public class CameraMove
{
    [Header("Camera Move To Location", order = 0)]
    [Space(-10,order = 1)]
    [Header("Camera Switch Possible Without An Extra Element",order = 2)]
    //EditorGUILayout.La
    public CinemachineVirtualCamera startpointCam;
    public CinemachineVirtualCamera endpointCam;

    [Header("Camera Timer")]
    public float additionalDelay = 0.25f;

    [Header("En-/Disable Objects")]
    public GameObject[] disableObjects;
    public GameObject[] enableObjects;
}
[System.Serializable]
public class MoveToTargetCam
{
    [Header("Camera Move To Location")]
    public CinemachineVirtualCamera targetCam;
    public CinemachineVirtualCamera[] disableOtherCams;

    [Header("Camera Timer")]
    public float additionalDelay = 0.25f;

    [Header("En-/Disable Objects")]
    public GameObject[] disableObjects;
    public GameObject[] enableObjects;
}
