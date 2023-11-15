namespace Models
{
    public class Question
    {
        private string title;
        private string optionA;
        private string optionB;
        private string optionC;
        private string optionD;
        private int solution;



        public string OptionD { get => optionD; set => optionD = value; }
        public string OptionC { get => optionC; set => optionC = value; }
        public string OptionB { get => optionB; set => optionB = value; }
        public string OptionA { get => optionA; set => optionA = value; }
        public string Title { get => title; set => title = value; }
        public int Solution { get => solution; set => solution = value; }

        public Question(string title, string OptionA, string OptionB, string OptionC, string OptionD, int solution = 0)
        {
            this.OptionD = OptionD;
            this.OptionC = OptionC;
            this.OptionB = OptionB;
            this.OptionA = OptionA;
            this.title = title;
            this.solution = solution;
        }
    }
}