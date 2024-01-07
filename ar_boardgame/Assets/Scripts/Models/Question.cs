using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class Question
    {
        [SerializeField] private string title;
        [SerializeField] private string optionA;
        [SerializeField] private string optionB;
        [SerializeField] private string optionC;
        [SerializeField] private string optionD;
        [SerializeField] private int solution;
        [SerializeField] private int optionSelected = 0;



        public string OptionD { get => optionD; set => optionD = value; }
        public string OptionC { get => optionC; set => optionC = value; }
        public string OptionB { get => optionB; set => optionB = value; }
        public string OptionA { get => optionA; set => optionA = value; }
        public string Title { get => title; set => title = value; }
        public int Solution { get => solution; set => solution = value; }
        public int OptionSelected { get => optionSelected; set => optionSelected = value; }

        public Question(string title, string OptionA, string OptionB, string OptionC, string OptionD, int solution = 0)
        {
            this.OptionD = OptionD;
            this.OptionC = OptionC;
            this.OptionB = OptionB;
            this.OptionA = OptionA;
            this.title = title;
            this.solution = solution;
        }
        public Question()
        {
        }
    }
}