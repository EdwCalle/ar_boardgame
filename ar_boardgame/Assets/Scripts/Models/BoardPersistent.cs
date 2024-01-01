using UnityEngine;
namespace Models
{
  [System.Serializable]
  public class BoardPersistent
  {
    [SerializeField] private int numberPlayers = 2;
    private int myTurn = 0;
    private int currentPlayerTurn = 0;
    private int currentRound = 0;
    private int[] squarePlayers = { 0, 0, 0, 0 };

    /// <summary>
    /// NumberPlayers in room
    /// </summary>
    public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }
    /// <summary>
    /// Get my turn. 
    /// </summary>
    public int MyTurn { get => myTurn; set => myTurn = value; }
    /// <summary>
    /// Current Round
    /// </summary>
    public int CurrentRound { get => currentRound; set => currentRound = value; }
    /// <summary>
    /// Positions of each player in board
    /// </summary>
    //  public int[] SquarePlayers { get => squarePlayers; set => squarePlayers = value; }
    /// <summary>
    /// Turn of currentPlayer
    /// </summary>
    public int CurrentPlayerTurn { get => currentPlayerTurn; set => currentPlayerTurn = value; }
    public int[] SquarePlayers { get => squarePlayers; set => squarePlayers = value; }

    /// <summary>
    /// All players data
    /// </summary>
    // public Student[] Students { get => students; set => students = value; }






  }
}