using AI;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    private CheckpointManager checkpointManager;
    private AICheckpointManager aicheckpointManager;
    private AICheckpointManagerMachine mlCheckpointManager;


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
        //aicheckpointManager = FindObjectOfType<AICheckpointManager>();
        mlCheckpointManager = FindObjectOfType<AICheckpointManagerMachine>();
        if (mlCheckpointManager)
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
            checkpointManager.VehicleThroughCheckpoint(this, other);

        }
        if (other.CompareTag("AI") )
        {
            //Debug.Log("Checkpoint");
            aicheckpointManager.VehicleThroughCheckpoint(this, other);
            //mlCheckpointManager.AIThroughCheckpoint(this, other);
        }

    }




}
