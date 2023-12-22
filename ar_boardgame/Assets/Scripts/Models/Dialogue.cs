using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class Dialogue
    {
        [SerializeField] private int numberCharacter;

        [SerializeField, TextArea] private string sentence;
        [SerializeField] private int order;
        private string gesture;
        public string Sentence { get => sentence; set => sentence = value; }
        public int NumberCharacter { get => numberCharacter; set => numberCharacter = value; }
        public int Order { get => order; set => order = value; }
    }
}