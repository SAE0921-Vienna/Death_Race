using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private PlayerManager playerManager;

        [Header("UI References")]

        public TextMeshProUGUI speedUnit;
        public TextMeshProUGUI ammoAmountUI;
        public Image powerUpUI;
        public TextMeshProUGUI lapsAmountUI;
        public TextMeshProUGUI positionsUI;
        public TextMeshProUGUI roundTimerUI;
        public RectTransform wrongDirectionUI;
        public TextMeshProUGUI highscoreUI;
        public Slider healthSlider;
        public TextMeshProUGUI moneyUI;
        public TextMeshProUGUI tempAddedMoneyUI;
        public Transform ammoToggle;
        public Transform healthToggle;
        public Transform noSpeedLimitToggle;
        public TextMeshProUGUI countDownTimer;

        public GameObject gameOverUI;
        private Timer _timer;

        [SerializeField] private Texture2D crosshair;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            _timer = FindObjectOfType<Timer>();
            _timer.CreateTimer(5f, () => StartCountdown());

            SetCursor();

            
        }

        private void Update()
        {
            roundTimerUI.text = gameManager.roundTimerAsString;
            if (playerManager != null)
            {
                lapsAmountUI.text = playerManager.currentLapIndex + "/" + gameManager.laps + " LAPS";
                switch (playerManager.currentPositionIndex)
                {
                    case 1:
                        positionsUI.text = playerManager.currentPositionIndex + "st";
                        break;
                    case 2:
                        positionsUI.text = playerManager.currentPositionIndex + "nd";
                        break;
                    case 3:
                        positionsUI.text = playerManager.currentPositionIndex + "rd";
                        break;
                    default:
                        break;
                }
            }

            if (int.Parse(ammoAmountUI.text) > 999)
            {
                ammoAmountUI.text = "999";
            }

        }

        public void StartCountdown()
        {
            print("Started Countdown");
            countDownTimer.gameObject.SetActive(true);
            countDownTimer.GetComponent<Animation>().Play();
        }

        public void SetCursor()
        {
            if (crosshair != null)
                Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.ForceSoftware);

            if (gameManager.ghostMode)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            //Debug.Log("Cursor Set");
        }

    }
}
