
namespace Models
{
    public class BoardSquare
    {
        private int nextSquare;
        private bool isSquarePoint;

        private string directionOne;
        private string directionTwo;
        private int squareOne;
        private int squareTwo;
        public BoardSquare(int nextSquare)
        {
            this.nextSquare = nextSquare;
            directionOne = "none";
            directionTwo = "none";
        }
        public int NextSquare { get => nextSquare; set => nextSquare = value; }
        public bool IsSquarePoint { get => isSquarePoint; set => isSquarePoint = value; }
        // public bool IsNextSquareDirection { get => isNextSquareDirection; set => isNextSquareDirection = value; }
        public string DirectionOne { get => directionOne; set => directionOne = value; }
        public string DirectionTwo { get => directionTwo; set => directionTwo = value; }

        public void setSquarePointPropeties(string directionOne, string directionTwo, int squareOne, int squareTwo)
        {
            isSquarePoint = true;
            this.directionOne = directionOne;
            this.directionTwo = directionTwo;
            this.squareOne = squareOne;
            this.squareTwo = squareTwo;
        }
        public void HasAnotherDirection(string directionOne)
        {
            //   isNextSquareDirection = true;
            this.directionOne = directionOne;
        }
        public void SwitchToOne()
        {
            nextSquare = squareOne;
            isSquarePoint = false;
        }
        public void SwitchToTwo()
        {
            nextSquare = squareTwo;
            isSquarePoint = false;
        }
        public void ResetSquarePoint()
        {
            isSquarePoint = true;
        }

    }
}