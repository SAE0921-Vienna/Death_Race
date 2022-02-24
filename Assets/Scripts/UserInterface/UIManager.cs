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

        public GameObject gameOverUI;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void Update()
        {
            roundTimerUI.text = gameManager.roundTimerAsString;

            lapsAmountUI.text = playerManager.currentLapIndex + "/" + gameManager.laps + " LAPS";

            //positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " POSITION";
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

    }
}
