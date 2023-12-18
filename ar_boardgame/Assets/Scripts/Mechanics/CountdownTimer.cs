
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

    float offSet = 4f;
    public Texting messageState;
    public Texting textTime;
    public bool gameStart;
    public bool gameFinish;

    public DetectPlane detectPlane;

    public GameObject scene;
    void Start()
    {

    }
    IEnumerator MinigameQueqe()
    {
        ChangeState(true, TimerState.offset, offSet, "Enfoca tus alrededores para dectectar el escenario");
        yield return StartCoroutine(CountownTimer(currentTime));

        ChangeState(false, TimerState.offset, 6f);//6
        detectPlane.EnablePlaneDetection(true);
        yield return StartCoroutine(CountownTimer(currentTime));

        detectPlane.EnablePlaneDetection(false);
        scene.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        ChangeState(true, TimerState.offset, offSet, "Ahora ubica el escenario acorde a tu espacio");
        yield return StartCoroutine(CountownTimer(currentTime));

        detectPlane.EnableTap(true);
        ChangeState(false, TimerState.offset, 15f);//15
        yield return StartCoroutine(CountownTimer(currentTime));


        detectPlane.EnableTap(false);
        mediatorMinigame.StartMinigame();
        // ChangeState(true, TimerState.offset, offSet, "Enfoca la cámara y exploremos el nivel");
        // yield return StartCoroutine(CountownTimer(currentTime));

        // ChangeState(false, TimerState.interaction, interactionTime);
        // yield return StartCoroutine(CountownTimer(currentTime));

        ChangeState(true, TimerState.offset, offSet, "Hora de Resolver el Juego!!!!!!!");
        yield return StartCoroutine(CountownTimer(currentTime));

        mediatorMinigame.StartHeadear();
        ChangeState(false, TimerState.resolve, resolveTime);
        yield return StartCoroutine(CountownTimer(currentTime));
        ChangeState(true, TimerState.offset, offSet, "Se acabó el tiempo, pero te dejo las respuestas");
        yield return StartCoroutine(CountownTimer(currentTime));


        mediatorMinigame.StopMinigame();
        ChangeState(false, TimerState.feedback, feedbackTime);
        yield return StartCoroutine(CountownTimer(currentTime));

        Singleton.instance.currentMinigame.TimerState = TimerState.none;
        //gameFinish = true;
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
        if (gameStart)
        {
            StartCoroutine(MinigameQueqe());
            gameStart = false;
        }
        if (gameFinish)
        {
            mediatorMinigame.WaitingFinishGame();
            gameFinish = false;
        }
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
        //  gameStart = true;

    }
    public void StartMinigame()
    {
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