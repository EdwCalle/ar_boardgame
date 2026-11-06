using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class Dialogue
    {
        [SerializeField] private Sentence[] sentences;
        public Sentence[] Sentences { get => sentences; set => sentences = value; }
    }
}