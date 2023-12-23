using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace thisAPP.UIElements
{
    [System.Serializable]
    public class Arrows : ModalAnimator
    {
        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject rightButton;
        [SerializeField] private GameObject upButton;
        [SerializeField] private GameObject downButton;

        public GameObject LeftButton { get => leftButton; set => leftButton = value; }
        public GameObject RightButton { get => rightButton; set => rightButton = value; }
        public GameObject UpButton { get => upButton; set => upButton = value; }
        public GameObject DownButton { get => downButton; set => downButton = value; }
    }
    [System.Serializable]
    public class MinigameInfo : ModalAnimator
    {
        [SerializeField] private TextMeshProUGUI minigameTitle;

        public TextMeshProUGUI MinigameTitle { get => minigameTitle; set => minigameTitle = value; }
    }
    [System.Serializable]
    public class RoundInfo : ModalAnimator
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
    public class QuestionsInfo : ModalAnimator
    {

        [SerializeField] private TextMeshProUGUI questionTitle;
        [SerializeField] private GameObject[] options;


        public TextMeshProUGUI QuestionTitle { get => questionTitle; set => questionTitle = value; }
        public GameObject[] Options { get => options; set => options = value; }
    }
    [System.Serializable]
    public class ResultsInfo : ModalAnimator
    {

        [SerializeField] private TextMeshProUGUI winner;


        public TextMeshProUGUI Winner { get => winner; set => winner = value; }
    }
    [System.Serializable]
    public class PlayerInfo : ModalAnimator
    {
        [SerializeField] private TextMeshProUGUI textStars;
        [SerializeField] private TextMeshProUGUI textCaps;
        [SerializeField] private TextMeshProUGUI nickname;


        public TextMeshProUGUI TextStars { get => textStars; set => textStars = value; }
        public TextMeshProUGUI TextCaps { get => textCaps; set => textCaps = value; }
        public TextMeshProUGUI Nickname { get => nickname; set => nickname = value; }

    }
    [System.Serializable]
    public class DialogueUi : ModalAnimator
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
    public class WaitPlayerUi : ModalAnimator
    {

        //  [SerializeField] private TextMeshProUGUI winner;


        //public TextMeshProUGUI Wimmner { get => winner; set => winner = value; }
    }

}