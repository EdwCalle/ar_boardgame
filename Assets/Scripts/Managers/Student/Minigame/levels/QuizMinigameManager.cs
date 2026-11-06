
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuizMinigameManager : MonoBehaviour, IMinigame
{
    [System.Serializable]
    public class OptionUi
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Toggle toggle;
        public TextMeshProUGUI Label { get => label; set => label = value; }
        public Toggle Toggle { get => toggle; set => toggle = value; }

    }
    public MinigameManager minigameManager;
    public TextMeshProUGUI questionTitle;
    public TextMeshProUGUI currentQuestionText;
    public OptionUi[] optionUis = new OptionUi[4];
    public int currentItem = 0;
    public int lastItem = 0;
    public void LoadLevel()
    {


    }
    public void StartLevel()
    {
        ShowOptionById(0);
        gameObject.SetActive(true);
    }
    public void ShowOptionById(int index)
    {
        //if (lastItem != index)
        minigameManager.models3DManager.ActiveCurrentModel3D(lastItem, index);
        SetQuestionInfo(index);
        if (Singleton.GetCurrentMinigame().TimerState == TimerState.feedback)
            ShowFeedbackItem(index);
    }
    public void SetQuestionInfo(int index)
    {
        currentQuestionText.text = $"Número de preguntas: {index + 1}/{Singleton.GetCurrentMinigame().Models.Count}";
        Question question = Singleton.GetCurrentMinigame().Models[index].Question;
        questionTitle.text = question.Title;
        optionUis[0].Label.text = question.OptionA;
        optionUis[1].Label.text = question.OptionB;
        optionUis[2].Label.text = question.OptionC;
        optionUis[3].Label.text = question.OptionD;
        optionUis[question.OptionSelected].Toggle.isOn = true;


    }
    public void OnRight()
    {
        lastItem = currentItem;
        if (currentItem == Singleton.GetCurrentMinigame().Models.Count - 1)
        {
            currentItem = Singleton.GetCurrentMinigame().Models.Count - 1;
            // not move
        }
        else
        {
            currentItem++;
            ShowOptionById(currentItem);
        }
    }
    public void OnLeft()
    {
        lastItem = currentItem;
        currentItem--;
        if (currentItem == -1)
        {
            currentItem = 0;
        }
        ShowOptionById(currentItem);

    }
    public void SelectOption(int value)
    {
        Singleton.GetCurrentMinigame().Models[currentItem].Question.OptionSelected = value;

    }
    public void ShowFeedback()
    {
        ShowFeedbackItem(currentItem);
    }
    public void ShowFeedbackItem(int index)
    {

        Question question = Singleton.GetCurrentMinigame().Models[index].Question;
        optionUis[0].Label.color = Color.black;
        optionUis[1].Label.color = Color.black;
        optionUis[2].Label.color = Color.black;
        optionUis[3].Label.color = Color.black;
        if (question.OptionSelected == question.Solution)
        {
            optionUis[question.OptionSelected].Label.color = Color.green;
        }
        else
        {
            optionUis[question.OptionSelected].Label.color = Color.red;
            optionUis[question.Solution].Label.color = Color.green;
        }

    }


}