using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    public List<ScriptableObject> powerUpList;
    private PickUpScriptableObject powerUp;
    private PlayerStats player;
    private int powerUpListLength = 1;

    public Image powerUpUI;

    private void Awake()
    {
        player = GetComponent<PlayerStats>();

        player.speed = GetComponent<VehicleController>().mAccelerationConstant;
        transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();

    }


    private void Update()
    {
        player.timer -= Time.deltaTime;
        if (player.timer < 0)
        {
            player.timer = -1;
            ResetPowerUps();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && powerUp != null)
        {
            ActivatePowerUp(powerUp);
        }

    }

    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    public void ActivatePowerUp(PickUpScriptableObject powerUp)
    {

        powerUpList.Remove(powerUp);
        powerUpUI.color = new Color(0, 0, 0, 0);
        powerUp.PowerUpAction(this.gameObject);
        this.powerUp = null;
    }


    /// <summary>
    /// Adds the powerUp to the list
    /// </summary>
    /// <param name="powerUp"></param>
    public void AddToPowerUpList(PickUpScriptableObject powerUp)
    {
        if (powerUpList.Count >= powerUpListLength)
        {
            powerUpList.Clear();
        }
        powerUpList.Add(powerUp);
        powerUpUI.sprite = powerUp.icon;
        powerUpUI.color = new Color(1, 1, 1, 1);
        this.powerUp = powerUp;

    }

    /// <summary>
    /// Resets the powerups after some time
    /// </summary>
    public void ResetPowerUps()
    {
        if (player.shield)
        {
            player.shield = false;
            transform.GetChild(0).gameObject.SetActive(false);

        }
        if (player.nitro)
        {
            player.nitro = false;
            //transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
            GetComponent<VehicleController>().mAccelerationConstant = player.speed;
        }
    }

    #region Power Up Methods
    public void ShieldPowerUp()
    {
        player.timer = player.timerCooldown;
        transform.GetChild(0).gameObject.SetActive(true);
        player.shield = true;
    }

    public void NitroPowerUp()
    {
        player.timer = player.timerCooldown;
        //transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        GetComponent<VehicleController>().mAccelerationConstant += player.nitroSpeed;
        player.nitro = true;

    }

    #endregion
}
