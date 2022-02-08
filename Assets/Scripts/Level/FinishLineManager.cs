using AI;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private GameManager gameManager;
    private AIManager aIManager;

    public Transform checkpointParent;

    public Transform minimap;

    private void Awake()
    {
        #region gameManager FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
        {
            //GameManager Found
        }
        else
        {
            Debug.LogWarning("GameManager NOT Found");
        }
        #endregion

        #region aiManager FindObjectOfType
        aIManager = FindObjectOfType<AIManager>();
        if (aIManager)
        {
            //AIManager Found
        }
        else
        {
            Debug.LogWarning("AIManager NOT Found");

        }
        #endregion

        transform.GetChild(0).gameObject.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(false);

        if (other.CompareTag("Player") && gameManager.currentLap == 0 && gameManager.currentCheckpoint == 0)
        {
            gameManager.currentLap += 1;
            checkpointParent.GetComponent<CheckpointManager>().SetFirstCheckpointMAT();
            transform.GetChild(0).gameObject.SetActive(false);
            gameManager.StartRoundTimer();
            gameManager.CheckLaps();
            minimap.gameObject.SetActive(true);

        }

        if (other.CompareTag("Player") && gameManager.currentCheckpoint == gameManager.checkpoints - 1)
        {
            gameManager.currentLap += 1;

            transform.GetChild(0).gameObject.SetActive(false);
            checkpointParent.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameManager.CheckLaps();

        }



        if (other.CompareTag("AI") && aIManager.currentLap == 0 && aIManager.currentCheckpoint == 0)
        {
            aIManager.currentLap += 1;
        }

        if (other.CompareTag("AI") && aIManager.currentCheckpoint == aIManager.checkpoints - 1)
        {
            aIManager.currentLap += 1;
            aIManager.CheckLaps();

        }

    }




}
