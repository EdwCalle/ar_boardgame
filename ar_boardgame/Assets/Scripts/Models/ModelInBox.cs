using UnityEngine;

namespace Models
{
    public class ModelInBox
    {
        private Vector3 position;
        private Vector3 scale;

        public ModelInBox()
        {
        }

        public ModelInBox(Vector3 position, Vector3 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Scale { get => scale; set => scale = value; }
    }
}
