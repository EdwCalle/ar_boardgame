
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.Collections;
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
    public PlayerInfo[] playersList2;
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
        roundInfo.roundTitle.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Title;
        roundInfo.numberRound.text = $"Número de ronda: {Singleton.instance.CurrentRound}";

    }
    public void SetDiceInfo(int dice)
    {
        roundInfo.diceTitle.text = $"{dice}";

    }
    public void SetTitlteMinigame()
    {
        minigameInfo.MinigameTitle.text = "Vamos a jugar:" + Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].MyMinigame.Title;
        // text2.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame2.Title;
        // text3.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame3.Title;
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
    // public void SetPlayersListInfo2(List<Student> students)
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         if (i < students.Count)
    //         {
    //             playersList2[i].nickname.text = students[i].NickName;
    //             playersList2[i].textCaps.text = students[i].Score.Caps + "";
    //             playersList2[i].textStars.text = students[i].Score.Stars + "";
    //         }
    //         else
    //         {
    //             // playersList[i].modal.SetActive(false);
    //         }
    //     }
    // }

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
    public void ShowQuestionsInfo(bool IsMyTurn)
    {
        ShowModal(questionsInfo.modal);
        PrepareQuestion(IsMyTurn);
    }
    public void PrepareQuestion(bool IsMyTurn)
    {
        EnableOptions(IsMyTurn);
        SetQuestion(Singleton.instance.CurrentTurn);
    }
    public void SetQuestion(int currentTurn)
    {
        questionsInfo.questionTitle.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[currentTurn].Title;
        SetOption(0, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[currentTurn].OptionA);
        SetOption(1, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[currentTurn].OptionB);
        SetOption(2, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[currentTurn].OptionC);
        SetOption(3, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[currentTurn].OptionD);

    }
    public void SetOption(int pos, string value)
    {
        questionsInfo.options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public async Task SelectOption(int value)
    {
        int solution = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[Singleton.instance.CurrentTurn].Solution;
        if (value == solution)
        {
            // Blink(questionsInfo.options[value], Color.green, 1f);
            //correect
            questionsInfo.options[value].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        else
        {
            questionsInfo.options[value].GetComponent<Animator>().SetInteger("optionState", -1);
            questionsInfo.options[solution].GetComponent<Animator>().SetInteger("optionState", 1);
            //Blink(questionsInfo.options[value], Color.red, 1f);
            // Blink(questionsInfo.options[solution], Color.green, 1f);
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
    // void Blink(GameObject gameObject, Color targetColor, float duration)
    // {
    //     Image myRawImage = gameObject.GetComponent<Image>();
    //     // Tween the color from the original color to a new color (fully transparent in this case) over the specified duration
    //     myRawImage.DOColor(targetColor, duration)
    //         .SetLoops(5, LoopType.Yoyo) // SetLoops with 2 and LoopType.Yoyo creates a single cycle of fading in and out
    //         .OnComplete(() =>
    //         {
    //             myRawImage.color = Color.white;
    //             //  HideModal(questionsInfo.modal);
    //             //questionsInfo.modal.SetActive(false);

    //         }); // Disable the GameObject after the blinking effect is complete
    // }
    // // void FadeIn(GameObject gameObject)
    // // {
    // //     gameObject.SetActive(true);
    // //     Fade(gameObject, 0f, 1f, 1.0f);

    // // }
    // // async void FadeOut(GameObject gameObject, float duration)
    // // {
    // //     await Fade(gameObject, 1f, 0f, duration);
    // //     gameObject.SetActive(false);

    // // }
    // // Task Fade(GameObject gameObject, float fromAlpha, float toAlpha, float duration)
    // //     {
    // //         CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
    // //     var tcs = new TaskCompletionSource<object>();
    // //         // Ensure the CanvasGroup exists
    // //         if (canvasGroup != null)
    // //         {
    // //             // Set the starting alpha
    // //             canvasGroup.alpha = fromAlpha;

    // //             // Use DOTween to tween the alpha property
    // //             canvasGroup.DOFade(toAlpha, duration)
    // //                 .OnComplete(() => tcs.SetResult(null));
    // //         }
    // //         else
    // // {
    // //     // If there's no CanvasGroup, complete the task immediately
    // //     tcs.SetResult(null);
    // // }

    // // return tcs.Task;
    // //     }
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
