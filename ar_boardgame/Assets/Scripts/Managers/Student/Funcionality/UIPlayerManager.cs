
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;
public class UIPlayerManager : MonoBehaviour
{
    [Header("Directions")]
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject upButton;
    public GameObject downButton;

    [Header("Round Information")]
    public TextMeshProUGUI titleRound;
    public TextMeshProUGUI numberRound;
    public GameObject buttonDice;
    public TextMeshProUGUI titleDice;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public GameObject buttons;
    public GameObject dialogFinish;

    public GameObject modalQuestion;
    public TextMeshProUGUI question;
    public GameObject[] options;
    // Start is called before the first frame update
    void Awake()
    {
        HideAll();
    }
    public void HideAll()
    {
        buttons.SetActive(false);
        dialogFinish.SetActive(false);
        modalQuestion.SetActive(false);
    }
    public void SetTextButtons()
    {
        text1.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame1.Title;
        text2.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame2.Title;
        text3.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame3.Title;
    }

    public void ShowButtons()
    {
        HideAll();
        buttons.SetActive(true);
    }
    public void ShowDice()
    {
        buttonDice.SetActive(true);
    }
    public void HideDice()
    {
        buttonDice.SetActive(false);
    }
    public void ShowDialogFinish()
    {
        HideAll();
        dialogFinish.SetActive(true);
    }
    public void ShowQuestion()
    {
        modalQuestion.SetActive(true);
        PrepareQuestion();
    }
    public void PrepareQuestion()
    {
        // GetComponent<>().tex
        EnableOptions(true);
        FadeIn(modalQuestion);
        question.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.Title;
        SetOption(0, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.OptionA);
        SetOption(1, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.OptionB);
        SetOption(2, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.OptionC);
        SetOption(3, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.OptionD);
    }
    public void SetOption(int pos, string value)
    {
        options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public void SelectOption(int value)
    {
        EnableOptions(false);
        int solution = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.Solution;
        if (value == solution)
        {
            Blink(options[value], Color.green, 1f);
            //correect
        }
        else
        {
            Blink(options[value], Color.red, 1f);
            Blink(options[solution], Color.green, 1f);
        }
        // Invoke(nameof(FadeOut(modalQuestion, 1f)), 1);
        //  StartCoroutine(DelayedFunction());
        // gameObject.SetActive(false);

        //   HideAll();


    }

    void EnableOptions(bool value)
    {
        options[0].GetComponent<Button>().interactable = value;
        options[1].GetComponent<Button>().interactable = value;
        options[2].GetComponent<Button>().interactable = value;
        options[3].GetComponent<Button>().interactable = value;
    }
    void Blink(GameObject gameObject, Color targetColor, float duration)
    {
        Image myRawImage = gameObject.GetComponent<Image>();
        // Tween the color from the original color to a new color (fully transparent in this case) over the specified duration
        myRawImage.DOColor(targetColor, duration)
            .SetLoops(5, LoopType.Yoyo) // SetLoops with 2 and LoopType.Yoyo creates a single cycle of fading in and out
            .OnComplete(() =>
            {
                myRawImage.color = Color.white;
                modalQuestion.SetActive(false);

            }); // Disable the GameObject after the blinking effect is complete
    }
    void FadeIn(GameObject gameObject)
    {
        gameObject.SetActive(true);
        Fade(gameObject, 0f, 1f, 1.0f);

    }
    async void FadeOut(GameObject gameObject, float duration)
    {
        await Fade(gameObject, 1f, 0f, duration);
        gameObject.SetActive(false);

    }
    Task Fade(GameObject gameObject, float fromAlpha, float toAlpha, float duration)
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        var tcs = new TaskCompletionSource<object>();
        // Ensure the CanvasGroup exists
        if (canvasGroup != null)
        {
            // Set the starting alpha
            canvasGroup.alpha = fromAlpha;

            // Use DOTween to tween the alpha property
            canvasGroup.DOFade(toAlpha, duration)
                .OnComplete(() => tcs.SetResult(null));
        }
        else
        {
            // If there's no CanvasGroup, complete the task immediately
            tcs.SetResult(null);
        }

        return tcs.Task;
    }
    public void ShowArrows(string directionOne, string directionTwo)
    {
        ShowArrow(directionOne);
        ShowArrow(directionTwo);
    }

    void ShowArrow(string direction)
    {
        switch (direction)
        {
            case "left": leftButton.SetActive(true); break;
            case "right": rightButton.SetActive(true); break;
            case "up": upButton.SetActive(true); break;
            case "down": downButton.SetActive(true); break;
        }
    }
    public void HideArrows()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        upButton.SetActive(false);
        downButton.SetActive(false);
    }
    // public void ShowDiceCurrentPlayer()
    // {
    //     if (Singleton.instance.playerTurn == currentPlayer)
    //         playerUI.ShowDice();
    //     else
    //         playerUI.HideDice();
    // }



    // Update is called once per frame
    void Update()
    {

    }

}
