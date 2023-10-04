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
        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string Position { get => position; set => position = value; }
        public string Scale { get => scale; set => scale = value; }
        public bool IsSolution { get => isSolution; set => isSolution = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

    }
}