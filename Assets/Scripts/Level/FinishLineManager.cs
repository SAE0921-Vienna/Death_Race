using AI;
using UserInterface;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private GameManager gameManager;

    public Transform checkpointParent;

    public Transform minimap;

    public UIManager uiManager;
    private AICheckpointManagerMachine _aiCheckpointManager;
    private CheckpointManager _checkpointManager;
    
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
        {
            //GameManager Found
        }
        else
        {
            Debug.LogWarning("GameManager NOT Found");
        }

        _checkpointManager = FindObjectOfType<CheckpointManager>();
        //transform.GetChild(0).gameObject.SetActive(true);
        _aiCheckpointManager = GetComponent<AICheckpointManagerMachine>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BaseVehicleManager>().currentLapIndex == 0 && other.GetComponentInParent<BaseVehicleManager>().currentCheckpointIndex == 0)
        {
            var vehicleManager = other.GetComponentInParent<BaseVehicleManager>();
            
            vehicleManager.currentLapIndex += 1;

            if (other.CompareTag("Player"))
            {
                gameManager.StartRoundTimer();
                minimap.gameObject.SetActive(true);
            }
        }

        if (other.GetComponentInParent<BaseVehicleManager>().currentCheckpointIndex == _checkpointManager.checkpointCount - 1)
        {
            var vehicleManager = other.GetComponentInParent<BaseVehicleManager>();
            
            vehicleManager.currentLapIndex += 1;

            if (gameManager.ghostMode)
            {
                FindObjectOfType<GhostManager>().StopRecording();
                FindObjectOfType<GhostManager>().ghost.playGhostRecording();
                gameManager.roundTimer = 0;
                gameManager.currentMilliSec = 0;
                gameManager.currentMin = 0;
                gameManager.currentSec = 0;
            }

            //If the vehicle is on current lap of index currentLapIndex 4 and collides with the Trigger, set the lap count back to 3.
            if (vehicleManager.currentLapIndex > gameManager.laps && vehicleManager.gameObject.CompareTag("Player"))
            {
                vehicleManager.currentLapIndex = gameManager.laps;
                gameManager.GameOver();
            }
            Debug.LogFormat("Started a new Lap! {0}", vehicleManager.currentLapIndex);
        }
    }




}
