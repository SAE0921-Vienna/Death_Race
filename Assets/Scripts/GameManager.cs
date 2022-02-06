using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishLine;

    private CheckpointManager checkpointManager;
    private FinishLineManager finishLineManager;

    private PlayerManager playerManager;
    public CinemachineVirtualCamera vCam;
    public float vCamPOV = 70f;



    [Header("Laps")]
    public int laps = 3;
    public int currentLap;
    [Header("Positions")]
    public float[] positions = new float[5];
    public int playerPosition;
    [Header("Checkpoints")]
    public int checkpoints;
    public int currentCheckpoint;
    public int nextCheckpoint;
    [Header("Round Timer")]
    public float roundTimer;
    [Header("Off Track Timer")]
    public float offTrackTimer;
    public float offTrackTimerLimit = 5f;

    public Vector3 spawnPlayerPosition;
    public Quaternion spawnPlayerRotation;
    public float spawnPlayerYOffset = 5f;

    private void Awake()
    {

        checkpointManager = FindObjectOfType<CheckpointManager>();
        if (!checkpointManager)
        {
            Debug.LogWarning("Checkpoint Manager was NOT found");
        }
        else
        {
            checkpoints = checkpointManager.checkpoints;
        }
        finishLineManager = FindObjectOfType<FinishLineManager>();
        if (!finishLineManager)
        {
            Debug.LogWarning("FinishLine Manager was NOT found");
        }
        playerManager = FindObjectOfType<PlayerManager>();
        if (!playerManager)
        {
            Debug.LogWarning("Player Manager was NOT found");
        }
        else
        {
            spawnPlayerPosition = new Vector3(playerManager.transform.position.x, playerManager.transform.position.y + spawnPlayerYOffset, playerManager.transform.position.z);
            spawnPlayerRotation = new Quaternion(playerManager.transform.rotation.x, playerManager.transform.rotation.y, playerManager.transform.rotation.z, playerManager.transform.rotation.w);
        }



        vCam = FindObjectOfType<CinemachineVirtualCamera>();

       vCamPOV = vCam.m_Lens.FieldOfView;
    }

    private void Update()
    {
        roundTimer = Time.time;
        roundTimer = (float)System.Math.Round(roundTimer, 0);


        CheckIfOnTrack();
 


    }


    public void CheckLaps()
    {
        if (currentLap > laps)
        {
            currentLap = laps;
            //Game Finish
            Debug.Log("YAY FINISH");
        }
        if (currentLap < 0)
        {
            currentLap = 0;
        }

    }

    /// <summary>
    /// Checks the currentcheckpoint and sets it
    /// </summary>
    public void CheckCurrentCheckpoint()
    {
        currentCheckpoint = checkpointManager.currentCheckpoint;
        nextCheckpoint = checkpointManager.nextCheckpointIndex;


    }

    public void CheckPosition()
    {
        //Check Position
    }

    public void CheckIfOnTrack()
    {
        if (playerManager != null)
        {
            if (!playerManager.isOnRoadtrack)
            {
                offTrackTimer += Time.deltaTime;
            }
            else
            {
                offTrackTimer = 0;
            }

            if (offTrackTimer > offTrackTimerLimit)
            {
                playerManager.gameObject.SetActive(false);
                RespawnPlayer();
            }
        }
    }

    public void RespawnPlayer()
    {
        offTrackTimer = 0;

        spawnPlayerPosition = new Vector3(spawnPlayerPosition.x, spawnPlayerPosition.y + spawnPlayerYOffset, spawnPlayerPosition.z);
        playerManager.vehicleController.currentSpeed = 0;
        playerManager.vehicleController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerManager.transform.position = spawnPlayerPosition;
        playerManager.transform.rotation = spawnPlayerRotation;
        playerManager.gameObject.SetActive(true);
    }
}
