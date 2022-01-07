using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
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

    }


}
