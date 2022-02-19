using PlayerController;
using UnityEngine;

namespace AI
{
    public class BaseVehicleManager : MonoBehaviour, IDamageable
    {
        protected ShipWeapon _shipWeapon;
        protected VehicleController _vehicleController;
        protected GameManager _gameManager;
        protected CheckpointManager _checkpointManager;
        
        [Header("Health")]
        public int health = 100;
        public int healthLimit = 100;
        public int healValue = 45;
        
        [Header("Ammo")]
        public int ammo;
        public int ammoAdd;
        public int ammoLimit;
        public float bombTimer = 10f;
        public Vector3 bombScale = new Vector3(5, 5, 5);
        
        [Header("Speed")]
        public float currentSpeed;
        
        [Header("PowerUp Activates")]
        public bool hasShield;
        public bool hasNitro;
        public bool canShoot;
        public bool hasBomb;
        public bool isImmortal;
        public bool isOnRoadtrack;

        [Header("Position")] 
        public int currentPositionIndex;
        public int currentLapIndex;
        public int currentCheckpointIndex;
        public int nextCheckpointIndex;
        public Checkpoint nextCheckpoint; //!
        
        [Header("Power Up Timer")]
        public float timer;
        public float timerCooldown = 5f;
        
        [Header("Off Track Timer")]
        public float offTrackTimer;
        public float offTrackTimerLimit = 5f;
        
        [Header("Respawn Transform")]
        public Vector3 spawnPosition;
        public Quaternion spawnRotation;
        public float spawnYOffset = 5f;


        protected virtual void Awake()
        {
            _vehicleController = GetComponent<VehicleController>(); //Could be broken idk.
            _shipWeapon = GetComponent<ShipWeapon>();

            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
            
            _gameManager = FindObjectOfType<GameManager>();
            if (!_gameManager)
                Debug.LogWarning("Game Manager NOT Found");

            _checkpointManager = FindObjectOfType<CheckpointManager>();
            if (!_checkpointManager)
                Debug.LogWarning("Checkpoint Manager NOT Found");
            
            nextCheckpoint = _checkpointManager.checkpointsInWorldList[0];
            nextCheckpointIndex = 0;
        }

        protected void Start()
        {
            AddAmmoOnStart();
        }

        protected virtual void Update()
        {
            UpdateValues();
            CheckIfOnTrack();
        }

        /// <summary>
        /// Updates the required values during runtime.
        /// </summary>
        protected void UpdateValues()
        {
            currentSpeed = _vehicleController.GetSpeed();
            isOnRoadtrack = _vehicleController.isOnRoadtrack;

            if (health <= 0)
            {
                //gameObject.SetActive(false);
                RespawnVehicle();
                health = healthLimit;
            }
        }

        /// <summary>
        /// Adds the amount of Ammo, specified in WeaponData during start.
        /// </summary>
        protected void AddAmmoOnStart()
        {
            if (!_shipWeapon)
            {
                Debug.LogFormat("No Weapon Found {0}", gameObject.name);
            }
            else
            {
                ammo = _shipWeapon.GetAmmo();
                ammoAdd = _shipWeapon.GetAmmo();
                //ammoLimit = playerShipWeapon.GetAmmo() * 3;
            }
        }

        /// <summary>
        /// Subtracts _damage from the Vehicles health.
        /// </summary>
        /// <param name="_damage"></param>
        public void GetDamage(int _damage)
        {
            health -= _damage;
        }

        public void CheckIfOnTrack()
        {
            if (!isOnRoadtrack)
            {
                offTrackTimer += Time.deltaTime;
            }
            else
            {
                offTrackTimer = 0;
            }
            if (offTrackTimer > offTrackTimerLimit)
            {
                gameObject.SetActive(false);
                RespawnVehicle();
            }
        }

        public void RespawnVehicle()
        {
            _vehicleController.currentSpeed = 0f;
            _vehicleController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _vehicleController.isOnRoadtrack = true;
            
            var vehicleTransform = transform;
            vehicleTransform.position = spawnPosition;
            vehicleTransform.rotation = spawnRotation;
            
            gameObject.SetActive(true);
        }
    }
}