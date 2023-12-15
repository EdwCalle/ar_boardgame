using UnityEngine;
using Models;
using Photon.Pun;
using System.Threading.Tasks;
using Photon.Realtime;
using System.Collections.Generic;
public class BoardManager : MonoBehaviourPunCallbacks
{
    [Header("Board Components")]
    public UIPlayerManager playerUI;
    public GameObject[] players;
    public GameObject[] boxes;

    [Header("Board Funcionality/offline")]
    [Range(0, 500)]
    public float speed;
    public bool isDefaultDice;
    public int defaultDice;
    int numberPlayers;
    public float offsetPlayer = 50f;

    private int currentSquare = 0;
    private bool isMoving = false;
    BoardSquare[] boardSquares = new BoardSquare[56];
    private Vector3 nextPosition;
    private int dice;
    private bool isActiveDice;
    private int currentPlayer = 0;
    private int playersReady = 0;

    public bool skipScene;

    void Start()
    {

        if (Singleton.instance.IsSinglePlayerMode)
        {
            numberPlayers = Singleton.instance.NumberPlayers;
            PrepareGameRPC();
        }
        else
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
            {
                // for sync scenes to other players
                PhotonNetwork.AutomaticallySyncScene = true;
                //get number players
                numberPlayers = Singleton.instance.NumberPlayers;
                photonView.RPC("WaitPlayers", RpcTarget.All);
            }
        }

    }
    [PunRPC]
    void WaitPlayersRPC()
    {
        playersReady++;
        if (playersReady == numberPlayers && photonView.IsMine)
        {
            if (skipScene)
                GoToMinigame();
            else
                photonView.RPC("PrepareGame", RpcTarget.All);
        }

    }
    [PunRPC]
    void PrepareGameRPC()
    {
        if (Singleton.instance.ExistRounds()) // si no es la ultima romda
        {
            // preparing boardsquare order
            PrepareBoxes();
            PreparePlayers();
            // show Round Info
            playerUI.SetRoundInfo();
            UpdatePlayersList(); // make it in player ui
            UpdateTurn(); // make it in player ui
            playerUI.HideWaitPlayers();
            // if is new round show partial results
            if (Singleton.instance.IsNotFirstRound())
            {
                //  ShowPartialResults();
            }
            // show new dialog
            // ShowDialog();
        }
        else
        {
            playerUI.HideWaitPlayers();
            //caculate winner
            playerUI.ShowResultsInfo();
        }
        //  finish game 

    }
    void PrepareBoxes()
    {
        // preparing boardsquare order
        // set next direction of a boardsquare
        for (int i = 0; i < 56; i++)
        { boardSquares[i] = new BoardSquare(i + 1); }
        //set correct pointer
        boardSquares[33].NextSquare = 0;
        boardSquares[43].NextSquare = 21;
        boardSquares[44].NextSquare = 36;
        boardSquares[46].NextSquare = 45;
        boardSquares[50].NextSquare = 52;
        boardSquares[53].NextSquare = 52;
        boardSquares[51].NextSquare = 41;
        boardSquares[55].NextSquare = 19;
        // set wich sqaure are sqarePoint and set the 2 directions to next square, 
        boardSquares[2].setSquarePointPropeties("right", "down", 34, 3);
        boardSquares[9].setSquarePointPropeties("up", "right", 46, 10);
        boardSquares[13].setSquarePointPropeties("up", "right", 53, 15);
        boardSquares[45].setSquarePointPropeties("up", "right", 44, 47);
        boardSquares[52].setSquarePointPropeties("up", "right", 51, 54);
        //set square directions wich ones doeesnt point another direction then down direction
        boardSquares[0].HasAnotherDirection("down");
        boardSquares[6].HasAnotherDirection("right");
        boardSquares[17].HasAnotherDirection("up");
        boardSquares[23].HasAnotherDirection("left");
        boardSquares[36].HasAnotherDirection("right");
        boardSquares[41].HasAnotherDirection("right");
        boardSquares[19].HasAnotherDirection("up");
        boardSquares[21].HasAnotherDirection("up");
    }
    void PreparePlayers()
    {
        for (int i = 0; i < 4; i++)//iterate 4 player
        {
            if (i < numberPlayers)  // if local players are the numberplayer
            {
                int playerBox = Singleton.instance.SquarePlayers[i];
                if (playerBox != 0)// set to their postion when restart scene
                {
                    players[i].transform.localPosition = boxes[playerBox].transform.localPosition;
                    players[i].transform.localPosition += SetOffsetPosition(i);
                }
                //   players[i].GetComponent<PlayerManager>().PrepareData($"Player{i}", i); // set name to players sprite
            }
            else
                players[i].SetActive(false);
        }
        currentSquare = Singleton.instance.SquarePlayers[0]; // set position to player one 
    }
    public Vector3 SetOffsetPosition(int playerTurn)
    {
        if (playerTurn == 0)
            return new Vector3(-offsetPlayer, offsetPlayer * 2, 0);

        if (playerTurn == 1)
            return new Vector3(offsetPlayer, offsetPlayer * 2, 0);

        if (playerTurn == 2)
            return new Vector3(-offsetPlayer, -offsetPlayer, 0);

        if (playerTurn == 3)
            return new Vector3(offsetPlayer, -offsetPlayer, 0);

        return Vector3.zero;
    }
    void UpdateTurn()
    {
        if (IsMyTurn)
            playerUI.ShowDice();
        else
            playerUI.HideDice();
    }
    bool IsMyTurn => Singleton.instance.MyTurn == CurrentPlayer;
    private void UpdatePlayersList()
    {
        List<Student> students = new();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Student student = new Student
            {
                Score = ScoreManager.UpdatedScore(player).FormatScore(),
                NickName = player.NickName
            };
            students.Add(student);
        }
        playerUI.SetPlayersListInfo(students);
    }
    public void ThrowDice()
    {
        int randomDice = Random.Range(1, 7);
        if (Singleton.instance.IsSinglePlayerMode)
            ThrowDiceRPC(randomDice);
        else
            photonView.RPC("ThrowDiceRPC", RpcTarget.All, randomDice);
    }
    [PunRPC]
    void ThrowDiceRPC(int randomDice)
    {
        if (!isMoving)
        {
            currentSquare = Singleton.instance.SquarePlayers[currentPlayer];// players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare;
            dice = randomDice;
            if (isDefaultDice)
                dice = defaultDice;
            playerUI.SetDiceInfo(dice);
            isActiveDice = true;
        }
    }
    void FixedUpdate()
    {
        MoveCurrentPlayer();
    }
    void MoveCurrentPlayer()
    {
        if (isMoving)
        {
            players[CurrentPlayer].transform.localPosition = Vector3.MoveTowards(players[CurrentPlayer].transform.localPosition, nextPosition, Time.fixedDeltaTime * speed);
            if (players[CurrentPlayer].transform.localPosition.Equals(nextPosition))
            {
                isMoving = false;
                dice--;
                //   Debug.Log("DiceNumber:" + dice + "CurrentSquare:" + currentSquare);
            }
        }
        else
        {
            if (isActiveDice)
            {
                if (dice > 0)// if there are more movements, move again
                {
                    // ask for if there are directions, stop movments and stop asking for current LocalPostion
                    if (boardSquares[currentSquare].IsSquarePoint)
                    {
                        isActiveDice = false;
                        print("this is not controlled");
                        if (IsMyTurn)
                            playerUI.ShowArrows(boardSquares[currentSquare].DirectionOne, boardSquares[currentSquare].DirectionTwo);
                    }
                    else
                    {
                        SetNextSquare();
                    }
                }
                else // stop moving
                {
                    isMoving = false;       //this stop moving when player go to their last square
                    isActiveDice = false; // this used for dont loop everytime when moving is false
                    playerUI.ShowQuestionsInfo(IsMyTurn);
                }
            }
        }
    }
    void SetNextSquare()
    {
        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition = boxes[currentSquare].transform.localPosition;
        //  nextPosition.x = boxes[currentSquare].transform.localPosition.x;
        // nextPosition.y = boxes[currentSquare].transform.localPosition.y;
        // nextPosition.z = players[currentPlayer].transform.localPosition.z;
        // put player in his respective corner when go to last box(square)
        if (dice == 1)
            nextPosition += SetOffsetPosition(CurrentPlayer);
        isMoving = true; // this activate the movement
    }
    public void ChangeDirection(string direction)
    {
        if (Singleton.instance.IsSinglePlayerMode)
            ChangeDirectionRPC(direction);
        else
            photonView.RPC("ChangeDirectionRPC", RpcTarget.All, direction);

    }
    [PunRPC]
    public void ChangeDirectionRPC(string direction)
    {
        if (direction == boardSquares[currentSquare].DirectionOne)
            boardSquares[currentSquare].SwitchToOne();
        else
            boardSquares[currentSquare].SwitchToTwo();
        playerUI.HideArrows();
        isActiveDice = true;
        SetNextSquare();
    }
    public void SelectOption(int value)
    {
        if (Singleton.instance.IsSinglePlayerMode)
        {
            SelectOptionRPC(value);
        }
        else
        {
            if (value == Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Questions[Singleton.instance.CurrentTurn].Solution)
            {
                ScoreManager.addStart(PhotonNetwork.LocalPlayer);
            }
            photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
        }
        //  if (photonView.IsMine)
    }
    [PunRPC]
    public async void SelectOptionRPC(int value)
    {
        //playerUI.SelectOption(value);
        UpdatePlayersList();
        await playerUI.SelectOption(value);
        FinishTurn();
        // ChangeTurn();
    }
    async void FinishTurn()
    {   //saving current position current player
        Singleton.instance.SquarePlayers[currentPlayer] = currentSquare;//players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare = currentSquare;
                                                                        // changing player
        CurrentPlayer++;
        // this verifys if the current player is the last one
        if (CurrentPlayer == numberPlayers)
        {
            // CurrentPlayer = 0;
            playerUI.ShowMinigameInfo();
            await Task.Delay(2000);
            playerUI.HideModal(playerUI.minigameInfo.modal);
            UpdateTurn();
            GoToMinigame();
        }
        else
        {
            UpdateTurn();
        }
    }
    public void GoToMinigame()
    {
        Singleton.instance.SetMinigame();
        //Singleton.instance.currentMinigame = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].MyMinigame; ;        if (PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel("minigames");
        // Singleton.instance.CurrentBox = currentSquare;
        //SceneManager.LoadScene("minigames");
    }

    public int CurrentPlayer { get => currentPlayer; set => currentPlayer = GetValue(value); }

    private static int GetValue(int value)
    {
        Singleton.instance.CurrentTurn = value;
        return value;
    }

}


