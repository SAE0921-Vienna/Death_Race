using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;
    private CheckpointManager checkpointManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        checkpointManager = GetComponentInParent<CheckpointManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Checkpoint");
            checkpointManager.PlayerThroughCheckpoint(this);

        }
     
    }

    //public void SetTrackCheckPoints(CheckpointManager trackCheckpoints)
    //{
    //    this.checkpointManager = trackCheckpoints;
    //}


}
