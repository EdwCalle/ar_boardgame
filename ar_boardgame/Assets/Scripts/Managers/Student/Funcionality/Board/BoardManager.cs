using UnityEngine;
using Models;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Threading.Tasks;
using System.Collections;
using System;
public class BoardManager : MonoBehaviourPunCallbacks
{
    [Header("Board Components")]
    public UIMediatorManager UImediatorManager;
    public GameManager gameManager;
    public GameObject[] players;
    public GameObject[] boxes;
    [Header("Board Funcionality/offline")]
    [Range(0, 500)]
    public float speed;
    public bool isDefaultDice;
    public int defaultDice;
    public float offsetPlayer = 50f;
    int numberPlayers;
    private int currentSquare = 0;
    //private bool isMoving = false;
    BoardSquare[] boardSquares = new BoardSquare[56];
    private Vector3 nextPosition;
    private int dice;
    //private bool isActiveDice;
    private int currentPlayer = 0;
    // private int playersReady = 0;
    private bool isChangedNextSquare = false;
    void Start()
    {
        numberPlayers = Singleton.GetNumberPlayers();
        // if (Singleton.IsSinglePlayer())
        // {

        //     // PrepareGameRPC();
        // }
        // else
        // {
        //     if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        //     {
        //         // for sync scenes to other players
        //         PhotonNetwork.AutomaticallySyncScene = true;
        //         WaitPlayers("PrepareGameRPC");

        //     }
        // }
    }
    // public void WaitPlayers(string NameRPC)
    // {
    //     photonView.RPC("WaitPlayersRPC", RpcTarget.All, NameRPC);
    // }
    // [PunRPC]
    // void WaitPlayers2RPC(string NameRPC)
    // {
    //     playersReady++;
    //     if (playersReady == numberPlayers && photonView.IsMine)
    //     {
    //         photonView.RPC(NameRPC, RpcTarget.All);
    //     }
    // }
    // [PunRPC]
    public void PrepareBoard()
    {
        PrepareBoxes();
        PreparePlayers();
    }
    // public void PrepareGame()
    // {
    //     ResetPlayersReady();
    //     if (Singleton.ExistRounds()) // si no es la ultima romda
    //     {
    //         // preparing boardsquare order
    //         PrepareBoxes();
    //         PreparePlayers();
    //         // show Round Info
    //         UImediatorManager.SetRoundInfo();
    //         UpdatePlayersList(); // make it in player ui
    //         UImediatorManager.HideWaitBoard();
    //         // if is new round show partial results
    //         if (Singleton.IsNotFirstRound())
    //         {
    //             //  ShowPartialResults();
    //         }
    //         UImediatorManager.ShowDialogue();
    //     }
    //     else
    //     {
    //         UImediatorManager.HideWaitBoard();
    //         //caculate winner
    //         UImediatorManager.ShowResultsInfo();
    //     }


    // }
    // void ResetPlayersReady()
    // {
    //     playersReady = 0;
    // }
    // [PunRPC]
    // void ShowDice2RPC()
    // {
    //     ResetPlayersReady();
    //     UImediatorManager.HideWaitPlayer();
    //     UImediatorManager.ShowDice();
    // }
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
                int playerBox = Singleton.GetSquarePlayer(i);
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
        currentSquare = Singleton.GetSquarePlayer(0); // set position to player one 
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
        // playerUI.SetPlayersListInfo(students);
    }
    // public void RollDice(int randomDice)
    // {
    //     if (Singleton.IsSinglePlayer())
    //         gameManager.RollDiceRPC(randomDice);
    //     else
    //         photonView.RPC("RollDiceRPC", RpcTarget.All, randomDice);
    // }
    // [PunRPC]
    // public void RollDiceRPC(int randomDice)
    // {
    //     //  StartCoroutine(UImediatorManager.diceManager.RotateDiceTime(randomDice));
    //     StartCoroutine(gameManager.diceManager.RotateDiceTime(randomDice));
    // }
    // public void ThrowDice(int randomDice)
    // {
    //     if (Singleton.IsSinglePlayer())
    //         ThrowDiceRPC(randomDice);
    //     else
    //         photonView.RPC("ThrowDiceRPC", RpcTarget.All, randomDice);
    // }
    public void ThrowDice(int randomDice)
    {
        currentSquare = Singleton.GetSquarePlayer(currentPlayer);
        dice = randomDice;
        if (isDefaultDice)
            dice = defaultDice;
        StartCoroutine(MovingPlayer());
    }
    // [PunRPC]
    // public async void ThrowDiceRPC(int randomDice)
    // {
    //     //   UImediatorManager.HideDice();
    //     await gameManager.diceManager.HideModal(gameManager.canvasModal);
    //     //   if (!isMoving)
    //     {
    //         currentSquare = Singleton.GetSquarePlayer(currentPlayer);
    //         dice = randomDice;
    //         if (isDefaultDice)
    //             dice = defaultDice;
    //         // gameManager.diceManager.SetDiceInfo(dice);
    //         //  isActiveDice = true;
    //         //SetNextSquare();
    //         StartCoroutine(MovingPlayer());
    //     }
    // }
    // void FixedUpdate()
    // {
    //       MovecurrentPlayer();
    // }
    // void MovecurrentPlayer()
    // {
    //     if (isMoving)
    //     {
    //         players[currentPlayer].transform.localPosition = Vector3.MoveTowards(players[currentPlayer].transform.localPosition, nextPosition, Time.fixedDeltaTime * speed);
    //         if (players[currentPlayer].transform.localPosition.Equals(nextPosition))
    //         {
    //             isMoving = false;
    //             dice--;
    //             //   Debug.Log("DiceNumber:" + dice + "CurrentSquare:" + currentSquare);
    //         }
    //     }
    //     else
    //     {
    //         if (isActiveDice)
    //         {
    //             if (dice > 0)// if there are more movements, move again
    //             {
    //                 // ask for if there are directions, stop movments and stop asking for current LocalPostion
    //                 if (boardSquares[currentSquare].IsSquarePoint)
    //                 {
    //                     isActiveDice = false;
    //                     print("this is not controlled");
    //                     // if (Singleton.IsMyTurn())
    //                     UImediatorManager.ShowArrows(boardSquares[currentSquare].DirectionOne, boardSquares[currentSquare].DirectionTwo);
    //                 }
    //                 else
    //                 {
    //                     SetNextSquare(dice);
    //                 }
    //             }
    //             else // stop moving
    //             {
    //                 isMoving = false;       //this stop moving when player go to their last square
    //                 isActiveDice = false; // this used for dont loop everytime when moving is false
    //                 UImediatorManager.PrepareQuestions();
    //             }
    //         }
    //     }
    // }

    // [PunRPC]
    // void ShowQuestionRPC()
    // {
    //     ResetPlayersReady();
    //     UImediatorManager.HideWaitPlayer();
    //     UImediatorManager.ShowQuestions();
    // }
    void SetNextSquare(int currentDice)
    {
        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition = boxes[currentSquare].transform.localPosition;
        // put player in his respective corner when go to last box(square)
        if (currentDice == 1)
            nextPosition += SetOffsetPosition(currentPlayer);
        //isMoving = true; // this activate the movement
    }
    // public void ChangeDirection(string direction)
    // {
    //     if (Singleton.IsSinglePlayer())
    //         ChangeDirectionRPC(direction);
    //     else
    //         photonView.RPC("ChangeDirectionRPC", RpcTarget.All, direction);
    // }
    public async Task ChangeDirection(string direction)
    {
        boardSquares[currentSquare].ChangeDirection(direction);
        await gameManager.arrowsManager.HideModal(gameManager.canvasModal);
        isChangedNextSquare = true;
        StartCoroutine(MovingPlayer());
    }
    // [PunRPC]
    // public async void ChangeDirectionRPC(string direction)
    // {
    //     // if (direction == boardSquares[currentSquare].DirectionOne)
    //     //     boardSquares[currentSquare].SwitchToOne();
    //     // else
    //     //     boardSquares[currentSquare].SwitchToTwo();
    //     boardSquares[currentSquare].ChangeDirection(direction);
    //     await gameManager.arrowsManager.HideModal(gameManager.canvasModal);
    //     isChangedNextSquare = true;
    //     StartCoroutine(MovingPlayer());
    // }
    // public void SelectOption(int value)
    // {
    //     if (Singleton.IsSinglePlayer())
    //     {
    //         SelectOptionRPC(value);
    //     }
    //     else
    //     {
    //         if (Singleton.IsAnwserCorrect(value, currentPlayer))
    //         {
    //             ScoreManager.addStart(PhotonNetwork.LocalPlayer);
    //         }
    //         photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
    //     }
    //     //  if (photonView.IsMine)
    // }
    // [PunRPC]
    // public async void SelectOptionRPC(int value)
    // {
    //     //playerUI.SelectOption(value);
    //     UpdatePlayersList();
    //     //  await UImediatorManager.SelectOption(value);
    //     FinishTurn();
    //     // ChangeTurn();
    // }
    public async Task FinishTurn()
    {   //saving current position current player
        Singleton.SaveSquarePlayer(currentPlayer, currentSquare);
        currentPlayer++;
        Singleton.UpdateTurn();
        // this checks if the current player is the last one
        if (currentPlayer == numberPlayers)
        {
            await gameManager.minigameInfoManager.ShowModal(gameManager.canvasModal);
            await Task.Delay(1000);
            await gameManager.minigameInfoManager.HideModal(gameManager.canvasModal);
            Singleton.ResetPlayerTurn();
            GoToMinigame();
        }
        else
        {
            await gameManager.diceManager.ShowModal(gameManager.canvasModal);
        }
    }
    public void GoToMinigame()
    {
        PhotonNetwork.LoadLevel("minigames");
    }
    public IEnumerator MovingPlayer()
    {
        print("que paso el timing");
        while (dice > 0)
        {
            print("dice" + dice);
            if (boardSquares[currentSquare].IsSquarePoint)
            {
                gameManager.ShowArrows(boardSquares[currentSquare].DirectionOne, boardSquares[currentSquare].DirectionTwo);
                //  print(dice + "DEBI MOSTRARLO PERO NO SE QUE PASO");
                break;
            }
            else
            {
                if (isChangedNextSquare)
                {
                    boardSquares[currentSquare].ResetSquarePoint();
                    isChangedNextSquare = false;
                }
                SetNextSquare(dice);
                yield return InternalMove();
                dice--;

            }
        }
        if (dice == 0)
        {
            Singleton.SaveSquarePlayer(currentPlayer, currentSquare);
            yield return gameManager.questionInfoManager.ShowModal(gameManager.canvasModal);
        }
        //
    }
    public IEnumerator InternalMove()
    {
        while (Vector3.Distance(players[currentPlayer].transform.localPosition, nextPosition) > 0.01f)
        {
            players[currentPlayer].transform.localPosition = Vector3.MoveTowards(players[currentPlayer].transform.localPosition, nextPosition, speed * Time.deltaTime);
            yield return null;
        }
        players[currentPlayer].transform.localPosition = nextPosition;
    }
}


