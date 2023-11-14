using System.Collections.Generic;

namespace Models
{
    public class GeneralGame
    {
        private int id;
        private string title;
        private string subject;
        private List<Round> rounds = new List<Round>();

        public GeneralGame()
        {
        }

        public GeneralGame(int id, string title, string subject)
        {
            this.id = id;
            this.title = title;
            this.subject = subject;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Subject { get => subject; set => subject = value; }
        public List<Round> Rounds { get => rounds; set => rounds = value; }

        public override string ToString()
        {
            return $"Title:{title},rounds:{string.Join("; ", rounds)}" + "";
        }


    }
}