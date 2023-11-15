namespace Models
{
    public class Question
    {
        private string title;
        private string optionA;
        private string optionB;
        private string optionC;
        private string optionD;



        public string OptionD { get => optionD; set => optionD = value; }
        public string OptionC { get => optionC; set => optionC = value; }
        public string OptionB { get => optionB; set => optionB = value; }
        public string OptionA { get => optionA; set => optionA = value; }
        public string Title { get => title; set => title = value; }

        public Question(string title, string OptionA, string OptionB, string OptionC, string OptionD)
        {
            this.OptionD = OptionD;
            this.OptionC = OptionC;
            this.OptionB = OptionB;
            this.OptionA = OptionA;
            this.title = title;

        }
    }
}