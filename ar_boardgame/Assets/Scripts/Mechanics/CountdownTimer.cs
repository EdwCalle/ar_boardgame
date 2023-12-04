
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;
using System.Collections;


public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private MediatorMinigame mediatorMinigame;
    private float interactionTime; // Total time for the countdown in seconds
    private float resolveTime;  // Total time for the countdown in seconds
    private float feedbackTime;  // Total time for the countdown in seconds
    private float currentTime;
    public float InteractionTime { get => interactionTime; set => interactionTime = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public float ResolveTime { get => resolveTime; set => resolveTime = value; }
    public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }

    float offSet = 3f;
    public Texting messageState;
    public Texting textTime;

    void Start()
    {

    }
    IEnumerator MinigameQueqe()
    {

        ChangeState(true, TimerState.offset, offSet, "Enfoca la cámara y exploremos el nivel");
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(false, TimerState.interaction, interactionTime);
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(true, TimerState.offset, offSet, "Hora de Resolver el Juego!!!!!!!");
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(false, TimerState.resolve, resolveTime);
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(true, TimerState.offset, offSet, "Se acabó el tiempo, pero te dejo las respuestas");
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(false, TimerState.feedback, feedbackTime);
        //SHOW Anwser
        ItemsManager.ShowFeedback();
        // ItemsManager.ShowFeedback();
        // show footer-finish
        mediatorMinigame.ActivateFinish(true);
        yield return StartCoroutine(CountownTimer(currentTime));
        Singleton.instance.currentMinigame.TimerState = TimerState.none;
        mediatorMinigame.WaitingFinishGame();


    }
    IEnumerator CountownTimer(float elapsedTime)
    {
        while (elapsedTime >= 0)
        {
            textTime.Label.text = elapsedTime.ToString("0");
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
    }
    void Update()
    {

    }

    void EnableMessageState(bool enable)
    {
        messageState.Container.SetActive(enable);
        textTime.Container.SetActive(!enable);

    }
    void ChangeState(bool enable, TimerState timerState, float currentTime, string message = null)
    {
        EnableMessageState(enable);
        Singleton.instance.currentMinigame.TimerState = timerState;
        this.currentTime = currentTime;
        if (message != null)
            SetMessage(message);

    }
    void SetMessage(string text)
    {
        messageState.Label.text = text;
    }
    public void GoToMinigame(string codeMinigame)
    {
        Debug.Log("go to minigame:" + codeMinigame);
        SceneManager.LoadScene(codeMinigame);
    }

    public void BuildTimer(float interactionTime, float resolveTime, float feedbackTime)
    {
        this.interactionTime = interactionTime;
        this.resolveTime = resolveTime;
        this.feedbackTime = feedbackTime;
    }
    public void PrepareLevel()
    {
        BuildTimer(Singleton.instance.currentMinigame.InteractionTime,
                   Singleton.instance.currentMinigame.ResolveTime,
                   Singleton.instance.currentMinigame.FeedbackTime);
        ChangeState(true, TimerState.offset, offSet, "Enfoca la cámara y exploremos el nivel");
        // mediatorMinigame.ActivateCamera();
        StartCoroutine(MinigameQueqe());
    }

}
[System.Serializable]
public class Texting
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI label;

    // [SerializeField] private TextMeshProUGUI testing;
    public GameObject Container { get => container; set => container = value; }
    public TextMeshProUGUI Label { get => label; set => label = value; }
    //    public TextMeshProUGUI Testing { get => testing; set => testing = value; }
}