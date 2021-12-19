using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    public List<ScriptableObject> powerUpList;
    private PickUpScriptableObject powerUp;

    public Image powerUpUI;

  

    private void Update()
    {
        ActivatePowerUp(powerUp);
    }

    /// <summary>
    /// Activates the picked up powerup
    /// </summary>
    /// <param name="powerUp"></param>
    public void ActivatePowerUp(PickUpScriptableObject powerUp)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && powerUp != null)
        {
            powerUpList.Remove(powerUp);
            powerUpUI.color = new Color(0, 0, 0, 0);
            powerUp.PowerUpAction(this.gameObject);

        }
    }

    /// <summary>
    /// Adds the powerUp to the list
    /// </summary>
    /// <param name="powerUp"></param>
    public void AddToPowerUpList(PickUpScriptableObject powerUp)
    {
        if (powerUpList.Count >= 1)
        {
            powerUpList.Clear();
        }
        powerUpList.Add(powerUp);
        powerUpUI.sprite = powerUp.icon;
        powerUpUI.color = new Color(1, 1, 1, 1);
        this.powerUp = powerUp;

    }


}
