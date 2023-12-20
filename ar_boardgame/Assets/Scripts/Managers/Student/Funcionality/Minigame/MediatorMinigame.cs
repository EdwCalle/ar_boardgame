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
    [SerializeField] private ExtraOneManager extraOneItem;



    // private DetectPlane;
    //[SerializeField] private CountdownTimer countdownTimer;

    int playersReady = 0;

    public ItemsManager ItemsManager { get => itemsManager; set => itemsManager = value; }

    // Start is called before the first frame update
    async void Start()
    {
        //Singleton.instance.boardPersistent.CurrentRound = 2;
        // Singleton.instance.SetMinigame();
        if (!Singleton.instance.IsSinglePlayerMode)
            PhotonNetwork.AutomaticallySyncScene = true;
        // setting level from here
        // level 1
        if (Singleton.GetCurrentMinigame().Level == 1)
            Singleton.GetCurrentMinigame().ConfigureLevel(HeaderType.extraInfo, SelectionType.MultipleSelection, ExtraComponentType.normal);
        // level 2
        if (Singleton.GetCurrentMinigame().Level == 2)
            Singleton.GetCurrentMinigame().ConfigureLevel(HeaderType.normal, SelectionType.OneTimeSelection, ExtraComponentType.explore);
        // level 3
        if (Singleton.GetCurrentMinigame().Level == 3)
            Singleton.GetCurrentMinigame().ConfigureLevel(HeaderType.questions, SelectionType.noSelection, ExtraComponentType.options);


        // disable ar session

        // show resume
        loadingScreenManager.StartAnimation("Cargando Minijuego, prepara tu cámara");
        /// headerManager.Configure(this); is not necessary right now
        // prepara el header, no olvidar implementar personalizadamente

        // prepare items
        await itemsManager.PrepareLevel();

        if (Singleton.instance.IsSinglePlayerMode)
        {
            StartGame();
        }
        else
        {
            photonView.RPC("WaitPlayersRPC", RpcTarget.All);
        }
        Debug.Log("finish start");
    }
    [PunRPC]
    void WaitPlayersRPC()
    {
        playersReady++;
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("StartGame", RpcTarget.All);
        }

        // CheckAllPlayersLoadedScene();
    }
    [PunRPC]
    void StartGame()
    {

        headerManager.PrepareLevel();
        //
        //prepare extra item
        extraOneItem.PrepareLevel();
        //prepare footer
        //   footerManager.PrepareLevel();
        // prepare timer

        // disable resume
        loadingScreenManager.StopAnimation();
        // show ar session
        // start timer
        timerManager.PrepareLevel();
        timerManager.StartMinigame();
        //ActivateCamera();
        // Debug.Log("preparing xd");
        playersReady = 0;
    }
    //from timer
    public void StartHeadear()
    {
        headerManager.ActivateHeader();
        extraOneItem.ActiveListFound(true);
    }
    // fromn timer
    public void StartMinigame()
    {
        itemsManager.ActivateItems();
        footerManager.showOptions();

    }
    // fromn timer
    public void StopMinigame()
    {
        //footerManager.ActivateFinish(active);
        ItemsManager.ShowFeedback();
        footerManager.ShowFeedback();
        footerManager.ShowResults();
        extraOneItem.ActiveListFound(false);
    }

    public void WaitingFinishGame()
    {

        loadingScreenManager.StartAnimation("Ingresando al tablero, esperando al resto de jugadores......");
        Score myPartialScore = ScoreManager.GetMinigameResults(Singleton.GetCurrentMinigame());
        if (!Singleton.instance.IsSinglePlayerMode)
        {
            ScoreManager.SetPartialScore(PhotonNetwork.LocalPlayer, myPartialScore);
            Singleton.instance.boardPersistent.CurrentRound++;
            //  Debug.Log("una ves ya pues");
            photonView.RPC("FinishGame", RpcTarget.All);
        }
    }
    [PunRPC]
    void FinishGame()
    {
        playersReady++;

        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("board");
        }
    }
    // public List<GameObject> GetItems()
    // {
    //     return itemsManager.items;
    // }
    // for refactoring
    public void FoundItem(int index)
    {
        extraOneItem.CheckItem(index);
    }
    //right now methods are static, so  mediator pattern design  doesn't applies totally

    // Update is called once per frame
    void Update()
    {

    }
}
