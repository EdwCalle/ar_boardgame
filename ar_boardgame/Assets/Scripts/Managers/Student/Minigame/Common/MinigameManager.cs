using Models;
using Photon.Pun;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class MinigameManager : MonoBehaviourPunCallbacks
{
    //Load Info Manager include title

    public LoadingScreenManager loadingScreenManager;
    public NormalMinigameManager normalMinigameManager;
    public SearchMinigameManager searchMinigameManager;
    public QuizMinigameManager quizMinigameManager;
    //Countdown Manager
    public CountdownTimer countdownTimer;
    //items manager
    public Models3DManager models3DManager;
    // DetectPlane Manager
    public DetectPlane detectPlane;
    // Result Manager
    public ResultsManager resultsManager;
    IMinigame minigame;
    private int playersReady;

    IMinigame GetTypeMinigame()
    {
        if (Singleton.GetCurrentMinigame().Level == 1)
            return minigame = normalMinigameManager;
        if (Singleton.GetCurrentMinigame().Level == 2)
            return minigame = searchMinigameManager;
        if (Singleton.GetCurrentMinigame().Level == 3)
            return minigame = quizMinigameManager;
        throw new System.Exception("ThisApp: No type of level has been loaded."); ;
    }
    async void Start()
    {
        // Singleton.AddRound();
        // Singleton.AddRound();

        //set loading screen manager animation
        loadingScreenManager.StartAnimation("Cargando Minijuego, prepara tu cámara");
        //Hide all components
        loadingScreenManager.titleMinigame.SetActive(false);
        normalMinigameManager.gameObject.SetActive(false);
        searchMinigameManager.gameObject.SetActive(false);
        quizMinigameManager.gameObject.SetActive(false);
        models3DManager.gameObject.SetActive(false);
        resultsManager.gameObject.SetActive(false);
        //load mimigame
        minigame = GetTypeMinigame();
        // load common info, but not models 
        loadingScreenManager.LoadLevel();
        // load info for each component
        minigame.LoadLevel();
        // Load Models 3D, not show them
        await models3DManager.LoadLevel();
        // loadLevel minigame in countdownTimer
        countdownTimer.LoadLevel();
        if (Singleton.IsSinglePlayer())
        {
            StartLevelRPC();
        }
        else
        {
            // PhotonNetwork.AutomaticallySyncScene = true;
            StartLevelRPC();
        }
        //start level

    }

    [PunRPC]
    void WaitingPlayersRPC()
    {
        playersReady++;
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            photonView.RPC("StartLevelRPC", RpcTarget.All);
        }

        // CheckAllPlayersLoadedScene();
    }
    [PunRPC]
    void StartLevelRPC()
    {
        loadingScreenManager.StopAnimation();
        countdownTimer.StartMinigame();
    }
    public void StartResolveTime()
    {
        //show title
        loadingScreenManager.StartLevel();
        // show minigame Info
        minigame.StartLevel();
        // show models
        models3DManager.ActivateModels3D();
    }
    public void SetScore(Score myPartialScore)
    {
        if (Singleton.IsSinglePlayer())
        {

        }
        else
        {
            ScoreManager.SetPartialScore(PhotonNetwork.LocalPlayer, myPartialScore);
        }
    }
    public void FinishGame()
    {
        Singleton.AddRound();
        if (Singleton.IsSinglePlayer())
        {
            SceneManager.LoadScene("board");
        }
        else
        {
            PhotonNetwork.LoadLevel("board");
        }
    }


}
