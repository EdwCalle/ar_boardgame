

using ExitGames.Client.Photon;
using Models;
using Photon.Pun;
using UnityEngine;

public class MediatorMinigame : MonoBehaviourPunCallbacks
{
    [SerializeField] private HeaderManager headerManager;
    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private LoadingScreenManager loadingScreenManager;

    [SerializeField] private CountdownTimer timerManager;
    [SerializeField] private FooterManager footerManager;
    [SerializeField] private ItemFinderManager itemFinderManager;



    // private DetectPlane;
    //[SerializeField] private CountdownTimer countdownTimer;

    int playersReady = 0;

    public ItemsManager ItemsManager { get => itemsManager; set => itemsManager = value; }

    // Start is called before the first frame update
    async void Start()
    {

        if (!Singleton.instance.IsSinglePlayerMode)
            PhotonNetwork.AutomaticallySyncScene = true;
        // setting level from here
        // level 1
        if (Singleton.instance.currentMinigame.Level == 1)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.extraInfo, SelectionType.MultipleSelection, ExtraComponentType.normal);
        // level 2
        if (Singleton.instance.currentMinigame.Level == 2)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.normal, SelectionType.OneTimeSelection, ExtraComponentType.explore);
        // level 3
        if (Singleton.instance.currentMinigame.Level == 3)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.questions, SelectionType.noSelection, ExtraComponentType.options);


        // disable ar session

        // show resume
        loadingScreenManager.StartAnimation("Cargando Minijuego, prepara tu cámara");
        /// headerManager.Configure(this); is not necessary right now
        // prepara el header, no olvidar implementar personalizadamente
        headerManager.PrepareLevel();
        itemFinderManager.SetItems();
        // prepare items
        await itemsManager.PrepareLevel();

        if (Singleton.instance.IsSinglePlayerMode)
        {
            StartGame();
        }
        else
        {
            photonView.RPC("PlayerLoadedScene", RpcTarget.All);
        }


        // //prepare extra item

        // //prepare footer
        // footerManager.PrepareLevel();
        // // prepare timer

        // // disable resume
        // loadingScreenManager.StopAnimation();
        // // show ar session

        // // start timer
        // timerManager.PrepareLevel();
        Debug.Log("finish start");
    }
    public async void PrepareLevel()
    {
        await itemsManager.PrepareLevel();
    }
    public void ActivateFinish(bool active)
    {
        //footerManager.ActivateFinish(active);
        ItemsManager.ShowFeedback();
        footerManager.ShowFeedback();
        footerManager.ShowResults();

        itemFinderManager.ActiveListFound(false);
    }
    public void PrepareMinigame()
    {

        itemsManager.ActivateItems();
        footerManager.PrepareLevel();

    }
    [PunRPC]
    void PlayerLoadedScene()
    {
        playersReady++;
        CheckAllPlayersLoadedScene();
    }
    void CheckAllPlayersLoadedScene()
    {
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("StartGame", RpcTarget.All);
        }
    }
    [PunRPC]
    void StartGame()
    {
        playersReady = 0;
        //prepare extra item

        //prepare footer
        //   footerManager.PrepareLevel();
        // prepare timer

        // disable resume
        loadingScreenManager.StopAnimation();
        // show ar session
        // start timer
        timerManager.PrepareLevel();
        //ActivateCamera();
        // Debug.Log("preparing xd");
    }
    public void WaitingFinishGame()
    {

        loadingScreenManager.StartAnimation("Ingresando al tablero, esperando al resto de jugadores......");
        Score myPartialScore = ScoreManager.GetMinigameResults(Singleton.instance.currentMinigame);
        if (!Singleton.instance.IsSinglePlayerMode)
        {
            ScoreManager.SetPartialScore(PhotonNetwork.LocalPlayer, myPartialScore);
            //  Debug.Log("una ves ya pues");
            photonView.RPC("PlayerFinishedGame", RpcTarget.All);
        }

        //else


    }
    [PunRPC]
    void PlayerFinishedGame()
    {
        playersReady++;
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            FinishGame();
            // photonView.RPC("FinishGame", RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    void FinishGame()
    {
        Singleton.instance.CurrentRound++;
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("board");
    }
    public void ActivateHeader()
    {
        headerManager.ActivateHeader();
        if (Singleton.instance.currentMinigame.ExtraComponent == ExtraComponentType.explore)
        {
            itemFinderManager.ActiveListFound(true);
        }
    }

    public void FoundItem(int index)
    {
        itemFinderManager.CheckItem(index);
    }
    public void PrepareLevelFooter()
    {
        footerManager.PrepareLevel();
    }


    //right now methods are static, so  mediator pattern design  doesn't applies totally

    // Update is called once per frame
    void Update()
    {

    }
}
