using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace thisAPP.UIElements
{
    [System.Serializable]
    public class ArrowsUI : Modal
    {
        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject rightButton;
        [SerializeField] private GameObject upButton;
        [SerializeField] private GameObject downButton;
        [SerializeField] private TextMeshProUGUI titleTurn;
        [SerializeField] private Sprite player;

        public GameObject LeftButton { get => leftButton; set => leftButton = value; }
        public GameObject RightButton { get => rightButton; set => rightButton = value; }
        public GameObject UpButton { get => upButton; set => upButton = value; }
        public GameObject DownButton { get => downButton; set => downButton = value; }
        public TextMeshProUGUI TitleTurn { get => titleTurn; set => titleTurn = value; }
        public Sprite Player { get => player; set => player = value; }
    }
    [System.Serializable]
    public class MinigameInfo : Modal
    {
        [SerializeField] private TextMeshProUGUI minigameTitle;

        public TextMeshProUGUI MinigameTitle { get => minigameTitle; set => minigameTitle = value; }
    }
    [System.Serializable]
    public class RoundInfo : Modal
    {
        [SerializeField] private TextMeshProUGUI roundTitle;
        [SerializeField] private TextMeshProUGUI numberRound;
        [SerializeField] private GameObject diceButton;
        [SerializeField] private TextMeshProUGUI diceTitle;

        public TextMeshProUGUI RoundTitle { get => roundTitle; set => roundTitle = value; }
        public TextMeshProUGUI NumberRound { get => numberRound; set => numberRound = value; }
        public GameObject DiceButton { get => diceButton; set => diceButton = value; }
        public TextMeshProUGUI DiceTitle { get => diceTitle; set => diceTitle = value; }
    }
    [System.Serializable]
    public class QuestionsInfo : Modal
    {
        [SerializeField] private TextMeshProUGUI questionTitle;
        [SerializeField] private TextMeshProUGUI textTimer;
        [SerializeField] private GameObject[] options;

        public TextMeshProUGUI QuestionTitle { get => questionTitle; set => questionTitle = value; }
        public GameObject[] Options { get => options; set => options = value; }
        public TextMeshProUGUI TextTimer { get => textTimer; set => textTimer = value; }
        // }
    }
    [System.Serializable]
    public class ResultsInfo : Modal
    {

        [SerializeField] private TextMeshProUGUI winner;


        public TextMeshProUGUI Winner { get => winner; set => winner = value; }
    }
    [System.Serializable]
    public class PlayerInfo : Modal
    {
        [SerializeField] private TextMeshProUGUI textStars;
        [SerializeField] private TextMeshProUGUI textCaps;
        [SerializeField] private TextMeshProUGUI nickname;


        public TextMeshProUGUI TextStars { get => textStars; set => textStars = value; }
        public TextMeshProUGUI TextCaps { get => textCaps; set => textCaps = value; }
        public TextMeshProUGUI Nickname { get => nickname; set => nickname = value; }

    }
    [System.Serializable]
    public class DialogueUi : Modal
    {
        [SerializeField] private TextMeshProUGUI chat;
        [SerializeField] private Button skipButton;
        [SerializeField] private GameObject[] characters;
        [SerializeField] private GameObject[] charactersName;


        public TextMeshProUGUI Chat { get => chat; set => chat = value; }
        public Button SkipButton { get => skipButton; set => skipButton = value; }
        public GameObject[] Characters { get => characters; set => characters = value; }
        public GameObject[] CharactersName { get => charactersName; set => charactersName = value; }

    }
    [System.Serializable]
    public class WaitPlayerUi : Modal
    {
        [SerializeField] private GameObject waitBoard;
        [SerializeField] private GameObject waitPlayer;
        [SerializeField] private TextMeshProUGUI message;

        public GameObject WaitPlayer { get => waitPlayer; set => waitPlayer = value; }
        public TextMeshProUGUI Message { get => message; set => message = value; }
        public GameObject WaitBoard { get => waitBoard; set => waitBoard = value; }
        //  [SerializeField] private TextMeshProUGUI winner;


        //public TextMeshProUGUI Wimmner { get => winner; set => winner = value; }
    }
    [System.Serializable]
    public class DiceUi : Modal
    {
        [SerializeField] private GameObject dice;
        [SerializeField] private GameObject rotationObject;
        [SerializeField] private GameObject limitObject;
        [SerializeField] private Image imageDice;
        [SerializeField] private Sprite[] dices;
        [SerializeField] private GameObject swipeAnimation;
        [SerializeField] private GameObject titleNumberDice;
        [SerializeField] private TextMeshProUGUI titleTurn;
        [SerializeField] private Sprite player;

        public GameObject RotationObject { get => rotationObject; set => rotationObject = value; }
        public GameObject LimitObject { get => limitObject; set => limitObject = value; }
        public Sprite[] Dices { get => dices; set => dices = value; }
        public GameObject SwipeAnimation { get => swipeAnimation; set => swipeAnimation = value; }
        public Image ImageDice { get => imageDice; set => imageDice = value; }
        public GameObject TitleNumberDice { get => titleNumberDice; set => titleNumberDice = value; }
        public GameObject Dice { get => dice; set => dice = value; }
        public TextMeshProUGUI TitleTurn { get => titleTurn; set => titleTurn = value; }
        public Sprite Player { get => player; set => player = value; }
    }
}