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
        if (other.tag == "Player" && gameManager.nextCheckpoint == gameManager.currentCheckpoint)
        {
            gameManager.CheckLaps();
            gameManager.currentLap += 1;
        }

    }


}
