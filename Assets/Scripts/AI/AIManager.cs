using UnityEngine;

namespace AI
{
    public class AIManager : MonoBehaviour
    {

        public AI_VehicleController aivehicleController;
        [Header("Health")]
        public int health = 100;
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
        public int currentCheckpoint;
        public int nextCheckpoint;
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
            aivehicleController = GetComponent<AI_VehicleController>();

        }

        private void Update()
        {
            if (aivehicleController)
            {
                currentSpeed = Mathf.RoundToInt(aivehicleController.currentSpeed * aivehicleController.mMaxSpeed);
                isOnRoadtrack = aivehicleController.isOnRoadtrack;

            }
            if (health <= 0) gameObject.SetActive(false);


            Debug.DrawLine(transform.position, FacingInfo().point, Color.red);


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
    }
}
