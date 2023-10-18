
using UnityEngine;

namespace Models
{
    public class Item
    {
        private int code;
        private string name;
        private string description;
        private string url;
        private string position;
        private string scale;
        private bool isSolution;
        private bool isSelected = false;
        private string colliderCenter;
        private string colliderSize;



        public Item()
        {
        }

        public Item(int code, string name, string description, string url, string position, string scale, bool isSolution, bool isSelected, string colliderSize, string colliderCenter)
        {
            this.code = code;
            this.name = name;
            this.description = description;
            this.url = url;
            this.position = position;
            this.scale = scale;
            this.isSolution = isSolution;
            this.isSelected = isSelected;
            this.colliderCenter = colliderCenter;
            this.ColliderSize = colliderSize;
        }

        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string Position { get => position; set => position = value; }
        public string Scale { get => scale; set => scale = value; }
        public bool IsSolution { get => isSolution; set => isSolution = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public string ColliderCenter { get => colliderCenter; set => colliderCenter = value; }
        public string ColliderSize { get => colliderSize; set => colliderSize = value; }

        public Vector3 GetPosition()
        {
            return TransformCoordinate(Position);
        }
        public Vector3 GetColliderScale()
        {
            return GetPosition() + TransformCoordinate(ColliderSize);
        }
        public Vector3 GetColliderCenter()
        {
            return TransformCoordinate(ColliderCenter);
        }
        private Vector3 TransformCoordinate(string position)
        {
            string[] split = position.Split(";");
            float x = float.Parse(split[0]);
            float y = float.Parse(split[1]);
            float z = float.Parse(split[2]);
            return new Vector3(x, y, z);
        }
    }
}