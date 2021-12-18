using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUpManager : MonoBehaviour
{

    public IPowerUp iPowerUp;

    public List<IPowerUp> powerUps = new List<IPowerUp>();


    private void OnTriggerEnter(Collider other)
    {
        iPowerUp = GetComponent<IPowerUp>();
        iPowerUp.InterfacePickUpResponse();
    }



}
