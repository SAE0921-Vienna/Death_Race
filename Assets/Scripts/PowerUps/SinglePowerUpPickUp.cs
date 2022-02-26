public class SinglePowerUpPickUp : RandomPowerUpSpawnPickUp
{
    /// <summary>
    /// Gets the powerup manager from gameobject
    /// </summary>
    private void Awake()
    {
        powerUpManager = GetComponent<PowerUpManager>();
    }

}
