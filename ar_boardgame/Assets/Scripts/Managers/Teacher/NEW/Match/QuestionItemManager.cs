using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionItemManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Toggle toggle;
    public QuestionCrudManager questionCrudManager;
    Question question;
    public void LoadData(Question question, bool isOn, QuestionCrudManager questionCrudManager)
    {
        title.text = $"CODE{question.Id}: {question.Title}";
        this.question = question;
        SelectToggleWithoutEvent(isOn);
        this.questionCrudManager = questionCrudManager;

    }
    void SelectToggleWithoutEvent(bool value)
    {
        // Temporarily remove the listener
        toggle.onValueChanged.RemoveListener(OnToggleQuestion);
        // Set the toggle's state without triggering the event
        toggle.isOn = value;
        // Re-add the listener
        toggle.onValueChanged.AddListener(OnToggleQuestion);
    }
    void OnToggleQuestion(bool isOn)
    {
        // Your logic here
        if (isOn)
        {
            questionCrudManager.SelectedQuestions.Add(question);
        }
        else
        {
            questionCrudManager.SelectedQuestions.Remove(question);
        }
    }
}
