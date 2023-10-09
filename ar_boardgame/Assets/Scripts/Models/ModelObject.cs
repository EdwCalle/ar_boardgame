
using UnityEngine;

namespace Models
{
    public class ModelObject
    {
        private int code;
        private string name;
        private string description;
        private string url;
        private string position;
        private string scale;
        private bool isSolution;
        private bool isSelected = false;

        public ModelObject()
        {
        }

        public ModelObject(int code, string name, string description, string url, string position, string scale, bool isSolution, bool isSelected)
        {
            this.code = code;
            this.name = name;
            this.description = description;
            this.url = url;
            this.position = position;
            this.scale = scale;
            this.isSolution = isSolution;
            this.isSelected = isSelected;
        }

        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string Position { get => position; set => position = value; }
        public string Scale { get => scale; set => scale = value; }
        public bool IsSolution { get => isSolution; set => isSolution = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public Vector3 GetPosition()
        {
            string[] split = Position.Split(";");
            float x = float.Parse(split[0]);
            float y = float.Parse(split[1]);
            //float y = float.Parse("-0,5");
            float z = float.Parse(split[2]);
            return new Vector3(x, y, z);
        }
    }
}