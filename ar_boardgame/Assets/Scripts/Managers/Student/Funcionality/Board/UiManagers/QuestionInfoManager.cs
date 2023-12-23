using System.Threading.Tasks;
using thisAPP.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionInfoManager : MonoBehaviour
{
    public QuestionsInfo uiElements;
    public void PrepareQuestion(bool IsMyTurn, int currentPlayer)
    {
        EnableOptions(IsMyTurn);
        SetQuestion(currentPlayer);
    }
    void EnableOptions(bool value)
    {
        uiElements.Options[0].GetComponent<Button>().interactable = value;
        uiElements.Options[1].GetComponent<Button>().interactable = value;
        uiElements.Options[2].GetComponent<Button>().interactable = value;
        uiElements.Options[3].GetComponent<Button>().interactable = value;
    }
    public void SetQuestion(int currentPlayer)
    {

        uiElements.QuestionTitle.text = Singleton.GetQuestionRound(currentPlayer).Title;
        SetOption(0, Singleton.GetQuestionRound(currentPlayer).OptionA);
        SetOption(1, Singleton.GetQuestionRound(currentPlayer).OptionB);
        SetOption(2, Singleton.GetQuestionRound(currentPlayer).OptionC);
        SetOption(3, Singleton.GetQuestionRound(currentPlayer).OptionD);

    }
    public void SetOption(int pos, string value)
    {
        uiElements.Options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public async Task SelectOption(int value, int currentPlayer)
    {
        int solution = Singleton.GetQuestionRound(currentPlayer).Solution;
        if (value == solution)
        {

            uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        else
        {
            uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", -1);
            uiElements.Options[solution].GetComponent<Animator>().SetInteger("optionState", 1);
        }
        EnableOptions(false);
        await Task.Delay(2000);
        uiElements.Options[value].GetComponent<Animator>().SetInteger("optionState", 0);
        uiElements.Options[solution].GetComponent<Animator>().SetInteger("optionState", 0);

    }

}
