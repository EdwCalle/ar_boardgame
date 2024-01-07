using UnityEngine;
using Models;
using Photon.Pun;
using System.Threading.Tasks;
using System.Collections;
using System;
public class BoardManager : MonoBehaviourPunCallbacks
{
    [Header("Board Components")]
    //   public UIMediatorManager UImediatorManager;
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
    }
    public void PrepareBoard()
    {
        PrepareBoxes();
        PreparePlayers();
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
        // set demons square
        boardSquares[11].SquareType = SquareType.demon;
        boardSquares[12].SquareType = SquareType.demon;
        boardSquares[54].SquareType = SquareType.demon;
        boardSquares[55].SquareType = SquareType.demon;
        // set angel square
        boardSquares[38].SquareType = SquareType.angel;
        boardSquares[39].SquareType = SquareType.angel;
        boardSquares[25].SquareType = SquareType.angel;
        // set random square
        boardSquares[8].SquareType = SquareType.random;
        boardSquares[48].SquareType = SquareType.random;
        boardSquares[49].SquareType = SquareType.random;
        boardSquares[17].SquareType = SquareType.random;
        boardSquares[31].SquareType = SquareType.random;

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
                    gameManager.playersInfoManager.UpdateScore(i);

                }
                //   players[i].GetComponent<PlayerManager>().PrepareData($"Player{i}", i); // set name to players sprite
            }
            else
                players[i].SetActive(false);
        }
        currentSquare = Singleton.GetSquarePlayer(0); // set position to player one 
        gameManager.playersInfoManager.FirstTurn();
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
    // private void UpdatePlayersList()
    // {
    //     List<Student> students = new();
    //     foreach (Player player in PhotonNetwork.PlayerList)
    //     {
    //         Student student = new Student
    //         {
    //             Score = ScoreManager.UpdatedScore(player).FormatScore(),
    //             NickName = player.NickName
    //         };
    //         students.Add(student);
    //     }
    //     // playerUI.SetPlayersListInfo(students);
    // }
    public void ThrowDice(int randomDice)
    {
        currentSquare = Singleton.GetSquarePlayer(currentPlayer);
        dice = randomDice;
        if (isDefaultDice)
            dice = defaultDice;
        StartCoroutine(MovingPlayer());
    }
    void SetNextSquare(int currentDice)
    {
        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition = boxes[currentSquare].transform.localPosition;
        // put player in his respective corner when go to last box(square)
        if (currentDice == 1)
            nextPosition += SetOffsetPosition(currentPlayer);
        //isMoving = true; // this activate the movement
    }
    public async Task ChangeDirection(string direction)
    {
        boardSquares[currentSquare].ChangeDirection(direction);
        await gameManager.arrowsManager.HideModal(gameManager.canvasModal);
        isChangedNextSquare = true;
        StartCoroutine(MovingPlayer());
    }
    public async Task FinishTurn()
    {
        print("me imprimi una vez");
        //saving current position current player
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
            gameManager.playersInfoManager.ChangeTurn();
            await gameManager.diceManager.ShowModal(gameManager.canvasModal);
        }
    }
    public void GoToMinigame()
    {
        PhotonNetwork.LoadLevel("minigames");
    }
    public IEnumerator MovingPlayer()
    {
        //   print("que paso el timing");
        while (dice > 0)
        {
            // print("dice" + dice);
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
            if (boardSquares[currentSquare].SquareType == SquareType.random)

            {
                gameManager.ShowRandomQuestion();
            }
            else
            {
                gameManager.ShowQuestions(boardSquares[currentSquare].SquareType);
            }
            //  Singleton.SaveSquarePlayer(currentPlayer, currentSquare);
            // yield return gameManager.questionInfoManager.ShowModal(gameManager.canvasModal);

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


