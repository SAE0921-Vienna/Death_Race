using System;
using UnityEngine;
using Cinemachine;
using Core;
using UserInterface;

public class GameManager : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private FinishLineManager finishLineManager;

    public GameObject audioManager;

    [SerializeField]
    private PlayerManager playerManager;
    public CinemachineVirtualCamera vCam;
    public Camera overlayCam;
    public event Action StartOfRace;

    public GameObject gameOverCanvas;
    public GameObject pauseCanvas;

    public float vCamPOV = 90f;

    public bool raceHasStarted;
    public bool raceFinished;

    [Header("Laps")]
    public int laps = 3;
    [Header("Positions")]
    public float[] positions = new float[5];
    [Header("Round Timer")]
    public string roundTimerAsString;

    public float roundTimer;
    public float currentMin = 0;
    public float currentSec = 0;
    public float currentMilliSec = 0;
    public string currentMinAsString;
    public string currentSecAsString;
    public string currentMiliAsString;

    public bool ghostMode;
    public bool withEffects;
    public bool reverseTrack;
    private Timer _timer;
    
    [Header("Start Of Race Timer")]
    public float startOfRaceTimer;

    private void Awake()
    {
        Time.timeScale = 1;
        roundTimerAsString = "00:00:00";

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
        overlayCam.fieldOfView = vCamPOV;
    }

    private void Start()
    {
        audioManager.transform.GetChild(0).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(1).GetComponent<VolumeSlider>().GetAudiosAtStart();
        audioManager.transform.GetChild(2).GetComponent<VolumeSlider>().GetAudiosAtStart();
        
        _timer = FindObjectOfType<Timer>();
        StartOfRaceTimer();
    }
    
    private void StartOfRaceTimer()
    {
        _timer.CreateTimer(startOfRaceTimer, () => { StartOfRace?.Invoke(); });
    }

    private void Update()
    {
        if (raceHasStarted)
        {
            ConvertTime();

        }
    }
    private void ConvertTime()
    {
        roundTimer += Time.deltaTime;

        currentMin = Mathf.Floor(roundTimer * 0.0166666666f);
        currentSec = Mathf.Floor(roundTimer - (currentMin * 60));
        currentMilliSec = (float)System.Math.Round(roundTimer - (currentSec + currentMin * 60), 2) * 100;
        currentMilliSec = Mathf.Floor(currentMilliSec);

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

        roundTimerAsString = currentMinAsString + ":" + currentSecAsString + ":" + currentMiliAsString;

        //roundTimer = (currentSec + currentMilliSec*0.01f + currentMin * 60);
    }

    public void StartRoundTimer()
    {
        raceHasStarted = true;
    }
    

    public void GameOver()
    {
        raceFinished = true;
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
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
