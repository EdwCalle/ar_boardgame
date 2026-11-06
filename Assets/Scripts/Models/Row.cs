

namespace Models
{
    public class Row
    {
        string[] cells;
        int id;
        public Row()
        {
        }
        public string[] Cells { get => cells; set => cells = value; }
        public int Id { get => id; set => id = value; }
    }
}