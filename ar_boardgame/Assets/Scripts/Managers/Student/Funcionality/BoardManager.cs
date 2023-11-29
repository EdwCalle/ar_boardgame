using UnityEngine;
using Models;
using UnityEngine.SceneManagement;
using Photon.Pun;
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
    public int numberPlayers = 1;
    public float offsetPlayer = 50f;

    private int currentSquare = 0;
    private bool isMoving;
    BoardSquare[] boardSquares = new BoardSquare[56];
    private Vector3 nextPosition;
    private int dice;
    private bool isActiveDice;
    private int currentPlayer = 0;


    void Start()
    {

        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        {
            // if (photonView.IsMine)

            ChangeTurn();

            // Singleton.instance
            //Debug.Log($"\npos FirstGame:{boxes[0].transform.localPosition}\npos player:{transform.localPosition}");
            isMoving = false;
            // preparing boardsquare order
            PrepareBoard();

            numberPlayers = Singleton.instance.NumberPlayers;

            for (int i = 0; i < 4; i++)
            {

                if (i > numberPlayers - 1)
                    players[i].SetActive(false);
                else
                {
                    players[i].GetComponent<PlayerManager>().PrepareData($"Player{i}", i);
                }
            }


            currentSquare = Singleton.instance.CurrentRound;
            if (currentSquare != 0)
                transform.localPosition = boxes[currentSquare].transform.localPosition;


            // GetComponent<SpriteRenderer>().sprite = Singleton.instance.firstSpritePlayer;

            //
            if (Singleton.instance.currentGeneralGame.Rounds.Count != Singleton.instance.CurrentRound)
            {

                // show title round
                playerUI.roundInfo.roundTitle.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.CurrentRound].Title;
                playerUI.roundInfo.numberRound.text = $"Número de ronda: {Singleton.instance.CurrentRound}";
                // show new dialog
            }
            else
            {
                playerUI.ShowResultsInfo();
                // show dialog Finish
            }
        }

    }
    // Update is called once per frame
    void Update()
    {


    }
    void PrepareBoard()
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
    void FixedUpdate()
    {
        MoveCurrentPlayer();
    }
    void MoveCurrentPlayer()
    {
        if (isMoving)
        {
            players[CurrentPlayer].transform.localPosition = Vector3.MoveTowards(players[CurrentPlayer].transform.localPosition, nextPosition, Time.deltaTime * velocity);
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

    public void SetOffsetPosition()
    {
        if (CurrentPlayer == 0)
            nextPosition += new Vector3(-offsetPlayer, offsetPlayer * 2, 0);
        if (CurrentPlayer == 1)
            nextPosition += new Vector3(offsetPlayer, offsetPlayer * 2, 0);
        if (CurrentPlayer == 2)
            nextPosition += new Vector3(-offsetPlayer, -offsetPlayer, 0);
        if (CurrentPlayer == 3)
            nextPosition += new Vector3(offsetPlayer, -offsetPlayer, 0);
    }
    public void ThrowDice()
    {
        photonView.RPC("ThrowDiceRPC", RpcTarget.All, Random.Range(1, 7));
    }
    [PunRPC]
    void ThrowDiceRPC(int randomDice)
    {
        if (!isMoving)
        {
            currentSquare = players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare;
            dice = randomDice;
            if (isDefaultDice)
                dice = defaultDice;
            playerUI.roundInfo.diceTitle.text = $"{dice}";
            isActiveDice = true;
        }
    }
    // [PunRPC]
    void FinishTurn()
    {   //saving current position current player
        players[CurrentPlayer].GetComponent<PlayerManager>().CurrentSquare = currentSquare;
        // changing player
        CurrentPlayer++;
        // this verifys if the current player is the last one
        if (CurrentPlayer == numberPlayers)
            CurrentPlayer = 0;


    }
    void ChangeTurn()
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
            SetOffsetPosition();
    }
    public void SelectOption(int value)
    {
        photonView.RPC("SelectOptionRPC", RpcTarget.All, value);
    }
    [PunRPC]
    public void SelectOptionRPC(int value)
    {
        playerUI.SelectOption(value);
        FinishTurn();
        ChangeTurn();
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
        SetNextSquare();
    }
    public void GoToMinigame(int code)
    {
        Singleton.instance.CurrentBox = currentSquare;
        Singleton.instance.SetMinigame(code);
        SceneManager.LoadScene("minigames");
    }
    bool IsMyTurn => Singleton.instance.MyTurn == CurrentPlayer;
    public int CurrentPlayer { get => currentPlayer; set => currentPlayer = GetValue(value); }

    private static int GetValue(int value)
    {
        Singleton.instance.CurrentTurn = value;
        return value;
    }
}
