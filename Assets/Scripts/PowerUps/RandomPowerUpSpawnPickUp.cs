using UnityEngine;
using AI;

public class RandomPowerUpSpawnPickUp : MonoBehaviour
{
    #region References 
    protected PickUpScriptableObject pickUpObject;
    protected PowerUpManager powerUpManager;

    protected BaseVehicleManager _vehicleManager;
    #endregion

    #region Rotate and Hover Points
    protected Vector3 pointA;
    protected Vector3 pointB;
    protected Vector3 rotation;
    #endregion

    #region Hover Information
    [SerializeField]
    [Range(0, 10)]
    protected float hoverSpeed = 0.5f;
    protected float minhoverSpeed = 0.1f;
    protected float maxhoverSpeed = 0.9f;
    [SerializeField]
    protected float hoverOffset = 0.5f;
    [SerializeField]
    [Range(0, 180)]
    protected float rotationSpeed = 2f;
    protected float minrotationSpeed = 45f;
    protected float maxrotationSpeed = 80f;

    [SerializeField]
    protected float timer;
    [SerializeField]
    protected float timerCooldown = 5f;
    [SerializeField]
    protected bool childObjectDeleted;

    [SerializeField]
    protected GameObject powerUpPrefabClone;

    #endregion

    /// <summary>
    /// Finds the powerUpManager
    /// </summary>
    private void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    /// <summary>
    /// Sets pointA, pointB and the hover/rotation speed 
    /// </summary>
    protected void Start()
    {
        pointA = new Vector3(transform.position.x, transform.position.y + hoverOffset, transform.position.z);
        pointB = new Vector3(transform.position.x, transform.position.y - hoverOffset, transform.position.z);
        rotation = new Vector3(0, Time.deltaTime, 0);

        childObjectDeleted = true;
        RandomRotateHoverSpeed();
    }

    /// <summary>
    /// Hover and Rotation Effect are being updated every frame and a new PowerUp will be spawned if timer hits 0 (or when previous PowerUp has been deleted)
    /// </summary>
    protected void Update()
    {
        //Hovering Effect
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.deltaTime * hoverSpeed, 1));
        //Rotation Effect
        transform.Rotate(rotation, rotationSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = -1;
            SpawnPrefab();
        }

    }

    /// <summary>
    /// Adds the powerup to the player or ai (vehicleManager)
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            _vehicleManager = other.GetComponentInParent<BaseVehicleManager>();

            if (!childObjectDeleted)
            {
                #region Manually Add one specific PowerUp
                if (pickUpObject != null && pickUpObject.powerUpType == PickUpScriptableObject.powerUps.Random)
                {
                    int rand = Random.Range(0, powerUpManager.powerUps.Length - 1);
                    pickUpObject = powerUpManager.powerUps[rand];
                    _vehicleManager.GetComponent<PowerUps>().AddToPowerUpList(pickUpObject);
                }
                else if (pickUpObject != null)
                {
                    _vehicleManager.GetComponent<PowerUps>().AddToPowerUpList(pickUpObject);

                }

                #endregion



                #region Randomly Add one PowerUp from the PowerUps List
                //int rand = Random.Range(0, powerUpManager.powerUps.Length);
                //pickUpObject = powerUpManager.powerUps[rand];
                //other.GetComponent<PowerUps>().AddToPowerUpList(pickUpObject);
                #endregion


                Destroy(transform.GetChild(0).gameObject);
                childObjectDeleted = true;
                timer = timerCooldown;
            }

        }
    }

    /// <summary>
    /// Spawns from 0 to the max PowerUps Length a PowerUp
    /// </summary>
    public void SpawnPrefab()
    {

        if (transform.childCount > 1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        if (childObjectDeleted)
        {
            int rand = Random.Range(0, powerUpManager.powerUps.Length);
            pickUpObject = powerUpManager.powerUps[rand];
            powerUpPrefabClone = Instantiate(pickUpObject.powerUpPrefab, transform);
            childObjectDeleted = false;
            RandomRotateHoverSpeed();

        }
    }

    /// <summary>
    /// This method randomizes the hover and the rotationspeed for the pick up powerups
    /// </summary>
    public void RandomRotateHoverSpeed()
    {
        #region Randomizing Hover- and Rotationspeed
        rotationSpeed = Random.Range(minrotationSpeed, maxrotationSpeed);
        hoverSpeed = Random.Range(minhoverSpeed, maxhoverSpeed);
        #endregion
    }

}
