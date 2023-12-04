namespace Models
{
    public class Score
    {
        private int stars;
        private int caps;
        private int extracoins;

        public int Stars { get => stars; set => stars = value; }
        public int Caps { get => caps; set => caps = value; }
        public int Extracoins { get => extracoins; set => extracoins = value; }
        public void FormatScore()
        {
            caps = stars / 10;
            stars %= 10;
        }
        public void UpdateScore(Score newScore)
        {
            caps += newScore.caps;
            stars += newScore.stars;
            //  EstimateScore();
        }
        public static Score GetMinigameResults(Minigame minigame)
        {
            if (minigame.Level == 1 || minigame.Level == 2)
                return GetResultMinigame1(minigame);
            if (minigame.Level == 3)
                return GetResultMinigame3(minigame);
            return null;
        }

        private static Score GetResultMinigame1(Minigame minigame)
        {
            int stars = 0;
            foreach (Item item in minigame.Models)
            {
                if (item.IsSolution == item.IsSelected)
                    stars++;
            }
            // Score score = new() { Stars = stars };
            // score.EstimateScore();
            return new() { Stars = stars }; ;
        }
        private static Score GetResultMinigame3(Minigame minigame)
        {
            int stars = 0;
            foreach (Item item in minigame.Models)
            {
                if (item.IsSolution = item.IsSelected)
                    stars++;
            }
            return new Score { Stars = stars };
        }

    }
}
