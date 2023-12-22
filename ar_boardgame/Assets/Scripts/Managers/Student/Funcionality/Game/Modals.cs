using TMPro;
using UnityEngine;

namespace thisAPP.UIElements
{

    public class Modals
    {
        [System.Serializable]
        public class Arrows
        {
            [SerializeField] private GameObject modal;
            [SerializeField] private GameObject leftButton;
            [SerializeField] private GameObject rightButton;
            [SerializeField] private GameObject upButton;
            [SerializeField] private GameObject downButton;

            public GameObject Modal { get => modal; set => modal = value; }
            public GameObject LeftButton { get => leftButton; set => leftButton = value; }
            public GameObject RightButton { get => rightButton; set => rightButton = value; }
            public GameObject UpButton { get => upButton; set => upButton = value; }
            public GameObject DownButton { get => downButton; set => downButton = value; }
        }
        [System.Serializable]
        public class MinigameInfo
        {
            [SerializeField] private GameObject modal;
            [SerializeField] private TextMeshProUGUI minigameTitle;

            public GameObject Modal { get => modal; set => modal = value; }
            public TextMeshProUGUI MinigameTitle { get => minigameTitle; set => minigameTitle = value; }
        }
        [System.Serializable]
        public class RoundInfo
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
        public class QuestionsInfo
        {
            [SerializeField] private GameObject modal;
            [SerializeField] private TextMeshProUGUI questionTitle;
            [SerializeField] private GameObject[] options;

            public GameObject Modal { get => modal; set => modal = value; }
            public TextMeshProUGUI QuestionTitle { get => questionTitle; set => questionTitle = value; }
            public GameObject[] Options { get => options; set => options = value; }
        }
        [System.Serializable]
        public class ResultsInfo
        {
            [SerializeField] private GameObject modal;
            [SerializeField] private TextMeshProUGUI wimmner;

            public GameObject Modal { get => modal; set => modal = value; }
            public TextMeshProUGUI Wimmner { get => wimmner; set => wimmner = value; }
        }
        [System.Serializable]
        public class PlayerInfo
        {
            [SerializeField] private TextMeshProUGUI textStars;
            [SerializeField] private TextMeshProUGUI textCaps;
            [SerializeField] private TextMeshProUGUI nickname;
            [SerializeField] private GameObject modal;

            public TextMeshProUGUI TextStars { get => textStars; set => textStars = value; }
            public TextMeshProUGUI TextCaps { get => textCaps; set => textCaps = value; }
            public TextMeshProUGUI Nickname { get => nickname; set => nickname = value; }
            public GameObject Modal { get => modal; set => modal = value; }
        }
    }
}