using UnityEngine;
using TMPro;
using Models;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon.StructWrapping;
public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    public GameObject[] boxes;
    private int currentSquare = 0;
    //private int maxRounds = 0;
    //private int currentRound = 0;
    public bool isDice;
    public int defaultDice;

    bool isMoving;
    bool idle;
    BoardSquare[] boardSquares = new BoardSquare[56];
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject upButton;
    public GameObject downButton;
    [SerializeField] public DialogManager selectGame;
    public float velocity;

    //Animator animator;
    Vector3 nextPosition;
    private int dice;
    private bool isWaitingClick;
    private bool isActiveDice;

    public TextMeshProUGUI titleRound;
    public TextMeshProUGUI numberRound;

    //public GameObject firstCharacter;


    public int numberPlayers = 1;
    public int currentPlayer = 0;

    public GameObject[] players;


    void Start()
    {
        //Debug.Log($"\npos FirstGame:{boxes[0].transform.localPosition}\npos player:{transform.localPosition}");
        isMoving = false;
        // preparing boardsquare order
        PrepareBoard();
        // it hides arrows which are used when the player has to change map's direction
        HideArrows();

        for (int i = 0; i < 4; i++)
        {

            if (i > numberPlayers - 1)
                players[i].SetActive(false);
            else
            {
                players[i].GetComponent<PlayerManager>().PrepareData($"Player{i}", i);
            }
        }





        currentSquare = Singleton.instance.currentPosition;
        if (currentSquare != 0)
            transform.localPosition = boxes[currentSquare].transform.localPosition;


        // GetComponent<SpriteRenderer>().sprite = Singleton.instance.firstSpritePlayer;

        //
        if (Singleton.instance.currentGeneralGame.Rounds.Count != Singleton.instance.currentRound)
        {

            // show title round
            titleRound.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Title;
            numberRound.text = $"Número de ronda: {Singleton.instance.currentRound}";
            // show new dialog


        }
        else
        {
            selectGame.ShowDialogFinish();
            // show dialog Finish
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
            players[currentPlayer].transform.localPosition = Vector3.MoveTowards(players[currentPlayer].transform.localPosition, nextPosition, Time.deltaTime * velocity);
            if (players[currentPlayer].transform.localPosition.Equals(nextPosition))
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
                    // ask for if there are directions, stop movments and stop asking for current localPosition
                    if (boardSquares[currentSquare].IsSquarePoint)
                        showArrows();
                    else
                    {
                        //
                        currentSquare = boardSquares[currentSquare].NextSquare;
                        nextPosition.x = boxes[currentSquare].transform.localPosition.x;
                        nextPosition.y = boxes[currentSquare].transform.localPosition.y;
                        isMoving = true;
                    }
                }
                else // stop moving
                {
                    isMoving = false;
                    isActiveDice = false;
                    print("al fin me toca responder");
                    players[currentPlayer].GetComponent<PlayerManager>().CurrentSquare = currentSquare;
                    currentPlayer++;
                    if (currentPlayer == numberPlayers)
                        currentPlayer = 0;

                    selectGame.ShowQuestion();

                }
            }
        }
    }
    public void ThrowDice(TextMeshProUGUI number_dice)
    {

        if (!isMoving)
        {
            currentSquare = players[currentPlayer].GetComponent<PlayerManager>().CurrentSquare;
            dice = Random.Range(1, 7);
            if (!isDice)
                dice = defaultDice;
            number_dice.text = $"{dice}";

            isActiveDice = true;
        }

    }

    void MoveToNextSquare()
    {

        print("move to next square");
        //nextPosition.z = transform.localPosition.z;
        idle = true;
        isMoving = true;
        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition.x = boxes[currentSquare].transform.localPosition.x;
        nextPosition.y = boxes[currentSquare].transform.localPosition.y;
    }
    void showArrows()
    {
        ShowArrow(boardSquares[currentSquare].DirectionOne);
        ShowArrow(boardSquares[currentSquare].DirectionTwo);
    }
    void ShowArrow(string direction)
    {
        switch (direction)
        {
            case "left": leftButton.SetActive(true); break;
            case "right": rightButton.SetActive(true); break;
            case "up": upButton.SetActive(true); break;
            case "down": downButton.SetActive(true); break;
        }
    }
    void HideArrows()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        upButton.SetActive(false);
        downButton.SetActive(false);
    }
    public void Click(string direction)
    {
        if (direction == boardSquares[currentSquare].DirectionOne)
            boardSquares[currentSquare].SwitchToOne();
        else
            boardSquares[currentSquare].SwitchToTwo();
        HideArrows();
        MoveToNextSquare();
        isWaitingClick = false;
    }
    public void GoToMinigame(int code)
    {
        Singleton.instance.currentPosition = currentSquare;
        Singleton.instance.SetMinigame(code);
        SceneManager.LoadScene("minigames");
    }
}
