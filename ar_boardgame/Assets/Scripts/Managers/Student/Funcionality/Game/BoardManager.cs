using UnityEngine;
using Models;
using Photon.Pun;
using System.Threading.Tasks;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;
public class BoardManager : MonoBehaviourPunCallbacks
{
    [Header("Board Components")]
    public UIPlayerManager playerUI;
    public GameObject[] players;
    public GameObject[] boxes;

    [Header("Board Funcionality/offline")]
    [Range(100, 500)]
    public float velocity;
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
        // string json = JsonUtility.ToJson(new Score { Stars = 20 });
        // string xd = new Score { Stars = 12 }.ToJson();
        // Debug.Log(new Score().ToJson());

        // Score score = JsonUtility.FromJson<Score>(xd);
        // {
        //     Debug.Log(score.ToJson());
        // }
        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        {

            // for sync scenes to other players
            PhotonNetwork.AutomaticallySyncScene = true;


            //get number players
            numberPlayers = Singleton.instance.NumberPlayers;
            photonView.RPC("WaitPlayers", RpcTarget.All);



        }
    }
    [PunRPC]
    void WaitPlayers()
    {
        playersReady++;
        if (playersReady == numberPlayers && photonView.IsMine)
        {
            if (skipScene)
                GoToMinigame(1);
            else
                photonView.RPC("PrepareGame", RpcTarget.All);
        }

    }
    [PunRPC]
    void PrepareGame()
    {
        Debug.Log("Me ejecute");
        if (Singleton.instance.currentGeneralGame.Rounds.Count != Singleton.instance.CurrentRound)
        {
            // preparing boardsquare order
            PrepareBoxes();
            PreparePlayers();
            UpdateTurn();
            // show Round Info
            playerUI.roundInfo.roundTitle.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Title;
            playerUI.roundInfo.numberRound.text = $"Número de ronda: {Singleton.instance.CurrentRound}";

            playerUI.WaitPlayersScreen();
            UpdatePlayersList();
            // if is new round show partial results
            if (Singleton.instance.CurrentRound != 0)
                ShowPartialResults();
            // show new dialog
            // ShowDialog();
        }
        else
            //  finish game 
            playerUI.ShowResultsInfo();
    }


    void PreparePlayers()
    {

        for (int i = 0; i < 4; i++)//iterate 4 player
        {
            if (i < numberPlayers)  // if local players are the numberplayer
            {
                int playerBox = Singleton.instance.CurrentBox[i];
                if (playerBox != 0)// set to their postion when restart scene
                {
                    players[i].transform.localPosition = boxes[playerBox].transform.localPosition;
                    players[i].transform.localPosition += SetOffsetPosition(i);
                }

                players[i].GetComponent<PlayerManager>().PrepareData($"Player{i}", i); // set name to players sprite
            }
            else
                players[i].SetActive(false);
        }
        currentSquare = Singleton.instance.CurrentBox[0]; // set position to player one 
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
        //set if the next square has another direction
        boardSquares[0].HasAnotherDirection("down");
        boardSquares[6].HasAnotherDirection("right");
        boardSquares[17].HasAnotherDirection("up");
        boardSquares[23].HasAnotherDirection("left");
        boardSquares[36].HasAnotherDirection("right");
        boardSquares[41].HasAnotherDirection("right");
        boardSquares[19].HasAnotherDirection("up");
        boardSquares[21].HasAnotherDirection("up");
        // first board-square  points to down
        //   currentMovement = movementDown;
    }
    void ShowPartialResults()
    {
        Debug.Log("mostrando resultados parciales");
    }
    void FixedUpdate()
    {
        MoveCurrentPlayer();
    }
    void MoveCurrentPlayer()
    {
        if (isMoving)
        {
            players[CurrentPlayer].transform.localPosition = Vector3.MoveTowards(players[CurrentPlayer].transform.localPosition, nextPosition, Time.fixedDeltaTime * velocity);
            if (players[CurrentPlayer].transform.localPosition.Equals(nextPosition))
            {
                isMoving = false;
                dice--;
                Debug.Log("DiceNumber:" + dice + "CurrentSquare:" + currentSquare);
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
                        //
                        // currentSquare = boardSquares[currentSquare].NextSquare;
                        // nextPosition.x = boxes[currentSquare].transform.localPosition.x;
                        // nextPosition.y = boxes[currentSquare].transform.localPosition.y;
                        // isMoving = true;
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
    public void ThrowDice()
    {
        // if (photonView.IsMine)
        photonView.RPC("ThrowDiceRPC", RpcTarget.All, Random.Range(1, 7));
    }
    [PunRPC]
    void ThrowDiceRPC(int randomDice)
    {

        if (!isMoving)
        {
            currentSquare = Singleton.instance.CurrentBox[currentPlayer];// players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare;
            dice = randomDice;
            if (isDefaultDice)
                dice = defaultDice;
            playerUI.roundInfo.diceTitle.text = $"{dice}";
            isActiveDice = true;
        }
    }
    // [PunRPC]
    async void FinishTurn()
    {   //saving current position current player
        Singleton.instance.CurrentBox[currentPlayer] = currentSquare;//players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare = currentSquare;
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
            GoToMinigame(1);

        }
        else
        {
            UpdateTurn();
        }
    }
    void UpdateTurn()
    {
        if (IsMyTurn)
            playerUI.ShowDice();
        else
            playerUI.HideDice();
    }
    void SetNextSquare()
    {
        print("Setting new square....");

        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition.x = boxes[currentSquare].transform.localPosition.x;
        nextPosition.y = boxes[currentSquare].transform.localPosition.y;
        // nextPosition.z = players[currentPlayer].transform.localPosition.z;
        isMoving = true;
        // put player in his respective corner when go to last box(square)
        if (dice == 1)
            nextPosition += SetOffsetPosition(CurrentPlayer);
    }
    public void SelectOption(int value)
    {
        //  if (photonView.IsMine)
        photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
    }
    [PunRPC]
    public async void SelectOptionRPC(int value)
    {
        //playerUI.SelectOption(value);

        await playerUI.SelectOption(value);
        FinishTurn();
        // ChangeTurn();
    }
    public void ChangeDirection(string direction)
    {
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
    public void GoToMinigame(int code)
    {
        Singleton.instance.SetMinigame(code);
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("minigames");
        // Singleton.instance.CurrentBox = currentSquare;
        //SceneManager.LoadScene("minigames");
    }
    bool IsMyTurn => Singleton.instance.MyTurn == CurrentPlayer;
    public int CurrentPlayer { get => currentPlayer; set => currentPlayer = GetValue(value); }

    private static int GetValue(int value)
    {
        Singleton.instance.CurrentTurn = value;
        return value;
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
        playerUI.SetPlayersListInfo(students);
    }
}


