using UnityEngine;

namespace Models
{
    public class Collider
    {
        private Vector3 center;
        private Vector3 size;



        public Collider()
        {
        }

        public Collider(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.size = size;
        }

        public Vector3 Center { get => center; set => center = value; }
        public Vector3 Size { get => size; set => size = value; }
    }
}
