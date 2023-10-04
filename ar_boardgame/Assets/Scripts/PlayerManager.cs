using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using TMPro;
using Models;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    public GameObject[] boxes;

    private int currentSquare = 0;
    public bool isDice;
    public int defaultDice;
    Vector2 movementLeft = new Vector3(-100, 0, 0);
    Vector3 movementRight = new Vector3(100, 0, 0);
    Vector3 movementUp = new Vector3(0, 100, 0);
    Vector3 movementDown = new Vector3(0, -100, 0);
    Vector3 currentMovement;
    bool isMoving;
    bool idle;
    BoardSquare[] boardSquares = new BoardSquare[56];
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject selectGame;
    public float velocity;

    Animator animator;
    Vector3 nextPosition;
    private int dice;
    private bool isWaitingClick;
    private bool isActiveDice;

    void Start()
    {
        isMoving = false;
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
        // it hides arrows which are used when the player has to change map's direction
        HideArrows();
        selectGame.SetActive(false);
        // get the animator for makes animations
        // animator = GetComponent<Animator>();

        // nextPosition = boxes[2].transform.position;
    }
    // Update is called once per frame
    void Update()
    {


    }
    void FixedUpdate()
    {
        // if (transform.position.Equals(nextPosition))
        // {
        //     isMoving = false;

        //     print($"dice:{dice} {transform.position.ToString()} {nextPosition.ToString()}");

        //     if (dice > 0)
        //     {
        //         if (!isWaitingClick)
        //             MovePlayer();
        //     }
        //     else
        //         stopRunnig();

        // }
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * velocity);
            if (transform.position.Equals(nextPosition))
            {
                isMoving = false;
                dice--;
                Debug.Log(dice);
            }
        }
        else
        {
            if (isActiveDice)
            {
                if (dice > 0)// if there are more movements, move again
                {
                    // ask for if there are directions, stop movments and stop asking for current position
                    if (boardSquares[currentSquare].IsSquarePoint)
                        showArrows();
                    else
                    {
                        currentSquare = boardSquares[currentSquare].NextSquare;
                        nextPosition.x = boxes[currentSquare].transform.position.x;
                        nextPosition.y = boxes[currentSquare].transform.position.y;
                        isMoving = true;
                    }
                }
                else // stop moving
                {
                    isMoving = false;
                    isActiveDice = false;
                    selectGame.SetActive(true);
                }
            }
        }


    }
    void stopRunnig()
    {
        isMoving = false;
        if (idle)
        {
            //  animator.SetBool("isRunning", false);
            idle = false;
        }
    }
    public void ThrowDice(TextMeshProUGUI number_dice)
    {
        dice = Random.Range(1, 7);
        if (isDice)
            dice = defaultDice;
        number_dice.text = $"{dice}";
        nextPosition.z = transform.position.z;
        // ask for next position, if yes continue
        // currentSquare = boardSquares[currentSquare].NextSquare;
        // nextPosition.x = boxes[currentSquare].transform.position.x;
        // nextPosition.y = boxes[currentSquare].transform.position.y;


        //isMoving = true;
        isActiveDice = true;
        //MovePlayer();

        //animator.SetBool("isRunning", true);
    }

    void MoveToNextSquare()
    {
        // nextPosition.x = transform.position.x + currentMovement.x;
        //nextPosition.y = transform.position.y + currentMovement.y;
        print("move to next square");
        nextPosition.z = transform.position.z;
        // dice--;
        idle = true;
        isMoving = true;
        currentSquare = boardSquares[currentSquare].NextSquare;
        nextPosition.x = boxes[currentSquare].transform.position.x;
        nextPosition.y = boxes[currentSquare].transform.position.y;
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
    public void GoToMinigame(string codeMinigame)
    {
        Debug.Log("go to minigame:" + codeMinigame);
        SceneManager.LoadScene(codeMinigame);
    }
}
