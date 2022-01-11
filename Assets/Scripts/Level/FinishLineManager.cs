using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private GameManager gameManager;
    private AIManager aIManager;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.currentLap == 0 && gameManager.currentCheckpoint == 0)
        {
            gameManager.currentLap += 1;
        }

        if (other.CompareTag("Player") && gameManager.currentCheckpoint == gameManager.checkpoints - 1)
        {
            gameManager.currentLap += 1;
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
