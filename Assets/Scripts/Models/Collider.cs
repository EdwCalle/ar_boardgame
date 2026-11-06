using UnityEngine;

namespace Models
{
    public class Collider
    {
        private Vector3 position;
        private Vector3 scale;

        public Collider()
        {
        }

        public Collider(Vector3 position, Vector3 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        public Vector3 Center { get => position; set => position = value; }
        public Vector3 Size { get => scale; set => scale = value; }
    }
}
