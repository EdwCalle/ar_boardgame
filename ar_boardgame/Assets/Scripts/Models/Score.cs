
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
    }
}
