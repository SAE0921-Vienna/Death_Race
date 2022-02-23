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
    }
    private void OnTriggerEnter(Collider other)
    {
        checkpointManager = GetComponentInParent<CheckpointManager>();

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Checkpoint");
            checkpointManager.VehicleThroughCheckpoint(this, other);

        }
        if (other.CompareTag("AI") )
        {
            var aiCheckpointManager = other.GetComponent<AICheckpointManager>();
            Debug.Log("AI through Checkpoint");
            aicheckpointManager.VehicleThroughCheckpoint(this, other);
        }

    }




}
