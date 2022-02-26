using AI;
using UserInterface;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private GameManager gameManager;

    public Transform minimap;

    public UIManager uiManager;

    public CheckpointManager _checkpointManager;

    public GhostManager ghostManager;


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

        ghostManager = GetComponent<GhostManager>();
        if (ghostManager == null)
        {
            Debug.Log("No GhostManager");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            var vehicleManager = other.GetComponentInParent<BaseVehicleManager>();

            if (vehicleManager.currentLapIndex == 0 && vehicleManager.currentCheckpointIndex == 0)
            {

                vehicleManager.currentLapIndex += 1;

                if (other.CompareTag("Player"))
                {
                    gameManager.StartRoundTimer();
                    minimap.gameObject.SetActive(true);
                }
            }

            if (vehicleManager.currentCheckpointIndex == _checkpointManager.checkpointCount - 1)
            {
                vehicleManager.currentLapIndex += 1;

                if (gameManager.ghostMode && !ghostManager.ghost.hasData)
                {
                    ghostManager.AddMoney();
                    ghostManager.SaveRecordingFirstTime();
                    ghostManager.ghost.isRecording = true;
                }
                else if (gameManager.ghostMode && ghostManager.ghost.hasData)
                {
                    ghostManager.AddMoney();
                    ghostManager.StopRecording();
                    ghostManager.StartRecording();
                }

                //If the vehicle is on current lap of index currentLapIndex 4 and collides with the Trigger, set the lap count back to 3.
                if (vehicleManager.currentLapIndex > gameManager.laps && vehicleManager.gameObject.CompareTag("Player") && !gameManager.ghostMode)
                {
                    vehicleManager.currentLapIndex = gameManager.laps;
                    gameManager.GameOver();
                }
                Debug.LogFormat("Started a new Lap! {0}", vehicleManager.currentLapIndex);
            }

        }


    }





}
