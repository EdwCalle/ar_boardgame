
using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class Model3D
    {
        [SerializeField] private int code;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private string url;
        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 scale;
        [SerializeField] private bool isSolution;
        [SerializeField] private bool isSelected = false;

        [SerializeField] private ModelInBox modelInBox;
        [SerializeField] private Vector3 colliderScale;
        [SerializeField] private Question question;
        public Model3D()
        {
        }

        public Model3D(int code, string name, string description, string url, Vector3 position, Vector3 scale, bool isSolution, bool isSelected, ModelInBox modelInBox, Vector3 colliderScale)
        {
            this.code = code;
            this.name = name;
            this.description = description;
            this.url = url;
            this.position = position;
            this.scale = scale;
            this.isSolution = isSolution;
            this.isSelected = isSelected;
            this.modelInBox = modelInBox;
            this.colliderScale = colliderScale;
        }

        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Scale { get => scale; set => scale = value; }
        public bool IsSolution { get => isSolution; set => isSolution = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public ModelInBox ModelInBox { get => modelInBox; set => modelInBox = value; }
        public Vector3 ColliderScale { get => colliderScale; set => colliderScale = value; }
        public Collider Collider { get; internal set; }
        public Question Question { get => question; set => question = value; }

        // public Vector3 GetPosition()
        // {
        //     return TransformCoordinate(Position);
        // }
        // public Vector3 GetColliderSize()
        // {
        //     return GetPosition() + TransformCoordinate(ColliderSize);
        // }
        // public Vector3 GetColliderCenter()
        // {
        //     return TransformCoordinate(ColliderCenter);
        // }
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