using System.Collections.Generic;
using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class GeneralGame
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private string subject;
        [SerializeField] private List<Round> rounds = new List<Round>();

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