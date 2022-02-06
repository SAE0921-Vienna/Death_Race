using AI;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    private CheckpointManager checkpointManager;
    private AICheckpointManager aicheckpointManager;


    private void Awake()
    {
        #region GameManager FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
        {
            //Gamemanager Found
        }
        else
        {
            Debug.LogWarning("GameManager NOT Found");
        }
        #endregion

        checkpointManager = GetComponentInParent<CheckpointManager>();

        #region aicheckpointManager FindObjectOfType
        aicheckpointManager = FindObjectOfType<AICheckpointManager>();
        if (aicheckpointManager)
        {
            //AIManager Found
        }
        else
        {
            Debug.LogWarning("AIManager NOT Found");

        }
        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Checkpoint");
            checkpointManager.PlayerThroughCheckpoint(this);
            if (gameManager.nextCheckpoint == 0 && gameManager.currentLap >= gameManager.laps)
            {
                FindObjectOfType<FinishLineManager>().transform.GetChild(0).gameObject.SetActive(true);
            }

        }
        if (other.CompareTag("AI") )
        {
            //Debug.Log("Checkpoint");
            aicheckpointManager.AIThroughCheckpoint(this);

        }

    }




}
