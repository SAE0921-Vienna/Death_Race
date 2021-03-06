using PlayerController;
using System.Collections;
using Core;
using UnityEngine;
using Weapons;

namespace AI
{
    public class BaseVehicleManager : MonoBehaviour, IDamageable
    {
        #region References
        protected ShipWeapon _shipWeapon;
        protected VehicleController _vehicleController;
        protected GameManager _gameManager;
        protected CheckpointManager _checkpointManager;
        protected GameObject spaceShipMesh;
        protected Timer _timer;
        #endregion

        #region Stats
        [Header("Health")]
        public int health = 100;
        public int healthLimit = 100;
        public int healValue = 45;

        [Header("Ammo")]
        public int ammo;
        public int ammoAdd;

        [Header("Speed")]
        public float currentSpeed;

        [Header("PowerUp Activates")]
        public bool hasShield;
        public bool hasNitro;
        public bool canShoot;
        public bool hasBomb;
        public bool hasAmmoPowerUp;
        public bool hasHealed;
        public bool isImmortal;
        public bool isOnRoadtrack;
        public bool isAlive;
        #endregion


        [Header("Cheats")]
        public bool unlimitedAmmo;
        public bool unlimitedHealth;
        public bool noSpeedLimit;

        #region Position and Timer
        [Header("Position")]
        public int currentPositionIndex;
        public int currentLapIndex;
        public int currentCheckpointIndex;
        public int nextCheckpointIndex;
        public Checkpoint nextCheckpoint;
        public Checkpoint previousCheckpoint;
        public float distance;

        [Header("Power Up Timer")]
        public float timer;
        public float timerCooldown = 5f;

        [Header("Off Track Timer")]
        public float offTrackTimer;
        public float offTrackTimerLimit = 5f;
        #endregion

        #region Respawn Information
        [Header("Respawn Transform")]
        public Vector3 spawnPosition;
        public Quaternion spawnRotation;
        public float spawnYOffset = 5f;

        [Header("Spawn Effects")]
        public GameObject[] changeEffectPrefab;
        [SerializeField] private Vector3 effectScale = new Vector3(10f, 10f, 10f);
        [SerializeField] private Transform spaceshipMesh;
        #endregion

        /// <summary>
        /// Health, SpawnPosition and Weapon are initialized
        /// </summary>
        protected virtual void Awake()
        {
            _vehicleController = GetComponent<VehicleController>(); //Could be broken idk.
            _shipWeapon = GetComponent<ShipWeapon>();
            _timer = GetComponent<Timer>();

            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            _gameManager = FindObjectOfType<GameManager>();
            if (!_gameManager)
                Debug.LogWarning("Game Manager NOT Found");

            _checkpointManager = FindObjectOfType<CheckpointManager>();
            if (!_checkpointManager)
                Debug.LogWarning("Checkpoint Manager NOT Found");


            var spaceshipLoad = GetComponent<SpaceshipLoad>();
            health = spaceshipLoad.CurrentShip.health;
            healthLimit = health;

            canShoot = false;

            if (GetComponentInChildren<SpaceshipRotator>())
                spaceshipMesh = GetComponentInChildren<SpaceshipRotator>().transform;
        }

        /// <summary>
        /// Get and adds Ammo
        /// </summary>
        protected virtual void Start()
        {
            nextCheckpoint = _checkpointManager.checkpointsInWorldList[0];
            nextCheckpointIndex = 0;

            isAlive = true;

            if (changeEffectPrefab.Length != 0)
            {
                StartCoroutine(SpawnEffect());
            }
            if (isAlive)
            {
                AddAmmoOnStart();
            }
        }

        /// <summary>
        /// Updates the HealthUI and Checks if vehicle is on track
        /// </summary>
        protected virtual void Update()
        {
            UpdateValues();
            CheckIfOnTrack();
            Die();

        }

        /// <summary>
        /// Updates the required values during runtime.
        /// </summary>
        protected virtual void UpdateValues()
        {
            currentSpeed = _vehicleController.GetSpeed();
            isOnRoadtrack = _vehicleController.isOnRoadtrack;
            if (nextCheckpoint != null)

                distance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);
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
                StartCoroutine(GetAmmo());
                //ammoLimit = playerShipWeapon.GetAmmo() * 3;
            }
        }

        /// <summary>
        /// Subtracts _damage from the Vehicles health.
        /// </summary>
        /// <param name="_damage"></param>
        public void GetDamage(int _damage)
        {
            if (!isImmortal)
            {
                health -= _damage;
            }
        }

        public virtual void Die()
        {
            if (health <= 0f)
            {
                if (isAlive == true)
                {
                    Invoke(nameof(RespawnVehicle), 2f);
                    spaceshipMesh.gameObject.SetActive(false);
                    GetComponent<ShipWeapon>().enabled = false;
                    GetComponent<IExplosion>().Explode();
                    isAlive = false;
                }
            }
        }

        /// <summary>
        /// Checks if the vehicle is on the roadtrack
        /// </summary>
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
                //gameObject.SetActive(false);
                RespawnVehicle();
            }
        }

        /// <summary>
        /// Respawns the vehicle at last saved checkpoint
        /// </summary>
        public virtual void RespawnVehicle()
        {
            health = healthLimit;
            StartCoroutine(SpawnEffect());
            spaceshipMesh.gameObject.SetActive(true);
            GetComponent<ShipWeapon>().enabled = true;
            isAlive = true;
        }

        public IEnumerator SpawnEffect()
        {
            GameObject effectClone = Instantiate(changeEffectPrefab[Random.Range(0, changeEffectPrefab.Length)], spaceshipMesh);
            effectClone.transform.position = new Vector3(spaceshipMesh.position.x, spaceshipMesh.position.y - 1f, spaceshipMesh.position.z);
            effectClone.transform.localScale = effectScale;

            yield return new WaitForSeconds(effectClone.GetComponent<ParticleSystem>().main.duration);

            Destroy(effectClone, 1.2f);
        }

        public IEnumerator GetAmmo()
        {
            yield return new WaitForSeconds(3f);

            //ammo = _shipWeapon.GetAmmo();
            ammoAdd = _shipWeapon.GetAmmo();
            //canShoot = true;
        }
    }


}