using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private PickUpScriptableObject pickUpObject;
    private PowerUpManager powerUpManager;

    private void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            #region Manually Add one specific PowerUp
            //if (pickUpObject != null)
            //{
            //    other.GetComponent<PowerUps>().AddToPowerUpList(pickUpObject);
            //}
            #endregion

            #region Randomly Add one PowerUp from the PowerUps List

            int rand = Random.Range(0, powerUpManager.powerUps.Length);
            pickUpObject = powerUpManager.powerUps[rand];
            other.GetComponent<PowerUps>().AddToPowerUpList(pickUpObject);

            #endregion

            Destroy(gameObject);
        }
    }

}
