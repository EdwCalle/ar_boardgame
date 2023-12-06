using System;

namespace Models
{
    [System.Serializable]
    public class Score
    {

        public int stars;
        public int caps;
        public int extracoins;

        public int Stars { get => stars; set => stars = value; }
        public int Caps { get => caps; set => caps = value; }
        public int Extracoins { get => extracoins; set => extracoins = value; }
        public Score FormatScore()
        {

            caps += stars / 10;
            stars %= 10;
            return this;
        }
        public void UpdateScore(Score newScore)
        {
            caps += newScore.caps;
            stars += newScore.stars;
            //  EstimateScore();
        }

        public String ToJson()
        {
            return "{\"stars\": " + stars + ", \"caps\": " + caps + "}";
        }
        public override string ToString()
        {
            return "{\"stars\": " + stars + ", \"caps\": " + caps + "}";
        }

    }
}
