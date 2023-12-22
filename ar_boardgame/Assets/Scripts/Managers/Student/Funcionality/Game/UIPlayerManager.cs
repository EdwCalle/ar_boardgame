
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using static thisAPP.UIElements.Modals;
public class UIPlayerManager : MonoBehaviour
{
    public RoundInfo roundInfo;
    public PlayerInfo[] playersList;
    public Arrows arrows;
    public MinigameInfo minigameInfo;
    public ResultsInfo resultsInfo;
    public QuestionsInfo questionsInfo;

    public GameObject canvasModal;

    Animator animator;

    public GameObject WaitPlayersModal;
    public GameObject DialogueModal;

    public DialogueController dialogueController;
    // Start is called before the first frame update
    void Awake()
    {
        animator = canvasModal.GetComponent<Animator>();

        // canvasModal.SetActive(false);
        arrows.Modal.SetActive(false);
        questionsInfo.Modal.SetActive(false);
        minigameInfo.Modal.SetActive(false);
        resultsInfo.Modal.SetActive(false);
        DialogueModal.SetActive(false);

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
        roundInfo.RoundTitle.text = Singleton.GetRoundTitle();
        roundInfo.NumberRound.text = $"Número de ronda: {Singleton.GetNumberRound()}";

    }
    public void SetDiceInfo(int dice)
    {
        roundInfo.DiceTitle.text = $"{dice}";

    }
    public void SetTitlteMinigame()
    {
        minigameInfo.MinigameTitle.text = "Vamos a jugar:" + Singleton.GetMinigameTitle();
    }
    public void SetDialogue()
    {
        dialogueController.Sentences = Singleton.GetSentences();
    }
    public void SetPlayersListInfo(List<Student> students)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < students.Count)
            {
                playersList[i].Nickname.text = students[i].NickName;
                playersList[i].TextCaps.text = students[i].Score.Caps + "";
                playersList[i].TextStars.text = students[i].Score.Stars + "";
            }
            else
            {
                // playersList[i].modal.SetActive(false);
            }
        }
    }

    public void ShowMinigameInfo()
    {
        ShowModal(minigameInfo.Modal);
        SetTitlteMinigame();
    }
    public void ShowDialogue()
    {

        ShowModal(DialogueModal);
        SetDialogue();
    }
    public void ShowDice()
    {
        roundInfo.DiceButton.SetActive(true);
    }
    public void HideDice()
    {
        roundInfo.DiceButton.SetActive(false);
    }
    public void ShowResultsInfo()
    {
        ShowModal(resultsInfo.Modal);
        resultsInfo.Wimmner.text = "El ganador es...." + "name";
    }
    public void ShowQuestionsInfo(bool IsMyTurn, int currentPlayer)
    {
        ShowModal(questionsInfo.Modal);
        PrepareQuestion(IsMyTurn, currentPlayer);
    }
    public void PrepareQuestion(bool IsMyTurn, int currentPlayer)
    {
        EnableOptions(IsMyTurn);
        SetQuestion(currentPlayer);
    }
    public void SetQuestion(int currentPlayer)
    {

        questionsInfo.QuestionTitle.text = Singleton.GetQuestionRound(currentPlayer).Title;
        SetOption(0, Singleton.GetQuestionRound(currentPlayer).OptionA);
        SetOption(1, Singleton.GetQuestionRound(currentPlayer).OptionB);
        SetOption(2, Singleton.GetQuestionRound(currentPlayer).OptionC);
        SetOption(3, Singleton.GetQuestionRound(currentPlayer).OptionD);

    }
    public void SetOption(int pos, string value)
    {
        questionsInfo.Options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public async Task SelectOption(int value, int currentPlayer)
    {
        int solution = Singleton.GetQuestionRound(currentPlayer).Solution;
        if (value == solution)
        {

            questionsInfo.Options[value].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        else
        {
            questionsInfo.Options[value].GetComponent<Animator>().SetInteger("optionState", -1);
            questionsInfo.Options[solution].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        EnableOptions(false);
        await Task.Delay(2000);
        HideModal(questionsInfo.Modal);
        questionsInfo.Options[value].GetComponent<Animator>().SetInteger("optionState", 0);
        questionsInfo.Options[solution].GetComponent<Animator>().SetInteger("optionState", 0);
        await Task.Delay(1050);

    }
    void EnableOptions(bool value)
    {
        questionsInfo.Options[0].GetComponent<Button>().interactable = value;
        questionsInfo.Options[1].GetComponent<Button>().interactable = value;
        questionsInfo.Options[2].GetComponent<Button>().interactable = value;
        questionsInfo.Options[3].GetComponent<Button>().interactable = value;
    }
    public void ShowArrows(string directionOne, string directionTwo)
    {

        ShowModal(arrows.Modal);
        SetActiveArrow(arrows.LeftButton, directionOne, directionTwo, "left");
        SetActiveArrow(arrows.RightButton, directionOne, directionTwo, "right");
        SetActiveArrow(arrows.UpButton, directionOne, directionTwo, "up");
        SetActiveArrow(arrows.DownButton, directionOne, directionTwo, "down");
    }
    public void HideArrows()
    {
        HideModal(arrows.Modal);

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
