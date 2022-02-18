using PlayerController;
using UnityEngine;

namespace AI
{
    public class AIManager : MonoBehaviour, IDamageable
    {
        public VehicleController ai_vehicleController;
        
        [Header("Health")]
        public int health = 100;
        public int healthLimit = 100;
        
        [Header("Nitro")]
        public float nitroSpeed = 50f;
        public float normalMaxSpeed;
        public float currentSpeed;
        
        [Header("Ammo")]
        public int ammo;
        //public int ammoAdd = 25;
        public int ammoAdd;
        //public int ammoLimit = 100;
        public int ammoLimit;
        public float bombTimer = 10f;
        public Vector3 bombScale = new Vector3(5, 5, 5);
        
        [Header("PowerUp Activates")]
        public bool shield;
        public bool nitro;
        public bool canShoot;
        public bool bomb;
        public bool isImmortal;
        public bool isOnRoadtrack;
        public bool isFacingCorrectDirection;

        [Header("Laps")]
        public int laps = 3;
        public int currentLap;
        
        [Header("Positions")]
        public int aiPosition;
        
        [Header("Checkpoints")]
        public int checkpoints;
        public int currentCheckpointIndex;
        public int nextCheckpointIndex;
        public Checkpoint nextCheckpoint;
        
        [Header("Round Timer")]
        public float roundTimer;
        
        [Header("Off Track Timer")]
        public float offTrackTimer;
        public float offTrackTimerLimit = 5f;

        public Vector3 spawnAIPosition;
        public Quaternion spawnAIRotation;
        public float spawnAIYOffset = 5f;

        [Header("Power Up Timer")]
        public float timer;
        public float timerCooldown = 5f;


        private void Awake()
        {
            ai_vehicleController = GetComponent(typeof(VehicleController)) as AI_VehicleController_ML;
            spawnAIPosition = transform.position;
            spawnAIRotation = transform.rotation;
        }

        private void Update()
        {
            if (ai_vehicleController)
            {
                currentSpeed = Mathf.RoundToInt(ai_vehicleController.currentSpeed * ai_vehicleController.mMaxSpeed);

                isOnRoadtrack = ai_vehicleController.isOnRoadtrack;

            }
            if (health <= 0) gameObject.SetActive(false);

            Debug.DrawLine(transform.position, FacingInfo().point, Color.red);
            CheckIfOnTrack();

            if (health <= 0)
            {
                RespawnAI();
                health = healthLimit;
            }

        }

        public RaycastHit FacingInfo()
        {
            Physics.Raycast(transform.position, transform.forward, out var hit);

            return hit;
        }

        public void CheckLaps()
        {
            if (currentLap > laps)
            {
                currentLap = laps;
                //Game Finish
                Debug.Log("YAY FINISH");
            }
            if (currentLap < 0)
            {
                currentLap = 0;
            }

        }

        public void GetDamage(int _damage)
        {
            health -= _damage;
            Debug.Log(this.gameObject.name + "bekommt Schaden: " + _damage);
        }

        public void CheckIfOnTrack()
        {
                if (!this.isOnRoadtrack)
                {
                    offTrackTimer += Time.deltaTime;
                }
                else
                {
                    offTrackTimer = 0;
                }

                if (offTrackTimer > offTrackTimerLimit)
                {
                    //this.gameObject.SetActive(false);
                    //RespawnAI();
                }
        }

        public void RespawnAI()
        {
            offTrackTimer = 0;

            spawnAIPosition = new Vector3(spawnAIPosition.x, spawnAIPosition.y + spawnAIYOffset, spawnAIPosition.z);
            this.ai_vehicleController.currentSpeed = 0;
            this.ai_vehicleController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.ai_vehicleController.isOnRoadtrack = true;
            this.transform.position = spawnAIPosition;
            this.transform.rotation = spawnAIRotation;
            this.gameObject.SetActive(true);
        }
    }
}
