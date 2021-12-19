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
                ShieldPowerUp(player);
                break;
            case powerUps.Nitro:
                Debug.Log("Nitro");
                //NitroPowerUp();
                break;
            default:
                break;
        }

    }

    //---------Power Ups-----------------

    private int shieldMin = 1;
    private int shieldMax = 100;

    private void ShieldPowerUp(GameObject player)
    {
        int rand = Random.Range(shieldMin, shieldMax);
        player.GetComponent<PlayerStats>().shield += rand;
        Debug.Log(player.GetComponent<PlayerStats>().shield);
    }


}