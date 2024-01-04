using System.Threading.Tasks;
using ExitGames.Client.Photon;
using Models;
using Photon.Pun;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks
{
    private int playersReady = 0;
    public GameObject canvasModal;
    public BoardManager boardManager;
    public TurnInfoManager turnInfoManager;

    public WaitPlayerManager waitPlayerManager;
    public DialogueManager dialogueManager;
    public DiceManager diceManager;

    public ArrowsManager arrowsManager;

    public QuestionInfoManager questionInfoManager;
    public RandomSquareManager randomSquareManager;
    public PlayersInfoManager playersInfoManager;
    public MinigameInfoManager minigameInfoManager;

    void Awake()
    {
        arrowsManager.uiElements.ModalUI.SetActive(false);
        dialogueManager.uiElements.ModalUI.SetActive(false);
        diceManager.uiElements.ModalUI.SetActive(false);
        turnInfoManager.uiElements.ModalUI.SetActive(false);
        questionInfoManager.uiElements.ModalUI.SetActive(false);
        randomSquareManager.uiElements.ModalUI.SetActive(false);
        minigameInfoManager.uiElements.ModalUI.SetActive(false);
        // resultsInfoManager.uiElements.ModalUI.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.IsSinglePlayer())
        {
            PrepareGameRPC();
        }
        else
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
            {
                Hashtable playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
                print("turno" + playerProperties["turn"]);
                Singleton.SetMyTurn((int)playerProperties["turn"]);

                // for sync scenes to other players
                PhotonNetwork.AutomaticallySyncScene = true;
                // WaitPlayers("PrepareGameRPC");
                photonView.RPC("WaitPlayersRPC", RpcTarget.All, "PrepareGameRPC");
                // photonView.RPC("WaitPlayersRPC", RpcTarget.All);
                // Debug.Log("no se lo que ");
            }
        }
    }
    public async Task WaitPlayer(string nameRPC)
    {
        await waitPlayerManager.ShowModal(canvasModal);
        photonView.RPC("WaitPlayersRPC", RpcTarget.All, nameRPC);
    }
    [PunRPC]
    void WaitPlayersRPC(string NameRPC)
    {
        //Debug.Log("pueda que si ");
        playersReady++;
        if (playersReady == Singleton.GetNumberPlayers() && photonView.IsMine)
        {
            photonView.RPC(NameRPC, RpcTarget.All);
        }
    }
    [PunRPC]
    async void PrepareGameRPC()
    {
        ResetPlayersReady();
        if (Singleton.ExistRounds())
        {
            boardManager.PrepareBoard();
            //uiMediatorManager.SetRoundInfo();// change way
            // UpdatePlayersList(); // change way
            await waitPlayerManager.HideModal(canvasModal);
            // if is new round show partial results
            if (Singleton.IsNotFirstRound())
            {
                //  ShowPartialResults();// change way
            }
            await dialogueManager.ShowModal(canvasModal);
            //  UImediatorManager.ShowDialogue();  //  ShowPartialResults();// change way
        }
        else
        {
            //UImediatorManager.HideWaitBoard();// change way
            //caculate winner
            //UImediatorManager.ShowResultsInfo();// change way
        }
    }
    [PunRPC]
    async void ShowDiceRPC()
    {
        ResetPlayersReady();
        await waitPlayerManager.HideModal(canvasModal);
        await diceManager.ShowModal(canvasModal);
    }
    public void RollDice(int randomDice)
    {
        if (Singleton.IsSinglePlayer())
            RollDiceRPC(randomDice);
        else
        {
            photonView.RPC("RollDiceRPC", RpcTarget.All, randomDice);
            //  photonView.RPC("RollDiceRPC", RpcTarget.All, randomDice);
        }

    }
    [PunRPC]
    public void RollDiceRPC(int randomDice)
    {
        //        print("yo soy el que me imprimi varias veces");
        StartCoroutine(diceManager.RotateDiceTime(randomDice));
    }
    public void ThrowDice(int randomDice)
    {
        ThrowDiceRPC(randomDice);
        // if (Singleton.IsSinglePlayer())
        //     ThrowDiceRPC(randomDice);
        // else
        //     photonView.RPC("ThrowDiceRPC", RpcTarget.All, randomDice);
    }
    [PunRPC]
    public async void ThrowDiceRPC(int randomDice)
    {
        await diceManager.HideModal(canvasModal);
        boardManager.ThrowDice(randomDice);
    }
    public void ChangeDirection(string direction)
    {
        if (Singleton.IsSinglePlayer())
            ChangeDirectionRPC(direction);
        else
            photonView.RPC("ChangeDirectionRPC", RpcTarget.All, direction);
    }
    [PunRPC]
    public async void ChangeDirectionRPC(string direction)
    {
        await boardManager.ChangeDirection(direction);
    }
    public void SelectOption(int value, bool solution, SquareType squareType)
    {
        if (Singleton.IsSinglePlayer())
            SelectOptionRPC(value);
        else
        {
            if (!solution && squareType == SquareType.demon)
                ScoreManager.DeleteExtraCoins(PhotonNetwork.LocalPlayer);
            if (solution && squareType == SquareType.demon)
                ScoreManager.AddExtraCoins(PhotonNetwork.LocalPlayer, 2);
            if (solution && squareType == SquareType.angel)
                ScoreManager.AddExtraCoins(PhotonNetwork.LocalPlayer, 1);
            if (solution && squareType == SquareType.normal)
                ScoreManager.AddStart(PhotonNetwork.LocalPlayer);
            photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
        }

    }
    [PunRPC]
    public async void SelectOptionRPC(int value)
    {
        await questionInfoManager.CheckOption(value);
    }
    public void FromMyTurn()
    {
        photonView.RPC("FromMyTurnRPC", RpcTarget.All);
    }

    [PunRPC]
    public void FromMyTurnRPC()
    {
        questionInfoManager.FromMyTurn();
    }
    void ResetPlayersReady()
    {
        playersReady = 0;
    }
    public async void ShowArrows(string DirectionOne, string DirectionTwo)
    {
        arrowsManager.ActivateArrows(DirectionOne, DirectionTwo);
        await arrowsManager.ShowModal(canvasModal);
    }
    public void ShowQuestions(SquareType squareType)
    {
        if (Singleton.IsMyTurn())
        {
            int RandomNumber = Singleton.GetRadomNumberQuestion(squareType);
            photonView.RPC("ShowQuestionsRPC", RpcTarget.All, squareType, RandomNumber);
        }

    }
    [PunRPC]
    async void ShowQuestionsRPC(SquareType squareType, int RandomNumber)
    {
        await questionInfoManager.PrepareModal(squareType, RandomNumber);
    }
    public void ShowRandomQuestion()
    {
        if (Singleton.IsMyTurn())
        {
            int RandomNumber = Random.Range(0, 3);
            if (RandomNumber == 0)
                ScoreManager.AddExtraCoins(PhotonNetwork.LocalPlayer, 1);
            if (RandomNumber == 1)
                ScoreManager.AddExtraCoins(PhotonNetwork.LocalPlayer, 2);
            if (RandomNumber == 2)
                ScoreManager.DeleteExtraCoins(PhotonNetwork.LocalPlayer);
            photonView.RPC("ShowRandomQuestionRPC", RpcTarget.All, RandomNumber);
        }

    }
    [PunRPC]
    void ShowRandomQuestionRPC(int RandomNumber)
    {
        randomSquareManager.PrepareRandomSquare(RandomNumber);
    }
    //Update is called once per frame
    void Update()
    {

    }
}
