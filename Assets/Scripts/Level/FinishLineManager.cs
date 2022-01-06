using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{

    private VehicleController vehicleController;
    private GameManager gameManager;

    private void Awake()
    {
        vehicleController = FindObjectOfType<VehicleController>().GetComponent<VehicleController>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && vehicleController.currentSpeed > 0)
        {
            gameManager.currentLap += 1;
            Debug.Log("Finishline (+1 Lap)");
        }
        if (other.tag == "Player" && vehicleController.currentSpeed <= 0)
        {
            gameManager.currentLap -= 1;
            Debug.Log("Finishline (-1 Lap)");

        }
    }

 
}
