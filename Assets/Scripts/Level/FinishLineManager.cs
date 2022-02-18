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
        transform.GetChild(0).gameObject.SetActive(true);
        _aiCheckpointManager = GetComponent<AICheckpointManagerMachine>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        //Makes the glowing effect disappear.
        if (transform.GetChild(0) != null)
            transform.GetChild(0).gameObject.SetActive(false);
        
        if (other.CompareTag("Player") && other.GetComponent<PlayerManager>().currentLapIndex == 0 && other.GetComponent<PlayerManager>().currentCheckpointIndex == 0)
        {
            var playerManager = other.GetComponent<PlayerManager>();
            
            playerManager.currentLapIndex += 1;
            if (gameManager.ghostMode)
            {
                checkpointParent.GetComponent<CheckpointManager>().SetFirstCheckpointMAT();
                transform.GetChild(0).gameObject.SetActive(false);
            }
            gameManager.StartRoundTimer();
            playerManager.CheckLapCount();
            minimap.gameObject.SetActive(true);
        }

        if (other.CompareTag("Player") && other.GetComponent<PlayerManager>().currentCheckpointIndex == _checkpointManager.checkpointCount - 1)
        {
            var playerManager = other.GetComponent<PlayerManager>();
            
            playerManager.currentLapIndex += 1;
            if (gameManager.ghostMode)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                checkpointParent.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }           
            playerManager.CheckLapCount();

            if (gameManager.ghostMode)
            {
                FindObjectOfType<GhostManager>().StopRecording();
                FindObjectOfType<GhostManager>().ghost.playGhostRecording();
                //gameManager.roundTimer = 0; Schaut sich Wolfi oder Tomi an.
            }
        }
        
        if (other.CompareTag("AI") && other.GetComponent<AIManager>().currentLapIndex == 0 && other.GetComponent<AIManager>().currentCheckpointIndex == 0)
        {
            var aIManager = other.GetComponent<AIManager>();
            
            aIManager.currentLapIndex += 1;
            aIManager.CheckLaps();
            
            //_aiCheckpointManager.ResetCheckpoints();
        }

        if (other.CompareTag("AI") && other.GetComponent<AIManager>().currentCheckpointIndex == other.GetComponent<AIManager>().currentLapIndex - 1)
        {
            var aIManager = other.GetComponent<AIManager>();
            
            aIManager.currentLapIndex += 1;
            aIManager.CheckLaps();

            //_aiCheckpointManager.ResetCheckpoints();
        }

    }




}
