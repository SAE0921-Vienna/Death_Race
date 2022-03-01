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

        public GameObject gameOverUI;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerManager = FindObjectOfType<PlayerManager>();
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

            if(int.Parse(ammoAmountUI.text) > 999)
            {
                ammoAmountUI.text = "999";
            }
               
        }

    }
}
