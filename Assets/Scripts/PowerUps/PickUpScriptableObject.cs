using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUps/New PowerUp") ]
public class PickUpScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public GameObject powerUpPrefab;
    public powerUps powerUpType;
    

    public enum powerUps
    {
        Shield,
        Nitro,
        Ammo,
        Bomb

    }

    public void PowerUpAction(GameObject player)
    {

        switch (powerUpType)
        {
            case powerUps.Shield:
                //Debug.Log("Shield");
                player.GetComponent<PowerUps>().ShieldPowerUp();
                break;
            case powerUps.Nitro:
                //Debug.Log("Nitro");
                player.GetComponent<PowerUps>().NitroPowerUp();
                break;
            case powerUps.Ammo:
                //Debug.Log("Ammo");
                player.GetComponent<PowerUps>().AmmoPowerUp();
                break;
            case powerUps.Bomb:
                //Debug.Log("Bomb");
                player.GetComponent<PowerUps>().BombPowerUp();
                break;
            default:
                break;
        }

    }



}