using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class BoardPersistent
    {
        [SerializeField] private int numberPlayers = 2;
        // [SerializeField]
        private int myTurn = 0;
        //  [SerializeField]
        private int currentRound = 0;
        //  [SerializeField]
        // private int currentTurn = 0;
        //[SerializeField]
        private int[] squarePlayers = { 0, 0, 0, 0 };

        public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }
        public int MyTurn { get => myTurn; set => myTurn = value; }
        public int CurrentRound { get => currentRound; set => currentRound = value; }
        //   public int CurrentTurn { get => currentTurn; set => currentTurn = value; }
        public int[] SquarePlayers { get => squarePlayers; set => squarePlayers = value; }

    }
}