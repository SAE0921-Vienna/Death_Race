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
        public RectTransform wrongDirectionUI;
        public TextMeshProUGUI highscoreUI;

        public GameObject gameOverUI;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        private void Update()
        {
            lapsAmountUI.text = gameManager.currentLap + "/" + gameManager.laps + " LAPS";
            //positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " POSITION";
            //switch, st, nd, rd, th,...
            switch (gameManager.playerPosition)
            {
                case 1:
                    positionsUI.text = gameManager.playerPosition + "st";
                    break;
                case 2:
                    positionsUI.text = gameManager.playerPosition + "nd";
                    break;
                case 3:
                    positionsUI.text = gameManager.playerPosition + "rd";
                    break;
                default:
                    break;
            }
            roundTimerUI.text = gameManager.roundTimer.ToString();
        }

    }
}
