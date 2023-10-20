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
    }


}
