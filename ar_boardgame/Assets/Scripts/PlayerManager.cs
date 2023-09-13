using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using TMPro;
using Models;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    // public GameObject[] boardSquares;
    private int currentSquare = 0;
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
    public float velocity;

    Animator animator;
    Vector3 nextPosition;
    private int dice;
    private bool isWaitingClick;

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
        boardSquares[14].setSquarePointPropeties("up", "right", 53, 15);
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
        // get the animator for makes animations
        // animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {


    }
    void FixedUpdate()
    {
        if (transform.position.Equals(nextPosition))
        {
            if (dice > 0)
            {
                if (!isWaitingClick)
                    MovePlayer();
            }
            else
                stopRunnig();

        }
        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * velocity * 54);


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
        number_dice.text = $"{dice}";
        MovePlayer();
        //animator.SetBool("isRunning", true);
    }
    void MovePlayer()
    {
        // ask if the currentSquare is the last in the dice, if it's true then don't ask for select direction , else
        if (boardSquares[currentSquare].IsSquarePoint)
        {
            isWaitingClick = true;
            // HideArrows();
            ShowArrow(boardSquares[currentSquare].DirectionOne);
            ShowArrow(boardSquares[currentSquare].DirectionTwo);
        }
        else
        {
            // ask if the next sqaure change direction
            if (boardSquares[currentSquare].IsNextSquareDirection)
                ChangeDirection(boardSquares[currentSquare].DirectionOne);
            MoveToNextSquare();

        }
    }
    void MoveToNextSquare()
    {

        // lack animation (1 sec or less)
        nextPosition.x = transform.position.x + currentMovement.x;
        nextPosition.y = transform.position.y + currentMovement.y;
        nextPosition.z = transform.position.z;
        dice--;
        isMoving = true;
        idle = true;
        currentSquare = boardSquares[currentSquare].NextSquare;

    }
    void ChangeDirection(string direction)
    {
        switch (direction)
        {
            case "left": currentMovement = movementLeft; break;
            case "right": currentMovement = movementRight; break;
            case "up": currentMovement = movementUp; break;
            case "down": currentMovement = movementDown; break;
        }
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
        {
            boardSquares[currentSquare].SwitchToOne();
            ChangeDirection(direction);

        }
        else
        {
            boardSquares[currentSquare].SwitchToTwo();
            ChangeDirection(boardSquares[currentSquare].DirectionTwo);
        }

        HideArrows();
        MoveToNextSquare();
        isWaitingClick = false;
    }

    public void GoToMinigame(int codeMinigame)
    {
        Debug.Log("go to minigame:" + codeMinigame);
    }
}
