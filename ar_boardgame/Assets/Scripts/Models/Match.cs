using System.Collections.Generic;
using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class Match
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private int numberPlayers;
        [SerializeField] private string subject;
        [SerializeField] private List<Round> rounds = new();
        [SerializeField] private List<Question> angelQuestions;
        [SerializeField] private List<Question> demonQuestions;
        public Match()
        {
        }

        public Match(int id, string title, string subject)
        {
            this.id = id;
            this.title = title;
            this.subject = subject;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Subject { get => subject; set => subject = value; }
        public List<Round> Rounds { get => rounds; set => rounds = value; }
        public List<Question> AngelQuestions { get => angelQuestions; set => angelQuestions = value; }
        public List<Question> DemonQuestions { get => demonQuestions; set => demonQuestions = value; }
        public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }

        public override string ToString()
        {
            return $"Title:{title},rounds:{string.Join("; ", rounds)}" + "";
        }


    }
}