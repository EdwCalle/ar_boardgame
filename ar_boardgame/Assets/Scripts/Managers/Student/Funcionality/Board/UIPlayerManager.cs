
using UnityEngine;
using System.Threading.Tasks;
using thisAPP.UIElements;
public class UIPlayerManager : MonoBehaviour
{
    public RoundInfo roundInfo;
    // public PlayerInfo[] playersList;
    public WaitPlayerManager waitPlayerManager;
    public DialogueManager dialogueManager;
    public ArrowsManager arrowsManager;
    public MinigameInfoManager minigameInfoManager;
    public QuestionInfoManager questionManager;
    public ResultsInfoManager resultsInfoManager;
    public GameObject canvasModal;
    void Awake()
    {
        // canvasModal.SetActive(false);
        arrowsManager.uiElements.Modal.SetActive(false);
        dialogueManager.uiElements.Modal.SetActive(false);
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
    public void ShowDialogue()
    {
        dialogueManager.SetDialogue();
        dialogueManager.uiElements.ShowModal(canvasModal);
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
        resultsInfoManager.SetResultsInfo();
        resultsInfoManager.uiElements.ShowModal(canvasModal);
    }
    public void ShowQuestions(bool IsMyTurn, int currentPlayer)
    {
        questionManager.PrepareQuestion(IsMyTurn, currentPlayer);
        questionManager.uiElements.ShowModal(canvasModal);
    }
    public async Task SelectOption(int value, int currentPlayer)
    {
        await questionManager.SelectOption(value, currentPlayer);
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



}
