using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishLine;

    private CheckpointManager checkpointManager;
    private FinishLineManager finishLineManager;

    public GameObject audioManager;

    private PlayerManager playerManager;
    public CinemachineVirtualCamera vCam;

    public GameObject gameOverCanvas;

    public float vCamPOV = 70f;

    public bool raceHasStarted;
    public bool raceFinished;

    [Header("Laps")]
    public int laps = 3;
    [Header("Positions")]
    public float[] positions = new float[5];
    [Header("Round Timer")]
    public float roundTimer;
    public string roundTimerAsSecString;
    public string roundTimerAsDeciString;
    public float timeSinceStart;

    private float currentTimer;
    private float currentMin;
    private float currentSec;
    private float currentMilliSec;

    public bool ghostMode;
    
    private void Awake()
    {
        Time.timeScale = 1;
        
        raceHasStarted = false;
        raceFinished = false;
        
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
        vCam = FindObjectOfType<CinemachineVirtualCamera>();

        vCamPOV = vCam.m_Lens.FieldOfView;
    }

    private void Start()
    {
        audioManager.transform.GetChild(0).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(1).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(2).GetComponent<VolumeSlider>().GetAudiosAtStart();
    }

    private void Update()
    {
        if (raceHasStarted)
        {
            roundTimer = Time.time - timeSinceStart;
            ConvertTime();
        }
    }
    private void ConvertTime()
    {
        string currentMinAsString;
        string currentSecAsString;
        string currentMiliAsString;

        currentTimer += Time.deltaTime;

        currentMilliSec = Mathf.RoundToInt(currentTimer * 100);
        if (currentSec >= 60f)
        {
            currentSec = 0;
            currentMin++;
        }
        if(currentMilliSec >= 100f)
        {
            currentSec++;
            currentTimer = 0;
        }

        if (currentMin < 10)
            currentMinAsString = "0" + currentMin;
        else
            currentMinAsString = "" + currentMin;

        if (currentSec < 10)
            currentSecAsString = "0" + currentSec;
        else
            currentSecAsString = "" + currentSec;

        if (currentMilliSec < 10)
            currentMiliAsString = "0" + currentMilliSec;
        else
            currentMiliAsString = "" + currentMilliSec;

        //roundTimerAsString = currentMinAsString + ":" + currentSecAsString + ":" + currentMiliAsString;
        roundTimerAsSecString = currentMinAsString + ":" + currentSecAsString + ":";
        roundTimerAsDeciString = currentMiliAsString;
    }

    public void StartRoundTimer()
    {
        timeSinceStart = Time.time;
        raceHasStarted = true;
    }

    //public void CheckLaps()
    //{
    //    if (currentLap > laps && !ghostMode)
    //    {
    //        currentLap = laps;
//
    //        raceFinished = true;
    //        //if (FindObjectOfType<GhostManager>())
    //        //{
    //        //    FindObjectOfType<GhostManager>().StopRecording();
    //        //}
    //        //Game Finish
//
    //        gameOverCanvas.gameObject.SetActive(true);
//
    //        Debug.Log("YAY FINISH");
    //        for (int i = 0; i < finishLineManager.checkpointParent.childCount; i++)
    //        {
    //            Checkpoint checkpoint = finishLineManager.checkpointParent.GetChild(i).GetComponent<Checkpoint>();
    //            if (checkpoint.transform.GetChild(0))
    //            {
    //                checkpoint.transform.GetChild(0).gameObject.SetActive(false);
    //            }
    //        }
    //    }
//
    //    if (currentLap < 0)
    //    {
    //        currentLap = 0;
    //    }
//
    //    playerManager.currentLapIndex = currentLap;
    //}
//

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }
    public void ReplayLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
