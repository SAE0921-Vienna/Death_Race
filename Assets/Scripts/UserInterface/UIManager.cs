using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class UIManager : MonoBehaviour
    {
        private GameManager gameManager;
        public PlayerManager playerManager;

        public TextMeshProUGUI speedUnit;
        public TextMeshProUGUI ammoAmountUI;
        public Image powerUpUI;
        public TextMeshProUGUI lapsAmountUI;
        public TextMeshProUGUI positionsUI;
        public TextMeshProUGUI roundTimerUI;
        public TextMeshProUGUI roundDeciTimerUI;
        public RectTransform wrongDirectionUI;
        public TextMeshProUGUI highscoreUI;

        public GameObject gameOverUI;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        private void Update()
        {
            lapsAmountUI.text = playerManager.currentLapIndex + "/" + gameManager.laps + " LAPS";
            //positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " POSITION";
            //switch, st, nd, rd, th,...
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
            //roundTimerUI.text =  "TIME: " + gameManager.roundTimerAsSecString;
            //roundDeciTimerUI.text = gameManager.roundTimerAsDeciString;
        }

    }
}
