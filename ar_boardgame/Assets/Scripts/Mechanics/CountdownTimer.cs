
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;
using Photon.Pun;


public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private MediatorMinigame mediatorMinigame;
    private float interactionTime; // Total time for the countdown in seconds
    private float resolveTime;  // Total time for the countdown in seconds
    private float feedbackTime;  // Total time for the countdown in seconds
    private float currentTime;
    //public GameObject time;
    //public TextMeshProUGUI timerText; // Reference to a UI Text component to display the timer
    private bool isCountingDown = false; // Start the countdown when true
    public float InteractionTime { get => interactionTime; set => interactionTime = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public float ResolveTime { get => resolveTime; set => resolveTime = value; }
    public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }

    float offSet = 4f;
    int sequence = 6;
    public Texting messageState;
    public Texting time;
    private int playersReady = 0;

    //public bool showResults; //encapusule this method

    void Start()
    {
        // if (!PhotonNetwork.IsMasterClient)
        //     offSet = 1;
        //  CurrentTime = TotalTime;

    }
    void Update()
    {
        if (isCountingDown)
        {
            CurrentTime -= Time.deltaTime;

            // Update the UI text to display the countdown
            if (time.Label != null)
            {
                time.Label.text = CurrentTime.ToString("0");
            }
            // Check if the countdown has finished
            if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                // Trigger an event or do something when the countdown finishes
                // if (countdownFinishedObject != null)
                // {
                //     countdownFinishedObject.SetActive(true);
                // }
                if (sequence == 6)
                    ChangeState(false, TimerState.interaction, interactionTime);
                //Activate resolve mode
                if (sequence == 5)
                    ChangeState(true, TimerState.offset, offSet, "Hora de Resolver el Juego!!!!!!!");
                //Activate offset mode
                if (sequence == 4)
                {
                    ChangeState(false, TimerState.resolve, resolveTime);
                    //activate selectable mode
                    ItemsManager.ActiveSelectableComponent(true);
                }
                //Activate resolve mode
                if (sequence == 3)
                {
                    ChangeState(true, TimerState.offset, offSet, "Se acabó el tiempo, pero te dejo las respuestas");

                }

                if (sequence == 2)
                {
                    ChangeState(false, TimerState.feedback, feedbackTime);
                    //SHOW Anwser
                    ItemsManager.ShowFeedback();
                    // show footer-finish
                    mediatorMinigame.ActivateFinish(true);
                }

                //Activate resolve mode
                if (sequence == 1)
                {
                    currentTime = 0;
                    isCountingDown = false;
                    Singleton.instance.currentMinigame.TimerState = TimerState.none;
                    mediatorMinigame.WaitingFinishGame();
                    // if (PhotonNetwork.IsMasterClient)

                    //     PhotonNetwork.LoadLevel("board");
                    //Activate resolve mode
                }

                sequence--;
            }
        }
    }

    void EnableMessageState(bool enable)
    {
        messageState.Container.SetActive(enable);
        time.Container.SetActive(!enable);

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
    // Start the countdown
    public void StartCountdown()
    {
        isCountingDown = true;
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
        StartCountdown();
    }

}
[System.Serializable]
public class Texting
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI label;

    public GameObject Container { get => container; set => container = value; }
    public TextMeshProUGUI Label { get => label; set => label = value; }
}