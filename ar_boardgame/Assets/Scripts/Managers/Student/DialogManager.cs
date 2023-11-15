
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
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

        question.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.Title;
        SetOption(0, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForA.OptionA);
        SetOption(1, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForB.OptionA);
        SetOption(2, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForC.OptionA);
        SetOption(3, Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].QuestionForD.OptionA);
    }
    public void SetOption(int pos, string value)
    {
        options[pos].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value;
    }
    public void SelectOption(int value)
    {
        HideAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
