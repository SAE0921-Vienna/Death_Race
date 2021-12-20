using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New PowerUp")]
public class PickUpScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public powerUps powerUpType;

    public enum powerUps
    {
        Shield,
        Nitro,

    }

    public void PowerUpAction(GameObject player)
    {

        switch (powerUpType)
        {
            case powerUps.Shield:
                Debug.Log("Shield");
                player.GetComponent<PowerUps>().ShieldPowerUp();
                break;
            case powerUps.Nitro:
                Debug.Log("Nitro");
                player.GetComponent<PowerUps>().NitroPowerUp();
                break;
            default:
                break;
        }

    }



}