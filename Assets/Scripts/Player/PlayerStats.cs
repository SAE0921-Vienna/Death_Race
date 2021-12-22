using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int health = 100;
    public float nitroSpeed = 50f;
    public float normalSpeed;
    public int ammo;
    public int ammoAdd = 25;
    public int ammoLimit = 100;
    public bool shield;
    public bool nitro;
    public bool canShoot;

    public float timer;
    public float timerCooldown = 5f;

   

}
