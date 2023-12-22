using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class Sentence
    {
        [SerializeField] private int numberCharacter;

        [SerializeField, TextArea] private string line;
        [SerializeField] private int order;
        private string gesture;
        public string Line { get => line; set => line = value; }
        public int NumberCharacter { get => numberCharacter; set => numberCharacter = value; }
        public int Order { get => order; set => order = value; }
    }
}