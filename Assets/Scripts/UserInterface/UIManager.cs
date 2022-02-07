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

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        private void Update()
        {
            lapsAmountUI.text = gameManager.currentLap + "/" + gameManager.laps + " LAPS";
            //positionsUI.text = gameManager.playerPosition + "/" + gameManager.positions + " POSITION";
            //switch, st, nd, rd, th,...
            positionsUI.text = gameManager.playerPosition +"st";
            roundTimerUI.text = gameManager.roundTimer.ToString();
        }

    }
}
