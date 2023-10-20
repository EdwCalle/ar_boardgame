using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CountdownTimer : MonoBehaviour
{
    private float interactionTime; // Total time for the countdown in seconds
    private float resolveTime;  // Total time for the countdown in seconds
    private float feedbackTime;  // Total time for the countdown in seconds
    private float currentTime;
    //public GameObject time;
    //public TextMeshProUGUI timerText; // Reference to a UI Text component to display the timer
    private bool isCountingDown = false; // Start the countdown when true
    public GameObject countdownFinishedObject; // GameObject to activate when the countdown finishes

    public float InteractionTime { get => interactionTime; set => interactionTime = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public float ResolveTime { get => resolveTime; set => resolveTime = value; }
    public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }

    float offSet = 4f;
    int sequence = 6;
    public Texting messageState;
    public Texting time;

    //public bool showResults; //encapusule this method

    private void Start()
    {
        //  CurrentTime = TotalTime;

    }

    private void Update()
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
                {
                    EnableMessageState(false);
                    currentTime = interactionTime;
                    //Activate resolve mode
                }
                if (sequence == 5)
                {
                    currentTime = offSet;
                    EnableMessageState(true);
                    SetMessage("Hora de Resolver el Juego!!!!!!!");
                    //Activate offset mode
                }

                if (sequence == 4)
                {
                    EnableMessageState(false);
                    currentTime = resolveTime;
                    //Activate resolve mode
                }
                if (sequence == 3)
                {
                    currentTime = offSet;
                    EnableMessageState(true);
                    SetMessage("Se acabó el tiempo, pero te dejo las respuestas ;)");
                }
                if (sequence == 2)
                {
                    EnableMessageState(false);
                    currentTime = feedbackTime;
                    //Activate resolve mode
                }
                if (sequence == 1)
                {
                    currentTime = 0;
                    isCountingDown = false;
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

        currentTime = offSet;
        EnableMessageState(true);
        SetMessage("Enfoca la cámara y exploremos el nivel");
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