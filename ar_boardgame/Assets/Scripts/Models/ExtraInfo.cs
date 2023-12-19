using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class ExtraInfo
    {
        [SerializeField] private string type;
        [SerializeField] private string info;
        public ExtraInfo()
        {

        }

        public ExtraInfo(string type)
        {
            this.type = type;
        }
        public ExtraInfo(string type, string info = null)
        {
            this.type = type;
            this.info = info;
        }
        public string Type { get => type; set => type = value; }
        public string Info { get => info; set => info = value; }
    }
}