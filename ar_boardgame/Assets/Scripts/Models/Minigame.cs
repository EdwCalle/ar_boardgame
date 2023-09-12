namespace Models
{
    public class Minigame
    {
        private string description;
        private string difficulty;
        private string title;
        public string Description { get => description; set => description = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public string Title { get => title; set => title = value; }
    }
}