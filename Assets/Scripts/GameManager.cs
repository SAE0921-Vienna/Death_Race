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
    public string roundTimerAsString;

    public float roundTimer;
    public float currentMin;
    public float currentSec;
    public float currentMilliSec;

    public bool ghostMode;

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
            ConvertTime();

        }
    }
    private void ConvertTime()
    {
        string currentMinAsString;
        string currentSecAsString;
        string currentMiliAsString;

        roundTimer += Time.deltaTime;

        currentMilliSec = Mathf.RoundToInt(roundTimer * 100);
        if (currentSec >= 60f)
        {
            currentSec = 0;
            currentMin++;
        }
        if (currentMilliSec >= 100f)
        {
            currentSec++;
            roundTimer = 0;
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

        roundTimerAsString = currentMinAsString + ":" + currentSecAsString + ":" + currentMiliAsString;
    }

    public void StartRoundTimer()
    {
        raceHasStarted = true;
    }

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
