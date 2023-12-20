using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FooterManager : MonoBehaviour
{
    [System.Serializable]
    public class Texting
    {
        [SerializeField] private GameObject container;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI label2;

        // [SerializeField] private TextMeshProUGUI testing;
        public GameObject Container { get => container; set => container = value; }
        public TextMeshProUGUI Label { get => label; set => label = value; }
        public TextMeshProUGUI Label2 { get => label2; set => label2 = value; }
        //    public TextMeshProUGUI Testing { get => testing; set => testing = value; }
    }
    [System.Serializable]
    public class OptionUi
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Toggle toggle;
        public TextMeshProUGUI Label { get => label; set => label = value; }
        public Toggle Toggle { get => toggle; set => toggle = value; }

        public GameObject modal;

    }

    public Button finish;
    public Texting finalMessage;
    public Texting Result;
    public OptionUi[] optionUis = new OptionUi[4];
    public GameObject optionsModal;
    public TextMeshProUGUI questiontitle;
    public TextMeshProUGUI info;
    public int lastIndex = -1;
    public bool feedback;
    public int CurrentQuestion = 0;
    [SerializeField] private MediatorMinigame mediatorMinigame;


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene()
    {
        Singleton.instance.boardPersistent.CurrentRound++;
        SceneManager.LoadScene("board");
    }
    public void showOptions()
    {
        if (Singleton.GetCurrentMinigame().ExtraComponentType == ExtraComponentType.options)
        {
            optionsModal.SetActive(true);
            ShowOptionById(0);
        }


    }
    public void OnRight()
    {
        lastIndex = CurrentQuestion;

        if (CurrentQuestion == Singleton.GetCurrentMinigame().Models.Count - 1)
        {
            CurrentQuestion = Singleton.GetCurrentMinigame().Models.Count - 1;
            // not move
        }
        else
        {
            CurrentQuestion++;
            ShowOptionById(CurrentQuestion);
        }


    }
    public void OnLeft()
    {
        lastIndex = CurrentQuestion;
        CurrentQuestion--;
        if (CurrentQuestion == -1)
        {
            CurrentQuestion = 0;
        }
        ShowOptionById(CurrentQuestion);

    }
    public void SelectOption(int value)
    {
        Singleton.GetCurrentMinigame().Models[CurrentQuestion].Question.IsSelected = value;
    }
    public void ShowResults()
    {
        //optionsModal.SetActive(false);
        Result.Container.SetActive(true);
        Score currentMinigameScore = ScoreManager.GetMinigameResults(Singleton.GetCurrentMinigame());
        Result.Label.text = $"Ganaste {currentMinigameScore.Stars}";
        Result.Label2.text = $"de {Singleton.GetCurrentMinigame().Models.Count} posibles";
    }
    public void ShowOptionById(int index)
    {
        if (lastIndex != -1)
            mediatorMinigame.ItemsManager.Items[lastIndex].SetActive(false);
        mediatorMinigame.ItemsManager.Items[index].SetActive(true);
        info.text = $"{index + 1}/{Singleton.GetCurrentMinigame().Models.Count}";
        Question question = Singleton.GetCurrentMinigame().Models[index].Question;
        questiontitle.text = question.Title;
        optionUis[0].Label.text = question.OptionA;
        optionUis[1].Label.text = question.OptionB;
        optionUis[2].Label.text = question.OptionC;
        optionUis[3].Label.text = question.OptionD;
        SetSelected(question.IsSelected);
        if (feedback)
            ShowFeedbackItem(index);
    }
    public void ShowFeedback()
    {
        feedback = true;
        ShowFeedbackItem(CurrentQuestion);
    }
    public void ShowFeedbackItem(int index)
    {

        Question question = Singleton.GetCurrentMinigame().Models[index].Question;
        optionUis[0].Label.color = Color.black;
        optionUis[1].Label.color = Color.black;
        optionUis[2].Label.color = Color.black;
        optionUis[3].Label.color = Color.black;
        if (question.IsSelected == question.Solution)
        {
            optionUis[question.IsSelected].Label.color = Color.green;
        }
        else
        {
            optionUis[question.IsSelected].Label.color = Color.red;
            optionUis[question.Solution].Label.color = Color.green;
        }

    }


    public void SetSelected(int selected)
    {
        optionUis[selected].Toggle.isOn = true;
    }
}
