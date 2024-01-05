using System.Collections;
using System.Threading.Tasks;
using Models;
using thisAPP.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionInfoManager : MonoBehaviour, IModalAnimator
{
    public QuestionsInfo uiElements;
    public GameManager gameManager;
    public float defaultTime = 60f;
    float elapsedTime;
    private Coroutine myCoroutine;
    private Question currentQuestion;
    private SquareType squareType;



    public void PrepareQuestion()
    {
        EnableOptions(Singleton.IsMyTurn());
        SetQuestion();
    }
    void EnableOptions(bool value)
    {
        uiElements.Options[0].GetComponent<Button>().interactable = value;
        uiElements.Options[1].GetComponent<Button>().interactable = value;
        uiElements.Options[2].GetComponent<Button>().interactable = value;
        uiElements.Options[3].GetComponent<Button>().interactable = value;
    }
    public void SetQuestion()
    {
        uiElements.QuestionTitle.text = currentQuestion.Title;
        SetOption(0, currentQuestion.OptionA);
        SetOption(1, currentQuestion.OptionB);
        SetOption(2, currentQuestion.OptionC);
        SetOption(3, currentQuestion.OptionD);
        uiElements.TextTimer.text = defaultTime.ToString("0#");
    }
    public void SetOption(int pos, string value)
    {
        uiElements.Options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public void SelectOption(int value)
    {
        bool solution = value == currentQuestion.Solution;

        gameManager.SelectOption(value, solution, squareType);
    }
    public async Task CheckOption(int value)
    {
        print(squareType);
        // print("no se cuanto");
        int solution = currentQuestion.Solution;
        if (value == -1)
        {
            uiElements.Options[solution].GetComponent<Animator>().SetInteger("optionState", 2);
        }
        else if (value == solution)
        {
            StopCoroutine(myCoroutine);
            uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", 1);
            gameManager.playersInfoManager.UpdateScore(Singleton.GetCurrentPlayerTurn());
        }
        else
        {
            StopCoroutine(myCoroutine);
            uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", -1);
            uiElements.Options[solution].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        EnableOptions(false);
        await Task.Delay(2000);
        if (value != -1)
            uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", 0);
        uiElements.Options[solution].GetComponent<Animator>().SetInteger("optionState", 0);
        await HideModal(gameManager.canvasModal);
        if (squareType == SquareType.angel || squareType == SquareType.demon)
        {
            //  gameManager.ShowQuestions(SquareType.normal);
            await PrepareModal(SquareType.normal);
        }
        else
        {
            //  print("quiero saber donde ocurre el error 2");
            await gameManager.boardManager.FinishTurn();
        }

    }
    public async Task PrepareModal(SquareType squareType, int RandomNumber = 0)
    {
        this.squareType = squareType;
        if (squareType == SquareType.demon)
        {
            currentQuestion = Singleton.GetDemonQuestion(RandomNumber);
        }
        if (squareType == SquareType.angel)
        {

            currentQuestion = Singleton.GetAngelQuestion(RandomNumber);
        }
        if (squareType == SquareType.normal)
        {
            currentQuestion = Singleton.GetQuestionRound(Singleton.GetCurrentPlayerTurn());
        }
        await ShowModal(gameManager.canvasModal);
    }
    public async Task ShowModal(GameObject canvasModal)
    {
        PrepareQuestion();
        uiElements.ShowModal(canvasModal, "QuestionInfo");
        await Task.Delay(1050);
        myCoroutine = StartCoroutine(Countdown());

    }
    IEnumerator Countdown()
    {
        elapsedTime = defaultTime;
        while (elapsedTime >= 0)
        {
            uiElements.TextTimer.text = elapsedTime.ToString("0#");
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
        if (Singleton.IsMyTurn())
        {
            EnableOptions(false);
            gameManager.FromMyTurn();
        }

    }
    public async void FromMyTurn()
    {
        await CheckOption(-1);
    }

    public async Task HideModal(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal, "QuestionInfo");
        await Task.Delay(1050);
    }
}
