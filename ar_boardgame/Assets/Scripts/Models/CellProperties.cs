using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class CellProperties
    {
        [SerializeField] private float fontSize = 30f;
        [SerializeField] private float width = 100f;

        public float Width { get => width; set => width = value; }
        public float FontSize { get => fontSize; set => fontSize = value; }
    }
}

