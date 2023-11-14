namespace Models
{
    public class Round
    {
        private int id;
        private string title;
        private int numberRound;
        private Minigame minigame1;
        private Minigame minigame2;
        private Minigame minigame3;
        private Minigame finalMinigame;
        private Question questionForA;
        private Question questionForB;
        private Question questionForC;
        private Question questionForD;


        public Round(int id, string title, int numberRound)
        {
            this.id = id;
            this.title = title;
            this.numberRound = numberRound;
        }
        public Round()
        {

        }
        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public int NumberRound { get => numberRound; set => numberRound = value; }
        public Minigame Minigame1 { get => minigame1; set => minigame1 = value; }
        public Minigame Minigame2 { get => minigame2; set => minigame2 = value; }
        public Minigame Minigame3 { get => minigame3; set => minigame3 = value; }
        public Minigame FinalMinigame { get => finalMinigame; set => finalMinigame = value; }
        public Question QuestionForA { get => questionForA; set => questionForA = value; }
        public Question QuestionForB { get => questionForB; set => questionForB = value; }
        public Question QuestionForC { get => questionForC; set => questionForC = value; }
        public Question QuestionForD { get => questionForD; set => questionForD = value; }

        public override string ToString()
        {
            return $"id:{id}, title:{Title}, NumberRound:{NumberRound},Minigame1:{Minigame1} ";
        }


    }


}
