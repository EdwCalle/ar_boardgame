using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    private int playersReady = 0;
    public GameObject canvasModal;
    public BoardManager boardManager;

    public WaitPlayerManager waitPlayerManager;
    public DialogueManager dialogueManager;
    public DiceManager diceManager;
    public ArrowsManager arrowsManager;
    public QuestionInfoManager questionInfoManager;
    public PlayersInfoManager playersInfoManager;
    public MinigameInfoManager minigameInfoManager;
    void Awake()
    {
        arrowsManager.uiElements.ModalUI.SetActive(false);
        dialogueManager.uiElements.ModalUI.SetActive(false);
        diceManager.uiElements.ModalUI.SetActive(false);
        questionInfoManager.uiElements.ModalUI.SetActive(false);
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
            photonView.RPC("RollDiceRPC", RpcTarget.All, randomDice);
    }
    [PunRPC]
    public void RollDiceRPC(int randomDice)
    {
        print("yo soy el que me imprimi varias veces");
        StartCoroutine(diceManager.RotateDiceTime(randomDice));
    }
    public void ThrowDice(int randomDice)
    {
        if (Singleton.IsSinglePlayer())
            ThrowDiceRPC(randomDice);
        else
            photonView.RPC("ThrowDiceRPC", RpcTarget.All, randomDice);
        ThrowDiceRPC(randomDice);
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
    public void SelectOption(int value)
    {
        if (Singleton.IsSinglePlayer())
            SelectOptionRPC(value);
        else
            photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
    }
    [PunRPC]
    public async void SelectOptionRPC(int value)
    {
        await questionInfoManager.CheckOption(value);
        await boardManager.FinishTurn();
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
    // public async void ShowQuestions()
    // {
    //     await questionInfoManager.ShowModal(canvasModal);
    // }
    // Update is called once per frame
    void Update()
    {

    }
}
