using UnityEngine;
using AI;

public class PickUp : MonoBehaviour
{

    private PickUpScriptableObject pickUpObject;
    private PowerUpManager powerUpManager;

    private BaseVehicleManager _vehicleManager;
    //private GameObject player;

    #region Rotate and Hover Points
    Vector3 pointA;
    Vector3 pointB;
    Vector3 rotation;
    #endregion
    [SerializeField]
    [Range(0, 10)]
    private float hoverSpeed = 0.5f;
    private float minhoverSpeed = 0.1f;
    private float maxhoverSpeed = 0.9f;
    [SerializeField]
    [Range(0, 180)]
    private float rotationSpeed = 2f;
    private float minrotationSpeed = 1.5f;
    private float maxrotationSpeed = 2.5f;

    [SerializeField]
    private float timer;
    [SerializeField]
    private float timerCooldown = 5f;
    [SerializeField]
    private bool childObjectDeleted;

    [SerializeField]
    private GameObject powerUpPrefabClone;

    private void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();

        //PlayerManager playermanager = FindObjectOfType<PlayerManager>();
        //player = playermanager.gameObject;

        //if (!playermanager)
        //{
        //    Debug.LogWarning("PlayerManager has NOT been found");
        //}
    }

    private void Start()
    {
        #region Defining Points
        pointA = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        pointB = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        rotation = new Vector3(0, Time.deltaTime, 0);
        #endregion
        childObjectDeleted = true;
        RandomRotateHoverSpeed();

    }

    private void Update()
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


    private void OnTriggerEnter(Collider other)
    {
        _vehicleManager = other.GetComponentInParent<BaseVehicleManager>();

        if (_vehicleManager.CompareTag("Player") || _vehicleManager.CompareTag("AI"))
        {
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
