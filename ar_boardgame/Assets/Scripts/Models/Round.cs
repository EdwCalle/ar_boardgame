using System.Collections.Generic;
using UnityEngine;


namespace Models
{
    [System.Serializable]
    public class Round
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private int numberRound;
        [SerializeField] private Minigame minigame;
        // private Minigame minigame2;
        // private Minigame minigame3;
        // [SerializeField] private Minigame finalMinigame;
        [SerializeField] private List<Question> questions = new();
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
        // public Minigame Minigame2 { get => minigame2; set => minigame2 = value; }
        // public Minigame Minigame3 { get => minigame3; set => minigame3 = value; }
        //  public Minigame FinalMinigame { get => finalMinigame; set => finalMinigame = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public Minigame Minigame { get => minigame; set => minigame = value; }


        public override string ToString()
        {
            return $"id:{id}, title:{Title}, NumberRound:{NumberRound},Minigame1:{Minigame} ";
        }


    }


}
