
using UnityEngine;
using System.Threading.Tasks;
using thisAPP.UIElements;
public class UIMediatorManager : MonoBehaviour
{
    public RoundInfo roundInfo;
    // public PlayerInfo[] playersList;
    public WaitPlayerManager waitPlayerManager;
    public DialogueManager dialogueManager;
    public DiceManager diceManager;
    public ArrowsManager arrowsManager;
    public MinigameInfoManager minigameInfoManager;
    public QuestionInfoManager questionManager;
    public ResultsInfoManager resultsInfoManager;
    public BoardManager boardManager;
    public GameObject canvasModal;
    //public Photon pw;
    void Awake()
    {
        // canvasModal.SetActive(false);
        arrowsManager.uiElements.Modal.SetActive(false);
        dialogueManager.uiElements.Modal.SetActive(false);
        diceManager.uiElements.Modal.SetActive(false);
        questionManager.uiElements.Modal.SetActive(false);
        minigameInfoManager.uiElements.Modal.SetActive(false);
        resultsInfoManager.uiElements.Modal.SetActive(false);
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
    // public void SetPlayersListInfo(List<Student> students)
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         if (i < students.Count)
    //         {
    //             playersList[i].Nickname.text = students[i].NickName;
    //             playersList[i].TextCaps.text = students[i].Score.Caps + "";
    //             playersList[i].TextStars.text = students[i].Score.Stars + "";
    //         }
    //         else
    //         {
    //             // playersList[i].modal.SetActive(false);
    //         }
    //     }
    // }
    public void ShowMinigameInfo()
    {
        minigameInfoManager.SetTitlteMinigame();
        minigameInfoManager.uiElements.ShowModal(canvasModal);
    }
    public void HideMinigameInfo()
    {
        minigameInfoManager.uiElements.HideModal(canvasModal);
    }
    public async void ShowDialogue()
    {
        await Task.Delay(1050);
        dialogueManager.SetDialogue();
        dialogueManager.uiElements.ShowModal(canvasModal);
        await Task.Delay(1050);
        dialogueManager.NextSentece();
    }
    public async void HideDialogue()
    {
        dialogueManager.uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
        //make a waiting players
        ShowDice();
    }
    public void ShowDice()
    {
        // roundInfo.DiceButton.SetActive(true);
        diceManager.uiElements.ShowModal(canvasModal);
        diceManager.SetDiceInfo();
    }
    public void RollDice(int numberDice)
    {
        // roundInfo.DiceButton.SetActive(true);
        boardManager.RollDice(numberDice);
    }
    public void HideDice()
    {
        roundInfo.DiceButton.SetActive(false);
        diceManager.uiElements.HideModal(canvasModal);
    }
    public void ShowResultsInfo()
    {
        resultsInfoManager.SetResultsInfo();
        resultsInfoManager.uiElements.ShowModal(canvasModal);
    }
    public void ShowQuestions()
    {
        questionManager.PrepareQuestion();
        questionManager.uiElements.ShowModal(canvasModal);
    }
    public async Task SelectOption(int value)
    {

        await questionManager.SelectOption(value);
        questionManager.uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
    }
    public void ShowArrows(string directionOne, string directionTwo)
    {
        arrowsManager.ActivateArrows(directionOne, directionTwo);
        arrowsManager.uiElements.ShowModal(canvasModal);
    }
    public void HideArrows()
    {
        arrowsManager.uiElements.HideModal(canvasModal);
    }
    public void HideWaitPlayers()
    {
        waitPlayerManager.uiElements.HideModal(canvasModal);

    }

    public void ThrowDice(int numberDice)
    {
        boardManager.ThrowDice(numberDice);
    }
}
