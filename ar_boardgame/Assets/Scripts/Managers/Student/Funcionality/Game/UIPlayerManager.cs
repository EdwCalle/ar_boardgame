
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
public class UIPlayerManager : MonoBehaviour
{
    [Serializable]
    public class Arrows
    {
        public GameObject modal;
        public GameObject leftButton;
        public GameObject rightButton;
        public GameObject upButton;
        public GameObject downButton;
    }
    [Serializable]
    public class MinigameInfo
    {
        public GameObject modal;
        public TextMeshProUGUI MinigameTitle;
    }
    [Serializable]
    public class RoundInfo
    {
        public TextMeshProUGUI roundTitle;
        public TextMeshProUGUI numberRound;
        public GameObject diceButton;
        public TextMeshProUGUI diceTitle;
    }
    [Serializable]
    public class QuestionsInfo
    {
        public GameObject modal;
        public TextMeshProUGUI questionTitle;
        public GameObject[] options;
    }
    [Serializable]
    public class ResultsInfo
    {
        public GameObject modal;
        public TextMeshProUGUI wimmner;
    }
    [Serializable]
    public class PlayerInfo
    {
        public TextMeshProUGUI textStars;
        public TextMeshProUGUI textCaps;
        public TextMeshProUGUI nickname;
        public GameObject modal;
    }
    public RoundInfo roundInfo;
    public PlayerInfo[] playersList;
    //[Header("Directions")]
    public Arrows arrows;
    // [Header("Showing Minigame")]
    public MinigameInfo minigameInfo;

    // [Header("Round Information")]

    //  [Header("Results")]
    public ResultsInfo resultsInfo;
    //  [Header("Questions")]
    public QuestionsInfo questionsInfo;

    public GameObject canvasModal;

    Animator animator;

    public GameObject WaitPlayersModal;
    // Start is called before the first frame update
    void Awake()
    {
        animator = canvasModal.GetComponent<Animator>();

        // canvasModal.SetActive(false);
        arrows.modal.SetActive(false);
        questionsInfo.modal.SetActive(false);
        minigameInfo.modal.SetActive(false);
        resultsInfo.modal.SetActive(false);

    }
    public void HideModal(GameObject gameObject)
    {

        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = false;
        animator.SetTrigger("fadeOut");
        gameObject.SetActive(false);
        Debug.Log("xq pasa esto..");

    }
    public void ShowModal(GameObject gameObject)
    {

        //  canvasModal.SetActive(true);
        //animator.SetBool("fade", true);
        animator.SetTrigger("fadeIn");
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.SetActive(true);

        // gameObject.GetComponent<Animator>().SetBool("fade", true);
    }

    public void SetRoundInfo()
    {
        roundInfo.roundTitle.text = Singleton.GetRoundTitle();
        roundInfo.numberRound.text = $"Número de ronda: {Singleton.GetNumberRound()}";

    }
    public void SetDiceInfo(int dice)
    {
        roundInfo.diceTitle.text = $"{dice}";

    }
    public void SetTitlteMinigame()
    {
        minigameInfo.MinigameTitle.text = "Vamos a jugar:" + Singleton.GetMinigameTitle();
    }
    public void SetPlayersListInfo(List<Student> students)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < students.Count)
            {
                playersList[i].nickname.text = students[i].NickName;
                playersList[i].textCaps.text = students[i].Score.Caps + "";
                playersList[i].textStars.text = students[i].Score.Stars + "";
            }
            else
            {
                // playersList[i].modal.SetActive(false);
            }
        }
    }

    public void ShowMinigameInfo()
    {
        ShowModal(minigameInfo.modal);
        SetTitlteMinigame();
    }
    public void ShowDice()
    {
        roundInfo.diceButton.SetActive(true);
    }
    public void HideDice()
    {
        roundInfo.diceButton.SetActive(false);
    }
    public void ShowResultsInfo()
    {
        ShowModal(resultsInfo.modal);
        resultsInfo.wimmner.text = "El ganador es...." + "name";
    }
    public void ShowQuestionsInfo(bool IsMyTurn, int currentPlayer)
    {
        ShowModal(questionsInfo.modal);
        PrepareQuestion(IsMyTurn, currentPlayer);
    }
    public void PrepareQuestion(bool IsMyTurn, int currentPlayer)
    {
        EnableOptions(IsMyTurn);
        SetQuestion(currentPlayer);
    }
    public void SetQuestion(int currentPlayer)
    {

        questionsInfo.questionTitle.text = Singleton.GetQuestionRound(currentPlayer).Title;
        SetOption(0, Singleton.GetQuestionRound(currentPlayer).OptionA);
        SetOption(1, Singleton.GetQuestionRound(currentPlayer).OptionB);
        SetOption(2, Singleton.GetQuestionRound(currentPlayer).OptionC);
        SetOption(3, Singleton.GetQuestionRound(currentPlayer).OptionD);

    }
    public void SetOption(int pos, string value)
    {
        questionsInfo.options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public async Task SelectOption(int value, int currentPlayer)
    {
        int solution = Singleton.GetQuestionRound(currentPlayer).Solution;
        if (value == solution)
        {

            questionsInfo.options[value].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        else
        {
            questionsInfo.options[value].GetComponent<Animator>().SetInteger("optionState", -1);
            questionsInfo.options[solution].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        EnableOptions(false);
        await Task.Delay(2000);
        HideModal(questionsInfo.modal);
        questionsInfo.options[value].GetComponent<Animator>().SetInteger("optionState", 0);
        questionsInfo.options[solution].GetComponent<Animator>().SetInteger("optionState", 0);
        await Task.Delay(1050);

    }
    void EnableOptions(bool value)
    {
        questionsInfo.options[0].GetComponent<Button>().interactable = value;
        questionsInfo.options[1].GetComponent<Button>().interactable = value;
        questionsInfo.options[2].GetComponent<Button>().interactable = value;
        questionsInfo.options[3].GetComponent<Button>().interactable = value;
    }
    public void ShowArrows(string directionOne, string directionTwo)
    {

        ShowModal(arrows.modal);
        SetActiveArrow(arrows.leftButton, directionOne, directionTwo, "left");
        SetActiveArrow(arrows.rightButton, directionOne, directionTwo, "right");
        SetActiveArrow(arrows.upButton, directionOne, directionTwo, "up");
        SetActiveArrow(arrows.downButton, directionOne, directionTwo, "down");
    }
    public void HideArrows()
    {
        HideModal(arrows.modal);

    }
    void SetActiveArrow(GameObject arrow, string directionOne, string directionTwo, string direction)
    {
        if (directionOne == direction || directionTwo == direction)
            arrow.SetActive(true);
        else
            arrow.SetActive(false);
    }
    public void HideWaitPlayers()
    {
        HideModal(WaitPlayersModal);
    }
    // Update is called once per frame
    void Update()
    {

    }


}
